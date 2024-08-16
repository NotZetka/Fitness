using API.Data.Dtos;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesHandler : IRequestHandler<GetPlanTemplatesQuery, GetPlanTemplatesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public GetPlanTemplatesHandler(IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<GetPlanTemplatesResponse> Handle(GetPlanTemplatesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans:true);
            var userPlanIds = user.FitnessPlans.Select(x => x.TemplateId);

            var plans = await _unitOfWork.PlansTemplateRepository.GetPlansTemplateQuery()
                .Include(x => x.Author)
                .Include(x => x.Exercises)
                .Where(x=>!userPlanIds.Contains(x.Id) && x.Public)
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
