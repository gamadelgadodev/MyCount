using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NewInCat
    {
         public string Name { get; set; }
        public string Description { get; set; }
        public string color { get; set; }

    }
     public class UpInCat
    {
        public int Id { get; set; }
         public string Name { get; set; }
        public string Description { get; set; }
        public string color { get; set; }

    }
}