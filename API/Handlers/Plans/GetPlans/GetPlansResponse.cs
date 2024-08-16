using API.Data;
using API.Data.Dtos;

namespace API.Handlers.Plans.GetPlans
{
    public class GetPlansResponse
    {
        public IEnumerable<FitnessPlanDto>? Plans { get; set; }
    }
}
