using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProBaumkontrollen.ViewModels.Base;

namespace ProBaumkontrollen.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand BackCommand => new Command(async () => await BackAsync());

        private async Task BackAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
        }

    }
}
