namespace API.Data.Dtos
{
    public class ExerciseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<RecordDto>? Records { get; set; }
    }
}
