using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBaumkontrollen.Models
{
    public class BaumItem
    {
        public Baum baum { get; set; }
        public Kontrolle kontrolle { get; set; }
        public Straße straße { get; set; }
        public Baumart baumart { get; set; }
        public Baumhöhenbereiche baumhöhenbereich { get; set; }
        public Entwicklungsphase entwicklungsphase { get; set; }
        public Schädigungsgrad schädigungsgrad { get; set; }
        public AusführenBis ausführenBis { get; set; }

    }
}
