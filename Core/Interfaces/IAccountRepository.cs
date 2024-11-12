using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> ListAcc(int userId);
    }
}