namespace Core.Entities
{
    public class Expense : BaseEntity
    {
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public ExpenseCat ExpenseCat { get; set; }
        public int ExpenseCatId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        
        public DateTime Date { get; set; }
        public bool nessesary { get; set; }
        public bool IsDeleted { get; set; }
    }
}