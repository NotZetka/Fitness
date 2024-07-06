namespace API.Data
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public int Repetitions { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
