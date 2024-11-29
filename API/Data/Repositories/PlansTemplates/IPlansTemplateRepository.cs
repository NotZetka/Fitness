using API.Data.Dtos;
using API.Handlers.Plans.EditPlanTemplate;

namespace API.Data.Repositories
{
    public interface IPlansTemplateRepository : IRepository<FitnessPlanTemplate>
    {
        Task<FitnessPlanTemplate> GetByIdAsync(
            int id,
            bool includeExercises = false,
            bool includeAuthor = false
            );
        IQueryable<FitnessPlanTemplate> GetPlansTemplateQuery();
        IQueryable<FitnessPlanTemplateDto> GetTrainerTemplatesQuery(int trainerId);
        void Update(EditPlanTemplateCommand model);
    }
}
