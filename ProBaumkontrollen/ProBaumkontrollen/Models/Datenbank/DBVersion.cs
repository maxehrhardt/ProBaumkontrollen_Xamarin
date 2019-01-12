using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProBaumkontrollen.Models
{
    [Table("tabDBVersion")]
    public class DBVersion
    {
        [PrimaryKey]
        public int id { get; set; }
        public string version { get; set; }
    }
}
