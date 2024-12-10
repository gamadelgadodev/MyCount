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
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<Account> _genericRepo;
        private readonly IAccountRepository _accRepo;
        public AccountController(IGenericRepository<Account> accountRepo,IAccountRepository accRepo)
        {
            _genericRepo = accountRepo;
            _accRepo = accRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Account>>> GetProducts()
        {
            //var spec = new ProductsWithTypesAndBrandesSpecification();
            var products = await _genericRepo.ListAllAsync();
            return Ok(products);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Account>>> GetActiveUsers()
        {
            var AccAc = await _genericRepo.ListAcAsync();
            return Ok(AccAc);
        }
        [HttpGet("UserAcc/{userId}")]
        public async Task<ActionResult<List<Account>>> getAccountsUser(int userId)
        {
            var AccAc = await _accRepo.ListAcc(userId);
            return Ok (AccAc);
        }
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Account>> GetByIdAsync(int id)
        {
            var AccId = await _genericRepo.GetByIdAsync(id);
            return AccId;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAcct(NewAcct acctDto)
        {
            var acct = new Account
            {
                UserId = acctDto.UserId,
                Name = acctDto.Name,
                Balance = acctDto.Balance,
                Description = acctDto.Description,
                Debt = acctDto.Debt,
                IsDeleted = false
            };
            var createUser = await _genericRepo.AddNewEntity(acct);
            return Ok(createUser);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpAcct acctDto)
        {
            var acctOld = await _genericRepo.GetByIdAsync(acctDto.Id);
            acctOld.Name = acctDto.Name;
            acctOld.Balance = acctDto.Balance;
            acctOld.Description = acctDto.Description;
            acctOld.Debt = acctDto.Debt;
            var updateUser = await _genericRepo.UpdateEntity(acctOld);
            return Ok(updateUser);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var acctDelete = await _genericRepo.DeleteEntity(id);
            return Ok(acctDelete);
        }
    }
}
