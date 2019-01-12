using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProBaumkontrollen.Models
{
    [Table("tabSchaedigungsgrad")]
    public class Schädigungsgrad
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
