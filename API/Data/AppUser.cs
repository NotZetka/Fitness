using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public IEnumerable<FitnessPlan> FitnessPlans { get; set; }
        public IEnumerable<FitnessPlanTemplate> FitnessPlansTemplates { get; set; }

        public IEnumerable<Message> MessagesSend { get; set; }
        public IEnumerable<Message> MessagesReceived { get; set; }
    }
}
