using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Boolean> TrustUser(string UserName,string Password);
        Task<User> GetByUsername(string UserName,string Password);
    }
}