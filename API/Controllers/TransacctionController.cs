using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace API.Controllers
{
    [Route("[controller]")]
    public class TransacctionController : Controller
    {
         private readonly IGenericRepository<Expense> _expRepo;
         private readonly IGenericRepository<Income> _incRepo;

        public TransacctionController(IGenericRepository<Expense> expRepo,IGenericRepository<Income> incRepo)
        {
             _expRepo = expRepo;
             _incRepo = incRepo;
        }

        [HttpGet("All")]
        //public async Task<ActionResult> GetProducts()
        public async Task<ActionResult<List<Expense>>> GetAllExpenses()
        {
            var expenses = await _expRepo.ListAllAsync();
            var incomes = await _incRepo.ListAcAsync();
            var combinedList = expenses.Select(e => new 
    {
        Id = e.Id,
        Description = e.Description,
        Date = e.Date,
        Type = "Expense",
        Value = e.Value
    })
    .Concat(incomes.Select(i => new 
    {
        Id = i.Id,
        Description = i.Description,
        Date = i.Date,
        Type = "Income",
        Value = i.Value
    }))
    .OrderBy(entry => entry.Date)
    .ToList();
            
            return Ok(combinedList);
        }


    }
}