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
        private readonly DbSet<AppUserBase> _dbSet;

        public UsersRepository(DataContext context, IMapper mapper, IUserService userService)
        {
            _configurationProvider = mapper.ConfigurationProvider;
            _userService = userService;
            _dbSet = context.Users;
        }

        public async Task<AppUserBase> FindUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == email.ToLower());
        }

        public async Task<AppUserBase> FindUserByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.Id == id);
        }

        public async Task<AppUserBase> FindUserByUsernamelAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(x =>
                x.UserName.ToLower() == username.ToLower());
        }

        public async Task<PagedResult<UserDto>> GetUsersListAsync(int? pageNumber = null, int? pageSize = null, string role = null)
        {
            var query = _dbSet
                .Where(x => x.Id != _userService.GetCurrentUserId())
                .ProjectTo<UserDto>(_configurationProvider);

            if(role != null) query = query.Where(x => x.Role == role);

            if(pageNumber != null && pageSize != null)
            {
                return await PagedResult<UserDto>.CreateFromQueryAsync(query, pageNumber.Value, pageSize.Value);
            }

            return new PagedResult<UserDto>(await query.ToListAsync());
        }
    }
}
