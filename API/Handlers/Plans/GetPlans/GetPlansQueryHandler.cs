﻿using API.Data;
using API.Data.Dtos;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.GetPlans
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, GetPlansQueryResult>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetPlansQueryHandler(DataContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<GetPlansQueryResult> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans: true);
            var plans = user.FitnessPlans.Select(_mapper.Map<FitnessPlanDto>);

            return new() { Plans = plans }; 
        }
    }
}