namespace Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string typeTransaction { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
        public TransactionCat TransactionCat { get; set; }
        public int TransactionCatId { get; set; }
        public bool? nessesary { get; set; }
        public bool IsDeleted { get; set; }
    }
}