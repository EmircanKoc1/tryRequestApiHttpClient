using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration config;

        public AuthController(IConfiguration config)
        {
            this.config = config;
        }

        [NonAction]
        public Token GetToken()
        {
            return new TokenService(config).GetToken();
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromForm] UserLoginModel model)
        {
            var user = UserService.Login(model);
            if (user is not null)
            {
                var token = GetToken();
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = DateTime.Now.AddMinutes(5);
                return Ok(token);
            }
            return BadRequest("Kullanıcı Adı şifre hatalı");


        }
        [HttpPost("[action]")]
        public IActionResult RefreshToken([FromForm]string refreshToken)
        {
            User user = UserService.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user is not null && user.RefreshTokenExpireDate < DateTime.Now)
            {
                var token = GetToken();
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = DateTime.Now.AddMinutes(5);
                return Ok(token);   
            }
            else
            {
                return BadRequest();
            }

        }




    }
}
