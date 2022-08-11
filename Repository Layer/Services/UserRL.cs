using Database_Layer.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using Repository_Layer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRL : IUserRL
    {
        ProductContext productContext;
        IConfiguration iconfiguration;

        public UserRL(ProductContext productContext, IConfiguration iconfiguration)
        {
            this.productContext = productContext;
            this.iconfiguration = iconfiguration;
        }

        public void Register(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.Name = userPostModel.Name;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                productContext.Users.Add(user);
                productContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var user = productContext.Users.Where(y => y.Email == userLoginModel.Email).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return GenerateJWTToken(user.Email, user.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJWTToken(string email, int UserId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",UserId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
