using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AppMember : AppUserBase
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public IList<FitnessPlan> FitnessPlans { get; set; }
        public BodyWeight BodyWeight { get; set; } = new();
    }
}
