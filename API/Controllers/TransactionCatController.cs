using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TransactionCatController : ControllerBase
    {
        private readonly IGenericRepository<TransactionCat> _ecatRepo;
        public TransactionCatController(IGenericRepository<TransactionCat> ecatRepo)
        {
            _ecatRepo = ecatRepo;
        }

        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Transaction>>> GetAllCat()
        {
            var exCat = await _ecatRepo.ListAllAsync();
            return Ok(exCat);
        }
        [HttpGet("AllExpenseCat")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Transaction>>> GetAllExCat()
        {
            var exCat = await _ecatRepo.ListAllAsync();
            exCat = exCat.Where(x=>x.typeCat=="expense").ToList();
            return Ok(exCat);
        }
         [HttpGet("AllIncoCat")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Transaction>>> GetAllInCat()
        {
            var incCat = await _ecatRepo.ListAllAsync();
            incCat = incCat.Where(x=>x.typeCat=="income").ToList();
            return Ok(incCat);
        }
        [HttpGet("Active")]
        public async Task<ActionResult<List<Transaction>>> GetActiveExCat()
        {
            var exCatAc = await _ecatRepo.ListAcAsync();
            return Ok(exCatAc);
        }

        [HttpPost]
        public async Task<ActionResult> CreateExpCat(NewExCat newExCat)
        {
            var exp = new TransactionCat
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