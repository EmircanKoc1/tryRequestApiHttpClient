using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class TokenService
    {
        IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public Token GetToken()
        {

            return new Token { AccessToken = GetAccessToken(), RefreshToken = GetRefreshToken()  };
        }

        private string GetRefreshToken()
        {

            return Guid.NewGuid().ToString();
        }

        private string GetAccessToken()
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Name, "Emir"), new Claim(ClaimTypes.Role, "Admin") };
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:key"]));
            var signingCredential = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
               claims: claims,
               issuer: _config["jwt:issuer"],
               audience: _config["jwt:audience"],
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["jwt:expire"])),
               signingCredentials: signingCredential
                );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }


    }
}
