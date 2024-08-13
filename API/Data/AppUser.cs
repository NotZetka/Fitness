using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public IList<FitnessPlan> FitnessPlans { get; set; }
        public IList<FitnessPlanTemplate> FitnessPlansTemplates { get; set; }

        public IList<Message> MessagesSend { get; set; }
        public IList<Message> MessagesReceived { get; set; }
        public BodyWeight BodyWeight { get; set; } = new();
    }
}
