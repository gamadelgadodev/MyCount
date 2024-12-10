using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepo;
        public UserController(IGenericRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var products = await _userRepo.ListAllAsync();
            return Ok(products);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<User>>> GetActiveUsers()
        {
            var userAc = await _userRepo.ListAcAsync();
            return Ok(userAc);
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUser userDto)
        {
            var userOld = await _userRepo.GetByIdAsync(userDto.Id);
            userOld.UserName = userDto.UserName;
            userOld.Password = userDto.Password;
            userOld.Name = userDto.Name;
            userOld.LastName = userDto.LastName;
            var updateUser = await _userRepo.UpdateEntity(userOld);
            return Ok(updateUser);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var userDelete = await _userRepo.DeleteEntity(id);
            return Ok(userDelete);
        }
    }
}
