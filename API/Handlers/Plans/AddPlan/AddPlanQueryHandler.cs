using API.Data;
using API.Data.Repositories.PlansRepository;
using API.Exceptions;
using API.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanQueryHandler : IRequestHandler<AddPlanQuery, AddPlanQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IPlansRepository _plansRepository;

        public AddPlanQueryHandler(IUserService userService, IPlansRepository plansRepository)
        {
            _userService = userService;
            _plansRepository = plansRepository;
        }
        public async Task<AddPlanQueryResult> Handle(AddPlanQuery request, CancellationToken cancellationToken)
        {
            var planTemplate = await _plansRepository.GetPlanTemplateByIdAsync(request.Id, true);

            if (planTemplate == null) throw new NotFoundException($"Plan with id {request.Id} has not been found");
            if (planTemplate.AuthorId != _userService.GetCurrentUserId() && !planTemplate.Public) throw new ForbiddenException("You are not allowed to add this plan");

            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans: true);

            if (user.FitnessPlans.Select(x => x.TemplateId).Contains(planTemplate.Id)) return new AddPlanQueryResult();

            var exercises = planTemplate.Exercises
                .SelectMany(x => Enumerable.Range(1, x.Sets)
                    .Select(_ => new Exercise
                    {
                        Name = x.Name,
                        Description = x.Description
                    })
                ).ToList();


            var plan = new FitnessPlan
            {
                TemplateId = planTemplate.Id,
                Archived = false,
                User = user,
                Name = planTemplate.Name,
                Exercises = exercises,
            };

            _plansRepository.AddFitnessPlan(plan);
            await _plansRepository.SaveChangesAsync();

            return new AddPlanQueryResult();
        }
    }
}
