using API.Data;
using API.Services;
using MediatR;

namespace API.Handlers.Plans.AddRecord
{
    public class AddRecordsQueryHandler : IRequestHandler<AddRecordsQuery, AddRecordsQueryResult>
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public AddRecordsQueryHandler(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<AddRecordsQueryResult> Handle(AddRecordsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUserAsync(includeRecords: true);

            var exercises = user.FitnessPlans.SelectMany(x => x.Exercises).ToList();

            foreach (var exercise in exercises)
            {
                var record = request.Records.FirstOrDefault(x => x.RecordId == exercise.Id);
                if (record != null) exercise.Records.Add(new Record { 
                    Date = DateTime.Today,
                    Weight = record.Weight,
                    Repetitions = record.Repetitions
                });
            }

            await _context.SaveChangesAsync();

            return new AddRecordsQueryResult();
        }
    }
}
