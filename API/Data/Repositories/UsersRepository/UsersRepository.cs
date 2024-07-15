using API.Data.Dtos;
using API.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.UsersRepository
{
    public class UsersRepository : AbstractRepository, IUsersRepository
    {
        private readonly AutoMapper.IConfigurationProvider _configurationProvider;
        private readonly IUserService _userService;

        public UsersRepository(DataContext context, IMapper mapper, IUserService userService) : base(context) {
            _configurationProvider = mapper.ConfigurationProvider;
            _userService = userService;
        }

        public async Task<AppUser> FindUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
        }

        public async Task<AppUser> FindUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.Id == id);
        }

        public async Task<AppUser> FindUserByUsernamelAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<UserDto>> GetUsersListAsync()
        {
            return await _context.Users
                .Where(x => x.Id != _userService.GetCurrentUserId())
                .ProjectTo<UserDto>(_configurationProvider)
                .ToListAsync();
        }

    }
}
