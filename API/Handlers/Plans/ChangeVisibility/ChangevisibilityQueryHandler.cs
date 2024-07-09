using API.Data.Repositories.PlansRepository;
using API.Exceptions;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityQueryHandler : IRequestHandler<ChangevisibilityQuery, ChangevisibilityQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IPlansRepository _plansRepository;

        public ChangevisibilityQueryHandler(IUserService userService, IPlansRepository plansRepository)
        {
            _userService = userService;
            _plansRepository = plansRepository;
        }
        public async Task<ChangevisibilityQueryResult> Handle(ChangevisibilityQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();
            var plan = await _plansRepository.GetPlanTemplateByIdAsync(request.TemplateId, false);

            if (plan == null) throw new NotFoundException($"Plan template with id: {request.TemplateId} has not been found");

            if (plan.AuthorId != userId) throw new UnauthorizedException("You are not author of this plan");

            plan.Public = !plan.Public;

            await _plansRepository.SaveChangesAsync();

            return new();
        }
    }
}
