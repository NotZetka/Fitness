using API.Data.Dtos;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightResponse
    {
        public bool GenderMale { get; set; }
        public BodyWeightDto BodyWeight { get; set; }
    }
}
