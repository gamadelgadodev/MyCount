using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Transactions;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly DatabaseContext _context;
        public FinanceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Core.Entities.Transaction>> ListAcc( int accountId, DateTime startDate, DateTime endDate)
        {

            var listTr = await _context.Transactions.Where(x=> x.AccountId==accountId && x.Date >= startDate && x.Date <= endDate).Include(t => t.TransactionCat).ToListAsync();
            return listTr;
        }
    }
}