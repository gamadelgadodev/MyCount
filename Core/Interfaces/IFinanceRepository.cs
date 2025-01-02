using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Core.Interfaces
{
    public interface IFinanceRepository
    {
        Task<List<Core.Entities.Transaction>> ListAcc(int accountId, DateTime startDate, DateTime endDate);
    }
}