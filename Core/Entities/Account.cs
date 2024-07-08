using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Account : BaseEntity
    {
        
        public User User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public decimal Debt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
