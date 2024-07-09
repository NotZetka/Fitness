using API.Data;
using API.Data.Repositories.PlansRepository;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.Publish
{
    public class PublishPlanQueryHandler : IRequestHandler<PublishPlanQuery, PublishPlanQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPlansRepository _plansRepository;

        public PublishPlanQueryHandler(IUserService userService, IMapper mapper, IPlansRepository plansRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _plansRepository = plansRepository;
        }

        public async Task<PublishPlanQueryResult> Handle(PublishPlanQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var plan = new FitnessPlanTemplate()
            {
                Author = user,
                Name = request.Name,
                Public = request.Public,
                Exercises = request.Exercises.Select(_mapper.Map<ExerciseTemplate>).ToList()
            };

            _plansRepository.AddFitnessPlanTemplate(plan);

            await _plansRepository.SaveChangesAsync();

            return new() { Id =  plan.Id };
        }
    }
}
