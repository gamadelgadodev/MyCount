using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ChartData
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<Dataset> Datasets { get; set; } = new List<Dataset>();

        public class Dataset
        {
            public string Label { get; set; }
            public List<int> Data { get; set; } = new List<int>();
            public List<string> BackgroundColor { get; set; } = new List<string>();
            public int HoverOffset { get; set; }
        }
    }
}