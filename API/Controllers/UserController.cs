using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepo;
        public UserController(IGenericRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<User>>> GetProducts()
        {
            //var spec = new ProductsWithTypesAndBrandesSpecification();
            var products = await _userRepo.ListAllAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUser userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                LastName = userDto.LastName,
                Name = userDto.Name,
                IsDeleted = false
            };
            var createUser = await _userRepo.AddNewEntity(user);
            return Ok(createUser);
        }
    }
}
