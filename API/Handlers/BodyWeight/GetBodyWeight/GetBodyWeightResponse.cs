using API.Data.Dtos;
using API.Utilities;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightResponse
    {
        public bool GenderMale { get; set; }
        public int? Height { get; set; }
        public PagedResult<BodyWeightRecordDto> BodyWeightRecords { get; set; }
    }
}
