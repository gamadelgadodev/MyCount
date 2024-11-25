using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> ListPage(int page,int pageSize,int accountId);
        // Task<Transaction> (Transaction entity);
        Task<Transaction> CreateTransB(Transaction tras);
        Task<Transaction> UpdateTrans(Transaction tras, decimal value);
        Task<Transaction> DelteTrans(int id);
    }
}