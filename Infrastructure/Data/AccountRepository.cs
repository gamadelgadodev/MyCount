using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AccountRepository : IAccountRepository
    {
         private readonly DatabaseContext _context;

         public AccountRepository(DatabaseContext context)
         {
            _context = context;
         }
        public async Task<List<Account>> ListAcc(int userId)
        {
            return await _context.Accounts
                .Where(x=>x.UserId==userId && x.IsDeleted == false)
                .ToListAsync();
        }
    }
}