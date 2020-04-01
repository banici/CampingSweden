using CampingParkAPI.Data;
using CampingParkAPI.Models;
using CampingParkAPI.Repository.IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CampingParkAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CampingParkDbContext _context;
        private readonly AppSettings _appSettings;

        public UserRepository(CampingParkDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if(user == null)
            {
                return null;
            }

            //if user is found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                    (new SymmetricSecurityKey(key), 
                                         SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public bool isUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
