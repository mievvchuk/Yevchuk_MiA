using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVMazeClient.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Premiered { get; set; }
        public string Summary { get; set; }
    }
}

