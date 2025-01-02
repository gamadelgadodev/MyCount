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
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceRepository _finaRepo;
         private readonly IGenericRepository<TransactionCat> _ecatRepo;
        public FinanceController(IFinanceRepository finaRepo, IGenericRepository<TransactionCat> ecatRepo)
        {
            _ecatRepo = ecatRepo;
            _finaRepo = finaRepo;
        }

        [HttpGet("All")]
         public async Task<ActionResult> GetFinances()
        {
            // List<string> labels = new List<string>();
            // List<int> data = new List<int>();
            // List<string> backgroundColor = new List<string>();
            // List<int> ind = new List<int>();
            var transactions = await _finaRepo.ListAcc(1,DateTime.Today.AddDays(-60),DateTime.Today);
              var groupedTransactions = transactions
            .GroupBy(t => t.TransactionCat)
            .OrderBy(g => g.Key.Name) // Asegura que el orden sea consistente
            .ToList();

             var labels = groupedTransactions.Select(g => g.Key.Name).ToList();
            var datas = groupedTransactions.Select(g => g.Sum(t => t.Value)).ToList();
             var colors = groupedTransactions.Select(g => g.Key.Color).ToList();

            var data = new{
                labels = labels,
            datasets = new[]
            {
                new
                {
                    label = "Acountability",
                    data = datas,
                    backgroundColor = colors,
                    hoverOffset = 10
                }
            }
            };
            // var cat = await _ecatRepo.ListAllAsync();
            // foreach(Transaction x in Transactions)
            // {
            //     if(ind.Contains(x.TransactionCatId))
            //     {
                    
            //     }
            //     else{

            //     }
            // }
            return Ok(data);
        }
    }
}