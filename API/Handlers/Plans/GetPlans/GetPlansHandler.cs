using API.Data;
using API.Data.Dtos;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.GetPlans
{
    public class GetPlansHandler : IRequestHandler<GetPlansQuery, GetPlansResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetPlansHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<GetPlansResponse> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans: true);
            var plans = user.FitnessPlans.Select(_mapper.Map<FitnessPlanDto>);

            return new() { Plans = plans }; 
        }
    }
}
