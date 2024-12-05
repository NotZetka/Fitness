using API.Data;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.CreatePlanTemplate
{
    public class CreatePlanTemplateHandler : IRequestHandler<CreatePlanTemplateCommand, CreatePlanTemplateResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlanTemplateHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreatePlanTemplateResponse> Handle(CreatePlanTemplateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentTrainerAsync();
            var fitenesssPlanTemplate = new FitnessPlanTemplate()
            {
                Name = request.Name,
                Author = user,
                Price = request.Price,
                Public = request.Public,
                Exercises = request.Exercises.Select(_mapper.Map<ExerciseTemplate>).ToList()  
            };
            
            _unitOfWork.PlansTemplateRepository.Add(fitenesssPlanTemplate);
            await _unitOfWork.SaveChangesAsync();

            return new() { Id = fitenesssPlanTemplate.Id };
        }
    }
}
