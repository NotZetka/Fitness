using API.Data.Dtos;
using API.Data.Repositories.MessagesRepository;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadQueryHandler : IRequestHandler<GetMessageThreadQuery, GetMessageThreadQueryResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetMessageThreadQueryHandler(IMessageRepository messageRepository, IUserService userService, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<GetMessageThreadQueryResponse> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            var curentUserId = _userService.GetCurrentUserId();

            var messageThread = await _messageRepository.GetMessageThreadAsync(curentUserId, request.userId);

            return new GetMessageThreadQueryResponse()
            {
                Messages = messageThread.Select(_mapper.Map<MessageDto>)
            };
        }
    }
}
