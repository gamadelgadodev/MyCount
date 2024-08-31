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
    public class ExpenseCatController : ControllerBase
    {
        private readonly IGenericRepository<ExpenseCat> _ecatRepo;
        public ExpenseCatController(IGenericRepository<ExpenseCat> ecatRepo)
        {
            _ecatRepo = ecatRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Expense>>> GetAllExCat()
        {
            var exCat = await _ecatRepo.ListAllAsync();
            return Ok(exCat);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Expense>>> GetActiveExCat()
        {
            var exCatAc = await _ecatRepo.ListAcAsync();
            return Ok(exCatAc);
        }

        [HttpPost]
        public async Task<ActionResult> CreateExpCat(NewExCat newExCat)
        {
            var exp = new ExpenseCat
            {
                Name = newExCat.Name,
                Description = newExCat.Description,
                Color = newExCat.Color,
                IsDeleted = false
            };
            var createExp = await _ecatRepo.AddNewEntity(exp);
            return Ok(createExp);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateExpCat(UpExCat exCat)
        {
            var exCatOld = await _ecatRepo.GetByIdAsync(exCat.Id);
            exCatOld.Name = exCat.Name;
            exCatOld.Description = exCat.Description;
            exCatOld.Color = exCat.Color;
            var updateEx = await _ecatRepo.UpdateEntity(exCatOld);
            return Ok(updateEx);
        }
        // [HttpDelete]
        // public async Task<ActionResult> DeleteExpCat(int id)
        // {
        //     var incDelete = await _ecatRepo.DeleteEntity(id);
        //     return Ok(incDelete);
        // }
    }
}