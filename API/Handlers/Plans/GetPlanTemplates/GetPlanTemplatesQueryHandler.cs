using API.Data;
using API.Data.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesQueryHandler : IRequestHandler<GetPlanTemplatesQuery, GetPlanTemplatesQueryResult>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetPlanTemplatesQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetPlanTemplatesQueryResult> Handle(GetPlanTemplatesQuery request, CancellationToken cancellationToken)
        {
            var plans = await _context.FitnessPlanTemplates
                .Include(x => x.Author)
                .Include(x => x.Exercises)
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
