namespace API.Data.Dtos
{
    public class FitnessPlanDto
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExerciseDto>? Exercises { get; set; }
    }
}
