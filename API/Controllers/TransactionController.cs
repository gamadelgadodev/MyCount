using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IGenericRepository<Transaction> _expRepo;
        public TransactionController(IGenericRepository<Transaction> expRepo)
        {
            _expRepo = expRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Transaction>>> GetAllTransactions()
        {
            var Transactions = await _expRepo.ListAllAsync();
            return Ok(Transactions);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Transaction>>> GetActiveTransactions()
        {
            var expAc = await _expRepo.ListAcAsync();
            return Ok(expAc);
        }
        [HttpGet("Recent")]
        public async Task<ActionResult<List<TransactionsGen>>> getRecent()
        {
            var Transactions = await _expRepo.ListPage(1,10);
            return Ok(Transactions);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTransaction(newTransaction newTransaction)
        {
            var exp = new Transaction
            {
                AccountId = newTransaction.AccountId,
                TransactionCatId = newTransaction.TransactionCatId,
                Value = newTransaction.Value,
                typeTransaction = newTransaction.typeTransaction,
                Description = newTransaction.Description,
                Date = DateTime.Now,
                nessesary = newTransaction.nessesary,
                IsDeleted = false
            };
            var createExp = await _expRepo.AddNewEntity(exp);
            return Ok(createExp);
        }
         [HttpPost("addExpense")]
        public async Task<ActionResult> CreateTransactionExp(newTransaction newTransaction)
        {
            var exp = new Transaction
            {
                AccountId = newTransaction.AccountId,
                TransactionCatId = newTransaction.TransactionCatId,
                Value = newTransaction.Value,
                typeTransaction = "expense",
                Description = newTransaction.Description,
                Date = DateTime.Now,
                nessesary = newTransaction.nessesary,
                IsDeleted = false
            };
            var createExp = await _expRepo.AddNewEntity(exp);
            return Ok(createExp);
        }

        [HttpPost("addIncome")]
        public async Task<ActionResult> CreateTransactionInc(newTransaction newTransaction)
        {
            var exp = new Transaction
            {
                AccountId = newTransaction.AccountId,
                TransactionCatId = newTransaction.TransactionCatId,
                Value = newTransaction.Value,
                typeTransaction = "income",
                Description = newTransaction.Description,
                Date = DateTime.Now,
                nessesary = newTransaction.nessesary,
                IsDeleted = false
            };
            var createExp = await _expRepo.AddNewEntity(exp);
            return Ok(createExp);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTransaction(upTransaction Transaction)
        {
            var expOld = await _expRepo.GetByIdAsync(Transaction.Id);
            expOld.AccountId = Transaction.AccountId;
            expOld.TransactionCatId = Transaction.TransactionCatId;
            expOld.Value = Transaction.Value;
            expOld.Description = Transaction.Description;
            var updateExp = await _expRepo.UpdateEntity(expOld);
            return Ok(updateExp);
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            var expDelete = await _expRepo.DeleteEntity(id);
            return Ok(expDelete);
        }

    }
}
