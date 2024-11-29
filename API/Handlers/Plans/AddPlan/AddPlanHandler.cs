using API.Data;
using API.Data.Repositories;
using API.Exceptions;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanHandler : IRequestHandler<AddPlanCommand, AddPlanResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AddPlanHandler(
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<AddPlanResponse> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var planTemplate = await _unitOfWork.PlansTemplateRepository.GetByIdAsync(request.Id, true);

            if (planTemplate == null) throw new NotFoundException($"Plan with id {request.Id} has not been found");
            if (planTemplate.AuthorId != _userService.GetCurrentUserId()) throw new ForbiddenException("You are not allowed to add this plan");

            var user = await _userService.GetCurrentMemberAsync(includeFitnessPlans: true);


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
                Archived = false,
                User = user,
                Name = planTemplate.Name,
                Exercises = exercises,
            };

            _unitOfWork.PlansRepository.Add(plan);
            await _unitOfWork.SaveChangesAsync();

            return new AddPlanResponse();
        }
    }
}
