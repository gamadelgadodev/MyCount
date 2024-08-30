using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
         private readonly IGenericRepository<Income> _incRepo;
        public IncomeController(IGenericRepository<Income> incRepo)
        {
            _incRepo = incRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Expense>>> GetAllIncomes()
        {
            var incomes = await _incRepo.ListAllAsync();
            return Ok(incomes);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Expense>>> GetActiveIncomes()
        {
            var incAc = await _incRepo.ListAcAsync();
            return Ok(incAc);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(NewIncome newInc)
        {
            var exp = new Income
            {
                AccountId = newInc.AccountId,
                Value = newInc.Value,
                Description = newInc.Description,
                IncomeCatId = newInc.IncomeCatId,
                Date = DateTime.Now,
                IsDeleted = false
            };
            var createExp = await _incRepo.AddNewEntity(exp);
            return Ok(createExp);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateIncome(UpIncome income)
        {
            var incOld = await _incRepo.GetByIdAsync(income.Id);
            incOld.AccountId = income.AccountId;
            incOld.Value = income.Value;
            incOld.Description = income.Description;
            incOld.IncomeCatId = income.IncomeCatId;
            var updateInc = await _incRepo.UpdateEntity(incOld);
            return Ok(updateInc);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteIncome(int id)
        {
            var incDelete = await _incRepo.DeleteEntity(id);
            return Ok(incDelete);
        }

    }
}