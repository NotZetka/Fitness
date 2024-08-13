using API.Data;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.BodyWeight.AddBodyWeightRecord
{
    public class AddBodyWeightRecordQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IUserService _userService) 
        : IRequestHandler<AddBodyWeightRecordQuery, AddBodyWeightRecordQueryResponse>
    {
        public async Task<AddBodyWeightRecordQueryResponse> Handle(AddBodyWeightRecordQuery request, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<BodyWeightRecord>(request);

            record.Date = DateTime.Now;

            var userId = _userService.GetCurrentUserId();

            _unitOfWork.BodyWeightRepository.AddRecord(userId, record);

            await _unitOfWork.SaveChangesAsync();

            return new AddBodyWeightRecordQueryResponse();
        }
    }
}
