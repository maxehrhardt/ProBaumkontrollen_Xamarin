using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProBaumkontrollen.Models
{
    [Table("tabAusfuehrenBis")]
    public class AusführenBis
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
