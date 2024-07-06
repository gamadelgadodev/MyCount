namespace Core.Entities
{
    public class Income : BaseEntity
    {
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IncomeCat IncomeCat { get; set; }
        public int IncomeCatId { get; set; }
    }
}