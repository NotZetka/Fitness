using API.Data;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.Plans.Publish
{
    public class PublishPlanHandler : IRequestHandler<PublishPlanCommand, PublishPlanResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PublishPlanHandler(IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PublishPlanResponse> Handle(PublishPlanCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync();

            var plan = new FitnessPlanTemplate()
            {
                Author = user,
                Name = request.Name,
                Public = request.Public,
                Exercises = request.Exercises.Select(_mapper.Map<ExerciseTemplate>).ToList()
            };

            _unitOfWork.PlansTemplateRepository.Add(plan);

            await _unitOfWork.SaveChangesAsync();

            return new() { Id =  plan.Id };
        }
    }
}
