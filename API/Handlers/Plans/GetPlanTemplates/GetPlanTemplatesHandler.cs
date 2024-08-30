using API.Data.Dtos;
using API.Data.Repositories;
using API.Services;
using API.Utilities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesHandler : IRequestHandler<GetPlanTemplatesQuery, PagedResult<FitnessPlanTemplateDto>>
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
        public async Task<PagedResult<FitnessPlanTemplateDto>> Handle(GetPlanTemplatesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeFitnessPlans:true);
            var userPlanIds = user.FitnessPlans.Select(x => x.TemplateId);

            var query = _unitOfWork.PlansTemplateRepository.GetPlansTemplateQuery()
                .Include(x => x.Author)
                .Include(x => x.Exercises)
                .Where(x => !userPlanIds.Contains(x.Id) && x.Public)
                .Select(x =>
                    new FitnessPlanTemplateDto()
                    {
                        Name = x.Name,
                        AuthorName = x.Author.UserName,
                        Exercises = x.Exercises.Select(e => _mapper.Map<ExerciseTemplateDto>(e)),
                        PlanId = x.Id,
                    });

            if(!request.PageNumber.HasValue) request.PageNumber = 1;
            if (!request.PageSize.HasValue) request.PageSize = query.Count();

            var plans = await PagedResult<FitnessPlanTemplateDto>.CreateFromQueryAsync(query, request.PageNumber.Value, request.PageSize.Value);

            return plans;
        }
    }
}
