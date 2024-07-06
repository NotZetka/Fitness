﻿using API.Data.Dtos;
using API.Exceptions;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.GetPlan
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, GetPlanQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetPlanQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<GetPlanQueryResult> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeRecords:true);
            var plan = user.FitnessPlans.FirstOrDefault(x=>x.Id == request.PlanId);

            if (plan == null) throw new NotFoundException($"user doesn't own plan with id: {request.PlanId}");

            var result = new GetPlanQueryResult()
            {
                Plan = _mapper.Map<FitnessPlanDto>(plan)
            };

            return result;
        }
    }
}
