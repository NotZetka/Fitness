using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AppTrainer : AppUserBase
    {
        public IList<FitnessPlanTemplate> FitnessPlansTemplates { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
