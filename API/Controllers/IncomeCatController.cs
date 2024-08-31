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
    public class IncomeCatController : ControllerBase
    {
        private readonly IGenericRepository<IncomeCat> _icatRepo;
        public IncomeCatController(IGenericRepository<IncomeCat> icatRepo)
        {
            _icatRepo = icatRepo;
        }
        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Income>>> GetAllInCat()
        {
            var inCat = await _icatRepo.ListAllAsync();
            return Ok(inCat);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Income>>> GetActiveInCat()
        {
            var inCatAc = await _icatRepo.ListAcAsync();
            return Ok(inCatAc);
        }

        [HttpPost]
        public async Task<ActionResult> CreateInCat(NewInCat newInCat)
        {
            var inc = new IncomeCat
            {
                Name = newInCat.Name,
                Description = newInCat.Description,
                color = newInCat.color,
                IsDeleted = false
            };
            var createInc = await _icatRepo.AddNewEntity(inc);
            return Ok(createInc);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateInCat(UpExCat exCat)
        {
            var exCatOld = await _icatRepo.GetByIdAsync(exCat.Id);
            exCatOld.Name = exCat.Name;
            exCatOld.Description = exCat.Description;
            exCatOld.color = exCat.Color;
            var updateEx = await _icatRepo.UpdateEntity(exCatOld);
            return Ok(updateEx);
        }
        // [HttpDelete]
        // public async Task<ActionResult> DeleteIncome(int id)
        // {
        //     var incDelete = await _icatRepo.DeleteEntity(id);
        //     return Ok(incDelete);
        // }
    }
}