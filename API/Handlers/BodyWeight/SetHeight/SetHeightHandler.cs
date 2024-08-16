using API.Data.Repositories;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightHandler : IRequestHandler<SetHeightCommand, SetHeightResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public SetHeightHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<SetHeightResponse> Handle(SetHeightCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();

            _unitOfWork.BodyWeightRepository.SetHeight(request.Height, userId);

            await _unitOfWork.SaveChangesAsync();

            return new SetHeightResponse();
        }
    }
}
