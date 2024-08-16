using API.Data.Repositories;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightHandler : IRequestHandler<GetBodyWeightQuery, GetBodyWeightResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public GetBodyWeightHandler(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetBodyWeightResponse> Handle(GetBodyWeightQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var bodyWeight = await _unitOfWork.BodyWeightRepository.GetBodyWeightAsync(user.Id);

            return new GetBodyWeightResponse() { 
                BodyWeight = bodyWeight,
                GenderMale = user.Gender.Equals("male", StringComparison.OrdinalIgnoreCase),
            };
        }
    }
}
