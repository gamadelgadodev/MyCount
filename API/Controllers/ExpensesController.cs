using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IGenericRepository<Expense> _expRepo;
        public ExpensesController(IGenericRepository<Expense> expRepo)
        {
            _expRepo = expRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Expense>>> GetAllExpenses()
        {
            var expenses = await _expRepo.ListAllAsync();
            return Ok(expenses);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Expense>>> GetActiveExpenses()
        {
            var expAc = await _expRepo.ListAcAsync();
            return Ok(expAc);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(newExpense newExp)
        {
            var exp = new Expense
            {
                AccountId = newExp.AccountId,
                ExpenseCatId = newExp.ExpenseCatId,
                Value = newExp.Value,
                Description = newExp.Description,
                Date = DateTime.Now,
                IsDeleted = false
            };
            var createExp = await _expRepo.AddNewEntity(exp);
            return Ok(createExp);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateExpense(upExpense expense)
        {
            var expOld = await _expRepo.GetByIdAsync(expense.Id);
            expOld.AccountId = expense.AccountId;
            expOld.ExpenseCatId = expense.ExpenseCatId;
            expOld.Value = expense.Value;
            expOld.Description = expense.Description;
            var updateExp = await _expRepo.UpdateEntity(expOld);
            return Ok(updateExp);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            var expDelete = await _expRepo.DeleteEntity(id);
            return Ok(expDelete);
        }

    }
}
