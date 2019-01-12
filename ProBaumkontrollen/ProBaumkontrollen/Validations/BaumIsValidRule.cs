using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProBaumkontrollen.Models;

namespace ProBaumkontrollen.Validations
{
    public class BaumIsValidRule:IValidationRule<Baum>
    {
        public string ValidationMessage { get; set; }

        public bool Check(Baum baum)
        {
            bool valid = true;
            var str="";

            if (baum == null)
            {
                valid = false;
            }

            
            if (baum.baumNr==0)
            {

            }
            
            
            return valid;
        }
    }
}
