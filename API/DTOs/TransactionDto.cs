using Core.Entities;

namespace API.DTOs
{
    public class newTransaction
    {
        public int AccountId { get; set; }
        public int TransactionCatId { get; set; }
        public string typeTransaction { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool? nessesary { get; set; }
    }
    public class upTransaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TransactionCatId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public bool? nessesary { get; set; }
    }
    public class TransactionsGen
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string typeTransaction { get; set; }
    }
}
