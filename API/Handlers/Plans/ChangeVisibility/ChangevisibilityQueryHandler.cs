using API.Data;
using API.Exceptions;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityQueryHandler : IRequestHandler<ChangevisibilityQuery, ChangevisibilityQueryResult>
    {
        private readonly IUserService _userService;
        private readonly DataContext _context;

        public ChangevisibilityQueryHandler(IUserService userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }
        public async Task<ChangevisibilityQueryResult> Handle(ChangevisibilityQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();
            var plan = _context.FitnessPlanTemplates.FirstOrDefault(x => x.Id == request.TemplateId);

            if (plan == null) throw new NotFoundException($"Plan template with id: {request.TemplateId} has not been found");

            if (plan.AuthorId != userId) throw new UnauthorizedException("You are not author of this plan");

            plan.Public = !plan.Public;

            await _context.SaveChangesAsync();

            return new();
        }
    }
}
