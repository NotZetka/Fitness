using API.Data.Dtos;
using API.Database;
using API.Exceptions.Accounts;
using API.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Handlers.Accounts.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public LoginHandler(DataContext context, UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = request.UsernameOrEmail.Contains('@') ?
                _userManager.Users.FirstOrDefault(x=>x.Email.ToLower() == request.UsernameOrEmail.ToLower()) :
                _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == request.UsernameOrEmail.ToLower());

            if (user == null) throw new UserNotFountException(request.UsernameOrEmail);

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result) throw new InvalidPasswordException();

            var token = await _tokenService.CreateToken(user);

            return new LoginQueryResult { Username = user.UserName!, Token = token };
        }
    }
}
