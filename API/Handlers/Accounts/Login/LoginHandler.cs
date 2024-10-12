using API.Data;
using API.Exceptions;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Handlers.Accounts.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly UserManager<AppMember> _memberManager;
        private readonly UserManager<AppTrainer> _trainerManager;
        private readonly ITokenService _tokenService;

        public LoginHandler(UserManager<AppMember> memberManager, UserManager<AppTrainer> trainerManager, ITokenService tokenService)
        {
            _memberManager = memberManager;
            _trainerManager = trainerManager;
            _tokenService = tokenService;
        }
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            AppUserBase? user = request.UsernameOrEmail.Contains('@') ?
                _memberManager.Users.FirstOrDefault(x=>x.Email.ToLower() == request.UsernameOrEmail.ToLower()) :
                _memberManager.Users.FirstOrDefault(x => x.UserName.ToLower() == request.UsernameOrEmail.ToLower());

            if (user == null)
            {
                user = request.UsernameOrEmail.Contains('@') ?
                _trainerManager.Users.FirstOrDefault(x => x.Email.ToLower() == request.UsernameOrEmail.ToLower()) :
                _trainerManager.Users.FirstOrDefault(x => x.UserName.ToLower() == request.UsernameOrEmail.ToLower());
            }

            if (user == null)
            {
                var message = request.UsernameOrEmail.Contains('@') ?
                    $"user with email: {request.UsernameOrEmail} has not been found" :
                    $"user with username: {request.UsernameOrEmail} has not been found";
                throw new NotFoundException(message);
            }

            var result = false;
            if(user is AppMember) result = await _memberManager.CheckPasswordAsync((AppMember)user, request.Password);
            else if(user is AppTrainer) result = await _trainerManager.CheckPasswordAsync((AppTrainer)user, request.Password);

            if (!result) throw new UnauthorizedException("invalid password");

            var token = await _tokenService.CreateToken(user);

            return new LoginResponse { Token = token };
        }
    }
}
