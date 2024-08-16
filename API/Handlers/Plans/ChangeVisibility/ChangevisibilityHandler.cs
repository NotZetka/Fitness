using API.Data.Repositories;
using API.Exceptions;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityHandler : IRequestHandler<ChangevisibilityCommand, ChangevisibilityResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public ChangevisibilityHandler(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ChangevisibilityResponse> Handle(ChangevisibilityCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetCurrentUserId();
            var plan = await _unitOfWork.PlansTemplateRepository.GetByIdAsync(request.TemplateId, false);

            if (plan == null) throw new NotFoundException($"Plan template with id: {request.TemplateId} has not been found");

            if (plan.AuthorId != userId) throw new UnauthorizedException("You are not author of this plan");

            plan.Public = !plan.Public;

            await _unitOfWork.SaveChangesAsync();

            return new();
        }
    }
}
