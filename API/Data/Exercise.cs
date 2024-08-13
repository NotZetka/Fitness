namespace API.Data
{
    public class Exercise : DbEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int FitnessPlanId { get; set; }
        public FitnessPlan FitnessPlan { get; set; }
        public IList<Record> Records { get; set; }
    }
}
