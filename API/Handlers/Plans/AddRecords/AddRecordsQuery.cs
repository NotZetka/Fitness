using MediatR;

namespace API.Handlers.Plans.AddRecord
{
    public class AddRecordsQuery : IRequest<AddRecordsQueryResult>
    {
        public IEnumerable<QueryRecord> Records { get; set; }
        public class QueryRecord
        {
            public int RecordId { get; set; }
            public int Weight { get; set; }
            public int Repetitions { get; set; }
        }
    }
    
}
