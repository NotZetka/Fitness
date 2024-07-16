using API.Data.Repositories.BodyWeightRepository;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightQueryHandler : IRequestHandler<GetBodyWeightQuery, GetBodyWeightQueryResponse>
    {
        private readonly IUserService _userService;
        private readonly IBodyWeightRepository _bodyWeightRepository;

        public GetBodyWeightQueryHandler(IUserService userService, IBodyWeightRepository bodyWeightRepository)
        {
            _userService = userService;
            _bodyWeightRepository = bodyWeightRepository;
        }
        public async Task<GetBodyWeightQueryResponse> Handle(GetBodyWeightQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var bodyWeight = await _bodyWeightRepository.GetBodyWeightAsync(user.Id);

            return new GetBodyWeightQueryResponse() { 
                BodyWeight = bodyWeight,
                GenderMale = user.Gender.Equals("male", StringComparison.OrdinalIgnoreCase),
            };
        }
    }
}
