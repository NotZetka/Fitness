using API.Database;
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
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public RegisterHandler(DataContext context, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<RegisterQueryResult> Handle(RegisterQuery request, CancellationToken cancellationToken)
        {
            if (await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName.ToLower() == request.UserName.ToLower()) != null) throw new UsernameAlreadyExistsException();
            if (await _context.Users.FirstOrDefaultAsync(x =>
                x.Email.ToLower() == request.Email.ToLower()) != null) throw new EmailAlreadyExistsException();

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
