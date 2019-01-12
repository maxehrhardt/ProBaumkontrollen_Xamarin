using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProBaumkontrollen.Models
{
    [Table("tabBaumhoehenbereiche")]
    public class Baumhöhenbereiche
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
