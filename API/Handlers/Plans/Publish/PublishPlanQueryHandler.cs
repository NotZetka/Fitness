using API.Data;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static API.Data.FitnessPlanTemplate;

namespace API.Handlers.Plans.Publish
{
    public class PublishPlanQueryHandler : IRequestHandler<PublishPlanQuery, PublishPlanQueryResult>
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PublishPlanQueryHandler(DataContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
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

            _context.FitnessPlanTemplates.Add(plan);

            await _context.SaveChangesAsync();

            return new() { Id =  plan.Id };
        }
    }
}
