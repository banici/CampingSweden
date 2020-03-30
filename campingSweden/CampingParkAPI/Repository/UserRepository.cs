using CampingParkAPI.Data;
using CampingParkAPI.Models;
using CampingParkAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CampingParkDbContext _context;

        public UserRepository(CampingParkDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
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
