namespace API.Data.Dtos
{
    public class FitnessPlanTemplateDto
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public bool Public { get; set; }
        public IEnumerable<ExerciseTemplateDto> Exercises { get; set; }
        public int PlanId { get; set; }
    }
}
