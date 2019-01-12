using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProBaumkontrollen.Models
{
    [Table("tabBaumart")]
    public class Baumart
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NameDeutsch { get; set; }
        public string NameBotanisch { get; set; }
    }
}
