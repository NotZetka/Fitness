namespace API.Data
{
    public class FitnessPlan : DbEntity
    {

        public int TemplateId { get; set; }
        public bool Archived { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string Name { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
