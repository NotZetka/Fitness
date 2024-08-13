﻿using API.Data.Dtos;
using API.Services;
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

        public async Task<IEnumerable<UserDto>> GetUsersListAsync()
        {
            return await _dbSet
                .Where(x => x.Id != _userService.GetCurrentUserId())
                .ProjectTo<UserDto>(_configurationProvider)
                .ToListAsync();
        }
    }
}