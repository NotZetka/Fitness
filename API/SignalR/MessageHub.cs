using API.Data;
using API.Data.Dtos;
using API.Data.Repositories;
using API.Exceptions;
using API.Handlers.Messages.SendMessage;
using API.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class MessageHub(
        IUnitOfWork _unitOfWork,
        IUserService _userService,
        IMapper _mapper,
        IValidator<SendMessageCommand> _queryValidator) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var otherUser = httpContext.Request.Query["userId"].FirstOrDefault();

            if(otherUser == null) throw new DirectoryNotFoundException("User has not been found");

            var otherUserId = int.Parse(otherUser);

            var currnetUserId = _userService.GetCurrentUserId();

            var groupname = GetGroupName(currnetUserId, otherUserId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);


            var messages = await _unitOfWork.MessageRepository.GetMessageThreadAsync(currnetUserId, otherUserId);

            await Clients.Group(groupname).SendAsync("ReceiveMessageThread", messages);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageCommand request) 
        {
            var validation = _queryValidator.Validate(request);

            if (!validation.IsValid) throw new ValidationException(validation.Errors);

            var currentUser = await _userService.GetCurrentUserAsync();

            if (request.ReceiverId == currentUser.Id) throw new ForbiddenException("You can't send message to yourself");

            var receiver = await _unitOfWork.UsersRepository.FindUserByIdAsync(request.ReceiverId);

            if (receiver == null) throw new NotFoundException("Receiver has not been fund");

            var message = new Message
            {
                Sender = currentUser,
                SenderUsername = currentUser.UserName,
                Receiver = receiver,
                ReceiverUsername = receiver.UserName,
                Content = request.Content,
            };

            _unitOfWork.MessageRepository.Add(message);

            await _unitOfWork.SaveChangesAsync();

            var group = GetGroupName(currentUser.Id, request.ReceiverId);
            await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
        }
        private string GetGroupName(int currentUserId, int secondUserId)
        {
            return currentUserId > secondUserId ?
                $"{secondUserId} - {currentUserId}" :
                $"{currentUserId} - {secondUserId}";
        }
    }
}
