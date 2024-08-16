using API.Data.Dtos;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadHandler : IRequestHandler<GetMessageThreadQuery, GetMessageThreadResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetMessageThreadHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<GetMessageThreadResponse> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            var curentUserId = _userService.GetCurrentUserId();

            var messageThread = await _unitOfWork.MessageRepository.GetMessageThreadAsync(curentUserId, request.userId);

            return new GetMessageThreadResponse()
            {
                Messages = messageThread.Select(_mapper.Map<MessageDto>)
            };
        }
    }
}
