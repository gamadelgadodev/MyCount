namespace Core.Entities
{
    public class Balance
    {
        public int id { get; set; }
        public int IdUser { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        // public decimal Loan { get; set; }
    }
}