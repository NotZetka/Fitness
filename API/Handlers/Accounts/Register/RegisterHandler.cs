using API.Database;
using API.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Accounts.Register
{
    public class RegisterHandler : IRequestHandler<RegisterQuery, RegisterQueryResult>
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RegisterHandler(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                DateOfBirth = request.DateOfBirth,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new IdentityException(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) throw new IdentityException(roleResult.Errors);

            return new RegisterQueryResult();
        }
    }
}
