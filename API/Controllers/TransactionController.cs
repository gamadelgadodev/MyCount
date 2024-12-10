using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IGenericRepository<Transaction> _expRepo;
        private readonly ITransactionRepository _tRepo;
        public TransactionController(IGenericRepository<Transaction> expRepo,ITransactionRepository tRepo)
        {
            _expRepo = expRepo;
            _tRepo = tRepo;
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
        [HttpGet("Recent/{accountId}")]
        public async Task<ActionResult<List<TransactionsGen>>> getRecent(int accountId)
        {
            var Transactions = await _tRepo.ListPage(1,5,accountId);
            return Ok(Transactions);
        }
        [HttpGet("AllTrans/{accountId}/{page}")]
        public async Task<ActionResult<List<TransactionsGen>>> getPagedTrans(int accountId, int page)
        {
            var Transactions = await _tRepo.ListPage(page,3,accountId);
            return Ok(Transactions);
        }
        [HttpPost("filteredTrans/{accountId}/{page}")]
        public async Task<ActionResult<List<TransactionsGen>>> getFilteredTrans(int accountId, int page,[ FromBody]FilterTr? filter)
        {
            var Transactions = await _tRepo.ListByFilter(filter.Value,filter.Description,filter.Cat,filter.Date,filter.RangeDate,accountId,page);
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
            var createExp = await _tRepo.CreateTransB(exp);
            return Ok(createExp);
        }
         [HttpPost("addTransacction")]
        public async Task<ActionResult> CreateTransactionExp(newTransaction newTransaction)
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
            var createExp = await _tRepo.CreateTransB(exp);
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
            var createExp = await _tRepo.CreateTransB(exp);
            return Ok(createExp);
        }
        [HttpPut("editTrans")]
        public async Task<ActionResult> UpdateTransactionReload(upTransaction Transaction)
        {
            
            var expOld = await _expRepo.GetByIdAsync(Transaction.Id);
            decimal valOld = expOld.Value;
            expOld.AccountId = Transaction.AccountId;
            expOld.TransactionCatId = Transaction.TransactionCatId;
            expOld.Value = Transaction.Value;
            expOld.Description = Transaction.Description;
            var updateExp = await _tRepo.UpdateTrans(expOld,valOld);
            return Ok(updateExp);
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
            var expDelete = await _tRepo.DelteTrans(id);
            return Ok(expDelete);
        }

    }
}
