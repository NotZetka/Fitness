using API.Data;
using API.Data.Repositories.BodyWeightRepository;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.BodyWeight.AddBodyWeightRecord
{
    public class AddBodyWeightRecordQueryHandler : IRequestHandler<AddBodyWeightRecordQuery, AddBodyWeightRecordQueryResponse>
    {
        private readonly IBodyWeightRepository _bodyWeightRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AddBodyWeightRecordQueryHandler(IBodyWeightRepository bodyWeightRepository, IMapper mapper, IUserService userService)
        {
            _bodyWeightRepository = bodyWeightRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<AddBodyWeightRecordQueryResponse> Handle(AddBodyWeightRecordQuery request, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<BodyWeightRecord>(request);

            record.Date = DateTime.Now;

            var userId = _userService.GetCurrentUserId();

            _bodyWeightRepository.AddRecord(userId, record);

            await _bodyWeightRepository.SaveChangesAsync();

            return new AddBodyWeightRecordQueryResponse();
        }
    }
}
