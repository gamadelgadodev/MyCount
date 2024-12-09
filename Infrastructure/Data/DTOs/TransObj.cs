using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.DTOs
{
    public class FilterTr
    {
        public decimal? Value { get; set; }
        public string? Description { get; set; } 
        public string? Cat { get; set; }
        public DateTime? Date { get; set; }
        public string? RangeDate { get; set;}
    }
}