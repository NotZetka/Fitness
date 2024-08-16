using MediatR;

namespace API.Handlers.Plans.AddRecord
{
    public class AddRecordsCommand : IRequest<AddRecordsResponse>
    {
        public IEnumerable<RecordEntity> Records { get; set; }
        public class RecordEntity
        {
            public int ExerciseId { get; set; }
            public int Weight { get; set; }
            public int Repetitions { get; set; }
        }
    }
    
}
