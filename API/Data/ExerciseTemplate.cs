namespace API.Data
{
    public class ExerciseTemplate
    {
        public int Id { get; set; }
        public int FitnessPlanTemaplteId { get; set; }
        public FitnessPlanTemplate FitnessPlanTemplate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
