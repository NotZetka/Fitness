using API.Data;
using API.Data.Dtos;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesQueryHandler : IRequestHandler<GetPlanTemplatesQuery, GetPlanTemplatesQueryResult>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetPlanTemplatesQueryHandler(DataContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<GetPlanTemplatesQueryResult> Handle(GetPlanTemplatesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans:true);
            var userPlanIds = user.FitnessPlans.Select(x => x.TemplateId);

            var plans = await _context.FitnessPlanTemplates
                .Include(x => x.Author)
                .Include(x => x.Exercises)
                .Where(x=>!userPlanIds.Contains(x.Id))
                .Select(x =>
                    new FitnessPlanTemplateDto()
                    {
                        Name = x.Name,
                        AuthorName = x.Author.UserName,
                        Exercises = x.Exercises.Select(e => _mapper.Map<ExerciseTemplateDto>(e)),
                        PlanId = x.Id,
                    })
                .ToListAsync();

            return new() { Plans = plans };
        }
    }
}
