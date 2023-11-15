using System;

namespace WebApplication1.Model
{
    public class User
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }

    }
}
