using API.Data.Repositories;
using API.Exceptions;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.EditPlanTemplate;

public class EditPlanTemplateHandler : IRequestHandler<EditPlanTemplateCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public EditPlanTemplateHandler(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }
    public async Task Handle(EditPlanTemplateCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserId();
        var plan = await _unitOfWork.PlansTemplateRepository.GetByIdAsync(request.Id);
        if(plan.AuthorId != userId) throw new ForbiddenException("You do not have permission to edit this plan.");
        
        _unitOfWork.PlansTemplateRepository.Update(request);
        await _unitOfWork.SaveChangesAsync();
    }
}