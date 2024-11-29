using API.Data.Dtos;
using API.Data.Repositories;
using API.Services;
using API.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Plans.GetYourTemplates;

public class GetYourTemplatesHandler : IRequestHandler<GetYourTemplatesQuery, PagedResult<FitnessPlanTemplateDto>>
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public GetYourTemplatesHandler(IUserService userService, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PagedResult<FitnessPlanTemplateDto>> Handle(GetYourTemplatesQuery request, CancellationToken cancellationToken)
    {
        var trainer = await _userService.GetCurrentTrainerAsync();
        
        var query = _unitOfWork.PlansTemplateRepository.GetTrainerTemplatesQuery(trainer.Id);

        if (request.PageSize.HasValue)
        {
            return await PagedResult<FitnessPlanTemplateDto>.CreateFromQueryAsync(query, request.PageNumber, request.PageSize.Value);
        }

        return new PagedResult<FitnessPlanTemplateDto>(await query.ToListAsync());
    }
}