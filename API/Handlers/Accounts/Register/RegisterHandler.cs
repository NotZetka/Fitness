using API.Data;
using API.Data.Repositories;
using API.Exceptions;
using API.Exceptions.Accounts;
using API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Handlers.Accounts.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.UsersRepository.FindUserByUsernamelAsync(request.UserName) != null) throw new ForbiddenException("User already exists");
            if (await _unitOfWork.UsersRepository.FindUserByEmailAsync(request.Email) != null) throw new ForbiddenException("Email already exists");

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

            return new RegisterResponse { Username = user.UserName, Token = token};
        }
    }
}
