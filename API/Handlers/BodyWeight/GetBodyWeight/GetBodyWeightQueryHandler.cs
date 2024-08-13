using API.Data.Repositories;
using API.Services;
using MediatR;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightQueryHandler : IRequestHandler<GetBodyWeightQuery, GetBodyWeightQueryResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public GetBodyWeightQueryHandler(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetBodyWeightQueryResponse> Handle(GetBodyWeightQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var bodyWeight = await _unitOfWork.BodyWeightRepository.GetBodyWeightAsync(user.Id);

            return new GetBodyWeightQueryResponse() { 
                BodyWeight = bodyWeight,
                GenderMale = user.Gender.Equals("male", StringComparison.OrdinalIgnoreCase),
            };
        }
    }
}
