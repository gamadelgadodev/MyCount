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
        public async Task<Boolean> GetUser(string UserName, string Password)
        {
            var result = await _context.Users.AnyAsync(x=>x.UserName.Equals(UserName) && x.Password.Equals(Password));
            return result;
        }
    }
}