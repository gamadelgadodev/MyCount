using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Boolean> GetUser(string UserName,string Password);
    }
}