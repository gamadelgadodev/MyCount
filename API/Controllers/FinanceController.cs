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

        [HttpPost("All")]
        public async Task<ActionResult> GetFinances([FromBody] FinanceRequest request)
        {
            var transactions = await _finaRepo.ListAcc(request.Acc,DateTime.Parse(request.StartDate),DateTime.Parse(request.EndDate));
              var groupedTransactions = transactions
            .GroupBy(t => t.TransactionCat)
            .OrderBy(g => g.Key.Name) 
            .ToList();

            var labels = groupedTransactions.Select(g => g.Key.Name).ToList();
            var type = groupedTransactions.Select(g=> g.Key.typeCat).ToList();
            var datas = groupedTransactions.Select(g => g.Sum(t => t.Value)).ToList();
            var colors = groupedTransactions.Select(g => g.Key.Color).ToList();

            var data = new{
                labels = labels,
                types = type,
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
            return Ok(data);
        }
    }
}