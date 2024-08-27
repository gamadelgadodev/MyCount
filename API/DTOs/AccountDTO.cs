using Core.Entities;

namespace API.DTOs
{
	public class NewAcct
	{
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public decimal Debt { get; set; }
    }
}
