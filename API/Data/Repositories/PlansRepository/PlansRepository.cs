
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.PlansRepository
{
    public class PlansRepository : AbstractRepository, IPlansRepository
    {
        private readonly IUserService _userService;

        public PlansRepository(DataContext context, IUserService userService) : base(context)
        {
            _userService = userService;
        }

        public void AddFitnessPlan(FitnessPlan plan)
        {
            _context.FitnessPlans.Add(plan);
        }

        public void AddFitnessPlanTemplate(FitnessPlanTemplate plan)
        {
            _context.FitnessPlanTemplates.Add(plan);
        }

        public async Task<FitnessPlan> GetPlanByIdAsync(int id)
        {
            return await _context.FitnessPlans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<FitnessPlanTemplate> GetPlansTemplateQuery()
        {
            return _context.FitnessPlanTemplates.AsQueryable();
        }

        public async Task<FitnessPlanTemplate> GetPlanTemplateByIdAsync(
            int id,
            bool includeExercises = false,
            bool includeAuthor = false
            )
        {
            var query = _context.FitnessPlanTemplates.AsQueryable();
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
