using API.Data;
using API.Exceptions;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Handlers.Accounts.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginQueryHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = request.UsernameOrEmail.Contains('@') ?
                _userManager.Users.FirstOrDefault(x=>x.Email.ToLower() == request.UsernameOrEmail.ToLower()) :
                _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == request.UsernameOrEmail.ToLower());

            if (user == null)
            {
                var message = request.UsernameOrEmail.Contains('@') ?
                    $"user with email: {request.UsernameOrEmail} has not been found" :
                    $"user with username: {request.UsernameOrEmail} has not been found";
                throw new NotFoundException(message);
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result) throw new UnauthorizedException("invalid password");

            var token = await _tokenService.CreateToken(user);

            return new LoginQueryResult { Username = user.UserName!, Token = token };
        }
    }
}
