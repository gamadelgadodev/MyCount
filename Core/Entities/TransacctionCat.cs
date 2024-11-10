namespace Core.Entities
{
    public class TransactionCat : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string typeCat { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
    }
}