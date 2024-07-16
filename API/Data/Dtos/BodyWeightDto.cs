namespace API.Data.Dtos
{
    public class BodyWeightDto
    {
        public int? Height { get; set; }
        public IList<BodyWeightRecordDto> WeightRecords { get; set; } = new List<BodyWeightRecordDto>();
    }
}
