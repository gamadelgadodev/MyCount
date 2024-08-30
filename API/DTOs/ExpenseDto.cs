using Core.Entities;

namespace API.DTOs
{
    public class newExpense
    {
        public int AccountId { get; set; }
        public int ExpenseCatId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool nessesary { get; set; }
    }
    public class upExpense
    {
        public int AccountId { get; set; }
        public int ExpenseCatId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool nessesary { get; set; }
    }
}
