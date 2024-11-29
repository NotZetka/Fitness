using API.Data;
using API.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public interface IUserService
    {
        public Task<AppMember> GetCurrentMemberAsync(
            bool includeFitnessPlans = false,
            bool includeExercises = false,
            bool includeRecords = false);

        public Task<AppUserBase> GetCurrentUserAsync();
        public Task<AppTrainer> GetCurrentTrainerAsync();

        public int GetCurrentUserId();
    }
    public class UserService : IUserService
    {
        private readonly HttpContext _httpContext;
        private readonly DataContext _context;

        public UserService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _context = context;
        }

        public async Task<AppMember> GetCurrentMemberAsync(
            bool includeFitnessPlans = false,
            bool includeExercises = false,
            bool includeRecords = false)
        {
            var userid = GetCurrentUserId();
            var query = _context.Members.Where(x => x.Id == userid);

            if (includeRecords)
            {
                query = query.Include(x => x.FitnessPlans).ThenInclude(x => x.Exercises).ThenInclude(x=>x.Records);
            }
            else if (includeExercises)
            {
                query = query.Include(x => x.FitnessPlans).ThenInclude(x => x.Exercises);
            }
            else if (includeFitnessPlans)
            {
                query = query.Include(x=>x.FitnessPlans);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<AppUserBase> GetCurrentUserAsync()
        {
            var userid = GetCurrentUserId();
            return await _context.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();
        }

        public async Task<AppTrainer> GetCurrentTrainerAsync()
        {
            var userid = GetCurrentUserId();
            return await _context.Trainers.Where(x => x.Id == userid).FirstOrDefaultAsync();
        }

        public int GetCurrentUserId()
        {
            var user = _httpContext.User;

            return user.GetUserId();
        }
    }
}
