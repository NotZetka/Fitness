using API.Data;
using API.Data.Dtos;

namespace API.Handlers.Plans.GetPlans
{
    public class GetPlansQueryResult
    {
        public IEnumerable<FitnessPlanDto>? Plans { get; set; }
    }
}
