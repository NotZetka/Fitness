using API.Data.Repositories;
using API.Data;
using API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using API.Exceptions.Accounts;
using API.Exceptions;
using API.Utilities.Static;
using FluentValidation;

namespace API.Handlers.Accounts.Register.Trainer
{
    public class RegisterTrainerHandler : IRequestHandler<RegisterTrainerCommand, RegisterResponse>
    {
        private readonly UserManager<AppTrainer> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterTrainerCommand> _validator;

        public RegisterTrainerHandler(
            UserManager<AppTrainer> userManager,
            ITokenService tokenService, 
            IUnitOfWork unitOfWork,
            IValidator<RegisterTrainerCommand> validator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<RegisterResponse> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (await _unitOfWork.UsersRepository.FindUserByUsernamelAsync(request.UserName) != null) throw new ForbiddenException("User already exists");
            if (await _unitOfWork.UsersRepository.FindUserByEmailAsync(request.Email) != null) throw new ForbiddenException("Email already exists");

            var user = new AppTrainer()
            {
                UserName = request.UserName,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new IdentityException(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.Trainer);

            if (!roleResult.Succeeded) throw new IdentityException(roleResult.Errors);

            var token = await _tokenService.CreateToken(user);

            return new RegisterResponse { Token = token };
        }
    }
}
