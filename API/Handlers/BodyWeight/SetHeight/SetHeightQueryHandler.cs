using API.Data.Repositories;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightQueryHandler : IRequestHandler<SetHeightQuery, SetHeightQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public SetHeightQueryHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<SetHeightQueryResponse> Handle(SetHeightQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();

            _unitOfWork.BodyWeightRepository.SetHeight(request.Height, userId);

            await _unitOfWork.SaveChangesAsync();

            return new SetHeightQueryResponse();
        }
    }
}
