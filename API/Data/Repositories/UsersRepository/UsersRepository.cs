using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.UsersRepository
{
    public class UsersRepository : AbstractRepository, IUsersRepository
    {
        public UsersRepository(DataContext context) : base(context) { }

        public async Task<AppUser> FindUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
        }
        public async Task<AppUser> FindUserByUsernamelAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName.ToLower() == username.ToLower());
        }

    }
}
