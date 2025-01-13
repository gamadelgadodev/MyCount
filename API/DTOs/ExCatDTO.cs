using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NewExCat
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string typeCat { get; set; }
    }
     public class UpExCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string typeCat { get; set; }
    }
}