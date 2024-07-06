namespace API.Data
{
    public class FitnessPlanTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public bool Public { get; set; }
        public IEnumerable<ExerciseTemplate> Exercises { get; set; } = new List<ExerciseTemplate>();

    }
}