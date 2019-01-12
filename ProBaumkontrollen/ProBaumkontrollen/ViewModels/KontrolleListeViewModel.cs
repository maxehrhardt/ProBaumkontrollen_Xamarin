using ProBaumkontrollen.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProBaumkontrollen.ViewModels
{
    public class KontrolleListeViewModel : ViewModelBase
    {
        public ICommand BackCommand => new Command(async () => await BackAsync());

        public KontrolleListeViewModel()
        {

        }

        private async Task BackAsync()
        {
            await NavigationService.NavigateToAsync<KontrolleFilterViewModel>();
        }
    }
}
