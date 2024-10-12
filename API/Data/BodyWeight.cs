namespace API.Data
{
    public class BodyWeight : DbEntity
    {
        public int? Height { get; set; }
        public int UserId { get; set; }
        public AppMember User { get; set; }
        public IList<BodyWeightRecord> WeightRecords { get; set; } = new List<BodyWeightRecord>();
    }
}
