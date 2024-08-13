using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class PlansTemplateRepository(DataContext context) : AbstractRepository<FitnessPlanTemplate>(context), IPlansTemplateRepository
    {
        public IQueryable<FitnessPlanTemplate> GetPlansTemplateQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<FitnessPlanTemplate> GetByIdAsync(
            int id,
            bool includeExercises = false,
            bool includeAuthor = false
            )
        {
            var query = _dbSet.AsQueryable();
            if (includeExercises)
            {
                query = query.Include(x => x.Exercises);
            }
            if (includeAuthor)
            {
                query = query.Include(x => x.Author);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
