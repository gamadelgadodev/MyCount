using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class DatabaseContext :  DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<IncomeCat> IncomeCats { get; set; }
        public DbSet<ExpenseCat> ExpenseCats { get; set; }
        public DbSet<Recurrent> Recurrents { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}