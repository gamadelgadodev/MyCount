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
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCat> transactionCats { get; set; }
        public DbSet<Recurrent> Recurrents { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}