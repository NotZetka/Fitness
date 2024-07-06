﻿using API.Data;
using API.Exceptions;
using API.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanQueryHandler : IRequestHandler<AddPlanQuery, AddPlanQueryResult>
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public AddPlanQueryHandler(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<AddPlanQueryResult> Handle(AddPlanQuery request, CancellationToken cancellationToken)
        {
            var planTemplate = _context
                .FitnessPlanTemplates
                .Include(x=>x.Exercises)
                .Where(x => x.Public)
                .FirstOrDefault(x => x.Id == request.Id);

            if (planTemplate == null) throw new NotFoundException($"Plan with id {request.Id} has not been found");

            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans: true);

            if (user.FitnessPlans.Select(x => x.TemplateId).Contains(planTemplate.Id)) return new AddPlanQueryResult();

            var plan = new FitnessPlan
            {
                TemplateId = planTemplate.Id,
                Archived = false,
                User = user,
                Name = planTemplate.Name,
                Exercises = planTemplate.Exercises.Select(x => new Exercise
                {
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(),
            };

            await _context.FitnessPlans.AddAsync(plan);
            await _context.SaveChangesAsync();

            return new AddPlanQueryResult();
        }
    }
}