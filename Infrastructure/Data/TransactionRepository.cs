using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly DatabaseContext _context;
        public TransactionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> ListPage(int page, int pageSize, int accountId)
        {
           return await _context.Transactions
            .Where(x => x.AccountId == accountId)
            .OrderByDescending(x => x.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<Transaction> CreateTransB(Transaction tras)
        {
            var  balance = await _context.Transactions
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Date)
                .Select(x => x.Balance)
                .FirstOrDefaultAsync();
            if(tras.typeTransaction.Equals("income"))
            {
                balance += tras.Value; 
                Console.WriteLine("hello entro al if");
            }
            else
            {
                balance -= tras.Value; 
                Console.WriteLine("hello entro al else");
                
            }
            Console.WriteLine(balance);

            tras.Balance = balance;
            await _context.Transactions.AddAsync(tras);
            await _context.SaveChangesAsync();
            return tras;
        }
    }
}