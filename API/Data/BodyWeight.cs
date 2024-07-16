namespace API.Data
{
    public class BodyWeight
    {
        public int Id { get; set; }
        public int? Height { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public IList<BodyWeightRecord> WeightRecords { get; set; } = new List<BodyWeightRecord>();
    }
}
