namespace Core.Entities
{
    public class Expense
    {
        public int id { get; set; }
        public int IdUser { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int IdType { get; set; }
    }
}