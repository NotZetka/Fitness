using API.Data.Dtos;
using API.Handlers.Plans.EditPlanTemplate;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class PlansTemplateRepository(DataContext context, IMapper mapper) : AbstractRepository<FitnessPlanTemplate>(context), IPlansTemplateRepository
    {
        public IQueryable<FitnessPlanTemplate> GetPlansTemplateQuery()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<FitnessPlanTemplateDto> GetTrainerTemplatesQuery(int trainerId)
        {
            return _dbSet
                .Where(x => x.AuthorId == trainerId)
                .Include(x=>x.Author)
                .ProjectTo<FitnessPlanTemplateDto>(mapper.ConfigurationProvider);
        }

        public void Update(EditPlanTemplateCommand model)
        {
            var plan = _dbSet.FirstOrDefault(x=>x.Id == model.Id);
            if (plan == null) return;
            plan.Name = model.Name;
            plan.Price = model.Price;
            plan.Public = model.Public;
            plan.Exercises = model.Exercises.Select(x=>mapper.Map<ExerciseTemplate>(x)).ToList();
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
