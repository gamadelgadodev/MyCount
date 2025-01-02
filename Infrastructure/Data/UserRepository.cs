using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
         private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsername(string UserName,string Password)
        {
            var result = await _context.Users.Where(x=>x.UserName.Equals(UserName) && x.Password.Equals(Password)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Boolean> TrustUser(string UserName, string Password)
        {
            var result = await _context.Users.AnyAsync(x=>x.UserName.Equals(UserName) && x.Password.Equals(Password));
            return result;
        }
    }
}