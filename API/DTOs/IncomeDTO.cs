using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NewIncome
    {
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int IncomeCatId { get; set; }
    }
    public class UpIncome
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int IncomeCatId { get; set; }
    }
}