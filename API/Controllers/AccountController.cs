using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<Account> _accountRepo;
        public AccountController(IGenericRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }
        [HttpGet]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Account>>> GetProducts()
        {
            //var spec = new ProductsWithTypesAndBrandesSpecification();
            var products = await _accountRepo.ListAllAsync();
            return Ok(products);
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
            var createUser = await _accountRepo.AddNewEntity(acct);
            return Ok(createUser);
        }
    }
}
