using API.Data;
using API.Data.Repositories;
using API.Services;
using AutoMapper;
using MediatR;

namespace API.Handlers.BodyWeight.AddBodyWeightRecord
{
    public class AddBodyWeightRecordHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IUserService _userService) 
        : IRequestHandler<AddBodyWeightRecordCommand, AddBodyWeightRecordResponse>
    {
        public async Task<AddBodyWeightRecordResponse> Handle(AddBodyWeightRecordCommand request, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<BodyWeightRecord>(request);

            record.Date = DateTime.Now;

            var userId = _userService.GetCurrentUserId();

            _unitOfWork.BodyWeightRepository.AddRecord(userId, record);

            await _unitOfWork.SaveChangesAsync();

            return new AddBodyWeightRecordResponse();
        }
    }
}
