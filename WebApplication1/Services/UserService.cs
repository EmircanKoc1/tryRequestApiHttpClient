using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class UserService
    {


        public static List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Kullanıcı1",
                Username ="user1",
                Password = "123",
                RefreshToken =null,
                RefreshTokenExpireDate= System.DateTime.Now
            },
             new User
            {
                Id = 2,
                Name = "Kullanıcı2",
                Username ="user2",
                Password = "123",
                RefreshToken =null,
                RefreshTokenExpireDate= System.DateTime.Now
            },
              new User
            {
                Id = 1,
                Name = "Kullanıcı3",
                Username ="user3",
                Password = "123",
                RefreshToken =null,
                RefreshTokenExpireDate= System.DateTime.Now
            },
               new User
            {
                Id = 4,
                Name = "Kullanıcı4",
                Username ="user4",
                Password = "123",
                RefreshToken =null,
                RefreshTokenExpireDate= System.DateTime.Now
            },

        };

        public static User Login(UserLoginModel model)
        {

            return Users.FirstOrDefault(u=>u.Password == model.Password && u.Username==model.Username);

        }
    }
}
