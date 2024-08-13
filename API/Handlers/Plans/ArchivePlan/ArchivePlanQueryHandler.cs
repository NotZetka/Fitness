﻿using API.Data.Repositories.PlansRepository;
using API.Exceptions;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.ArchivePlan
{
    public class ArchivePlanQueryHandler : IRequestHandler<ArchivePlanQuery, ArchivePlanQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ArchivePlanQueryHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArchivePlanQueryResult> Handle(ArchivePlanQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans: true);
            var plan = user.FitnessPlans.FirstOrDefault(x => x.Id == request.PlanId);

            if (plan == null) throw new NotFoundException($"user doesn't own plan with id: {request.PlanId}");

            plan.Archived = !plan.Archived;

            await _unitOfWork.SaveChangesAsync();

            return new();
        }
    }
}
