using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
namespace e_commerce_api
{
     public class Auth : IJwtAuth
    {
        private readonly string username =  "Ziad";
        private readonly string password = "192837";
        private readonly string key;
        public Auth(string key)
        {
            this.key = key;
        }
        public string Authentication(string username, string password)
        {
            if (!this.username.Equals(username) || !this.password.Equals(password))
            {
                return null;
            }

            //Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //encrypt the key into bytes
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                //Signing credential: private key + algorithm
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}