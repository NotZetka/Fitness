using API.Data.Repositories.BodyWeightRepository;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightQueryHandler : IRequestHandler<SetHeightQuery, SetHeightQueryResponse>
    {
        private readonly IBodyWeightRepository _bodyWeightRepository;
        private readonly IUserService _userService;

        public SetHeightQueryHandler(IBodyWeightRepository bodyWeightRepository, IUserService userService)
        {
            _bodyWeightRepository = bodyWeightRepository;
            _userService = userService;
        }
        public async Task<SetHeightQueryResponse> Handle(SetHeightQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();

            _bodyWeightRepository.SetHeight(request.Height, userId);

            await _bodyWeightRepository.SaveChangesAsync();

            return new SetHeightQueryResponse();
        }
    }
}
