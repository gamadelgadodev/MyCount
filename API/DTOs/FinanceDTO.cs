using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FinanceRequest
{
    public int Acc { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}
}