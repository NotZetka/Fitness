using API.Data.Dtos;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadQueryHandler : IRequestHandler<GetMessageThreadQuery, GetMessageThreadQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetMessageThreadQueryHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<GetMessageThreadQueryResponse> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
            var curentUserId = _userService.GetCurrentUserId();

            var messageThread = await _unitOfWork.MessageRepository.GetMessageThreadAsync(curentUserId, request.userId);

            return new GetMessageThreadQueryResponse()
            {
                Messages = messageThread.Select(_mapper.Map<MessageDto>)
            };
        }
    }
}
