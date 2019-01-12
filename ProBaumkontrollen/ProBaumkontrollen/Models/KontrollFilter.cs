using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBaumkontrollen.Models
{
    public class KontrollFilter
    {
        public int MinBaumNr{get;set;}
        public int MaxBaumNr { get; set; }
        public Straße Straße { get; set; }
    }
}
