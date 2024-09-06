using API.Data.Dtos;
using API.Services;
using API.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AutoMapper.IConfigurationProvider _configurationProvider;
        private readonly IUserService _userService;
        private readonly DbSet<AppUser> _dbSet;

        public UsersRepository(DataContext context, IMapper mapper, IUserService userService)
        {
            _configurationProvider = mapper.ConfigurationProvider;
            _userService = userService;
            _dbSet = context.Users;
        }

        public async Task<AppUser> FindUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
        }

        public async Task<AppUser> FindUserByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.Id == id);
        }

        public async Task<AppUser> FindUserByUsernamelAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.UserName.ToLower() == username.ToLower());
        }

        public async Task<PagedResult<UserDto>> GetUsersListAsync(int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbSet
                .Where(x => x.Id != _userService.GetCurrentUserId())
                .ProjectTo<UserDto>(_configurationProvider);

            if(pageNumber != null && pageSize != null)
            {
                return await PagedResult<UserDto>.CreateFromQueryAsync(query, pageNumber.Value, pageSize.Value);
            }

            return new PagedResult<UserDto>(await query.ToListAsync());
        }
    }
}
