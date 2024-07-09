using API.Data;
using API.Data.Repositories.UsersRepository;
using API.Exceptions;
using API.Exceptions.Accounts;
using API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Accounts.Register
{
    public class RegisterHandler : IRequestHandler<RegisterQuery, RegisterQueryResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUsersRepository _accountsRepository;

        public RegisterHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUsersRepository accountsRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _accountsRepository = accountsRepository;
        }
        public async Task<RegisterQueryResult> Handle(RegisterQuery request, CancellationToken cancellationToken)
        {
            if (await _accountsRepository.FindUserByUsernamelAsync(request.UserName) != null) throw new ForbiddenException("User already exists");
            if (await _accountsRepository.FindUserByEmailAsync(request.Email) != null) throw new ForbiddenException("Email already exists");

            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth.Value,
                Gender = request.Gender,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new IdentityException(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) throw new IdentityException(roleResult.Errors);

            var token = await _tokenService.CreateToken(user);

            return new RegisterQueryResult { Username = user.UserName, Token = token};
        }
    }
}
