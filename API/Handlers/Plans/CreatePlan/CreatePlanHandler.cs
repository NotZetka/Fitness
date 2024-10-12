using API.Data;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.CreatePlan
{
    public class CreatePlanHandler : IRequestHandler<CreatePlanCommand, CreatePlanResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlanHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatePlanResponse> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var plan = new FitnessPlan()
            {
                User = user,
                Name = request.Name,
                Exercises = request.Exercises.Select(_mapper.Map<Exercise>).ToList()
            };

            _unitOfWork.PlansRepository.Add(plan);

            await _unitOfWork.SaveChangesAsync();

            return new() { Id = plan.Id };
        }
    }
}
