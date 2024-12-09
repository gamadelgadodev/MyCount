using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data.DTOs;
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
            var account = await _context.Accounts
                    .Where(x => x.Id == tras.AccountId)
                    .FirstOrDefaultAsync();
            var balance = account.Balance;
            if (tras.typeTransaction.Equals("income"))
            {
                balance += tras.Value;
            }
            else
            {
                balance -= tras.Value;

            }

            tras.Balance = balance;
            account.Balance = balance;
            await _context.Transactions.AddAsync(tras);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return tras;
        }

        public async Task<Transaction> UpdateTrans(Transaction tras, decimal value)
        {
            var oldTras = await _context.Transactions
                                .Where(x => x.Id == tras.Id)
                                .FirstOrDefaultAsync();
            var account = await _context.Accounts
                    .Where(x => x.Id == tras.AccountId)
                    .FirstOrDefaultAsync();
            var balance = account.Balance;
            if (oldTras.typeTransaction.Equals("income"))
            {
                account.Balance -= value;
                account.Balance += tras.Value;
            }
            else
            {
                account.Balance += value;
                account.Balance -= tras.Value;
            }
            _context.Transactions.Update(tras);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return tras;
        }

        public async Task<Transaction> DelteTrans(int id)
        {
            var oldTras = await _context.Transactions
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();
            var account = await _context.Accounts
                    .Where(x => x.Id == oldTras.AccountId)
                    .FirstOrDefaultAsync();
            var balance = account.Balance;
            if (oldTras.typeTransaction.Equals("income"))
            {
                account.Balance -= oldTras.Value;
            }
            else
            {
                account.Balance += oldTras.Value;
            }
            oldTras.IsDeleted = true;
            await _context.SaveChangesAsync();
            return oldTras;
        }

        public async Task<List<Transaction>> ListByFilter(decimal? Value, string? Description, string? Cat, DateTime? Date, string? RangeDate, int accountId, int page)
        {
            // Inicializamos la consulta base
            var query = _context.Transactions
                .Where(x => x.AccountId == accountId)
                .OrderByDescending(x => x.Date)
                .AsQueryable();

            // Filtrar por Value, considerando tolerancia decimal
            if (Value.HasValue)
            {
                query = query.Where(x => Math.Abs(x.Value - Value.Value) < 0.01m);
            }

            // Filtrar por Description (insensible a mayúsculas y minúsculas)
            if (!string.IsNullOrWhiteSpace(Description))
            {
                query = query.Where(x => x.Description.ToLower().Contains(Description.ToLower()));
            }

            // Filtrar por categoría
            if (!string.IsNullOrWhiteSpace(Cat))
            {
                query = query.Where(x => x.TransactionCat.Name == Cat);
            }

            // Filtrar por una fecha específica
            if (Date.HasValue)
            {
                query = query.Where(x => x.Date.Date == Date.Value.Date);
            }

            // Filtrar por rango de fechas
            if (!string.IsNullOrWhiteSpace(RangeDate))
            {
                var dates = RangeDate.Split(':');
                if (dates.Length == 2
                    && DateTime.TryParse(dates[0], out var startDate)
                    && DateTime.TryParse(dates[1], out var endDate))
                {
                    query = query.Where(x => x.Date >= startDate && x.Date <= endDate);
                }
            }

            // Aplicar paginación
            const int pageSize = 5;
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}