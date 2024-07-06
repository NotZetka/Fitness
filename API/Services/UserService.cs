using API.Data;
using API.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Services
{
    public interface IUserService
    {
        public Task<AppUser> GetCurrentUserAsync(
            bool includeFitnessPlans = false,
            bool includeExercises = false,
            bool includeRecords = false);

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

        public async Task<AppUser> GetCurrentUserAsync(
            bool includeFitnessPlans = false,
            bool includeExercises = false,
            bool includeRecords = false)
        {
            var userid = GetCurrentUserId();
            var query = _context.Users.Where(x => x.Id == userid);

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

        public int GetCurrentUserId()
        {
            var user = _httpContext.User;
            var id = user.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value;
            if (id == null) throw new NotFoundException("User not found");

            return int.Parse(id);
        }
    }
}
