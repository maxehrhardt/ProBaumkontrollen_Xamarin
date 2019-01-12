using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProBaumkontrollen.Models;

using ProBaumkontrollen.Services;
using ProBaumkontrollen.ViewModels.Base;


namespace ProBaumkontrollen.Validations
{
    public class StraßeIsValidRule:IValidationRule<Straße>
    {
        public string ValidationMessage { get; set; }

        public bool Check(Straße straße)
        {
            bool valid = true;
            var str = "";

            if (straße == null)
            {
                valid = false;
            }
            else
            {
                if (straße.name == null || straße.name.Length == 0)
                {
                    valid = false;
                }

                var dataService = ViewModelLocator.Resolve<IDataService>();
                if (!dataService.GetAllStraßen().ToList().Exists(x => x.name == straße.name))
                {
                    valid = false;
                    ValidationMessage = ValidationMessage + "Die Straße ist noch nicht in der Datenbank enthalten.";
                }
            }



            

            return valid;
        }
    }
}
