using API.Data;
using API.Data.Repositories;
using API.Exceptions;
using API.Exceptions.Accounts;
using API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Handlers.Accounts.Register.Member
{
    public class RegisterMemberHandler : IRequestHandler<RegisterMemberCommand, RegisterResponse>
    {
        private readonly UserManager<AppMember> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterMemberCommand> _validator;

        public RegisterMemberHandler(UserManager<AppMember> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IValidator<RegisterMemberCommand> validator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<RegisterResponse> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (await _unitOfWork.UsersRepository.FindUserByUsernamelAsync(request.UserName) != null) throw new ForbiddenException("User already exists");
            if (await _unitOfWork.UsersRepository.FindUserByEmailAsync(request.Email) != null) throw new ForbiddenException("Email already exists");

            var user = new AppMember()
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

            return new RegisterResponse { Token = token };
        }
    }
}
