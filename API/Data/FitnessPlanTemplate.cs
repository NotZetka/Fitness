namespace API.Data
{
    public class FitnessPlanTemplate : DbEntity
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public AppTrainer Author { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ExerciseTemplate> Exercises { get; set; } = new List<ExerciseTemplate>();

    }
}