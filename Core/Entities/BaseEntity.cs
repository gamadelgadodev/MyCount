using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class BaseEntity
    {
        [JsonPropertyOrder(-1)]
        public int Id { get; set; }
    }
}
