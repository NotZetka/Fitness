using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(IdentityUser<int> user);
    }

    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppMember> _memberManager;
        private readonly UserManager<AppTrainer> _trainerManager;

        public TokenService(
            IConfiguration config,
            UserManager<AppMember> memberManager,
            UserManager<AppTrainer> trainerManager
            )
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _memberManager = memberManager;
            _trainerManager = trainerManager;
        }
        public async Task<string> CreateToken(IdentityUser<int> user)
        {

            var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                };

            if (user is AppMember) 
            {
                var roles = await _memberManager.GetRolesAsync((AppMember)user);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            else if (user is AppTrainer)
            {
                var roles = await _trainerManager.GetRolesAsync((AppTrainer)user);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
