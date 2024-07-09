namespace API.Data.Repositories.PlansRepository
{
    public interface IPlansRepository : IRepository
    {
        Task<FitnessPlanTemplate> GetPlanTemplateByIdAsync(
            int id,
            bool includeExercises = false,
            bool includeAuthor = false
            );
        Task<FitnessPlan> GetPlanByIdAsync(int id);
        IQueryable<FitnessPlanTemplate> GetPlansTemplateQuery();
        void AddFitnessPlan(FitnessPlan plan);
        void AddFitnessPlanTemplate(FitnessPlanTemplate plan);
    }
}
