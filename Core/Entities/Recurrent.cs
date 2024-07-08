using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Recurrent : BaseEntity
    {  
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Regularity { get; set; }
        public DateTime PastDate { get; set; }
        public DateTime NextTime { get; set;}
        public bool IsDeleted { get; set; }

    }
}
