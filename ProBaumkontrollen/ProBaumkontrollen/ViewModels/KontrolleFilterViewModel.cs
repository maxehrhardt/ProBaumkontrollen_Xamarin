using ProBaumkontrollen.Models;
using ProBaumkontrollen.Validations;
using ProBaumkontrollen.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProBaumkontrollen.ViewModels
{
    public class KontrolleFilterViewModel : ViewModelBase
    {
        private ObservableCollection<Straße> _allStraßen;
        private ObservableCollection<Straße> _straßenFiltered;
        private ValidatableObject<Straße> _straßeSelected;
        private int _minBaumNr;
        private int _maxBaumNr;
        private KontrollFilter _filter;

        public ICommand UpdateStraßeCommand => new Command<TextChangedEventArgs>(UpdateStraße);
        public ICommand StraßeWasSelectedCommand => new Command<SelectedItemChangedEventArgs>(StraßeWasSelected);
        public ICommand BackCommand => new Command(async () => await BackAsync());
        public ICommand ShowListCommand => new Command(async () => await ShowList());

        public KontrolleFilterViewModel()
        {
            _straßeSelected = new ValidatableObject<Straße>();
            _straßeSelected.Value = new Straße();
            _straßeSelected.Validations.Add(new StraßeIsValidRule());
            _straßenFiltered = new ObservableCollection<Straße>(DataService.GetAllStraßen());
            _allStraßen = new ObservableCollection<Straße>(DataService.GetAllStraßen());

            _filter = new KontrollFilter();

        }

        public ObservableCollection<Straße> AllStraßen
        {
            get
            {
                return _allStraßen;
            }
            set
            {
                _allStraßen = value;
                RaisePropertyChanged(() => AllStraßen);
            }
        }

        public ObservableCollection<Straße> StraßenFiltered
        {
            get
            {
                return _straßenFiltered;
            }
            set
            {
                _straßenFiltered = value;
                RaisePropertyChanged(() => StraßenFiltered);
            }
        }

        public ValidatableObject<Straße> StraßeSelected
        {
            get
            {
                return _straßeSelected;
            }
            set
            {
                _straßeSelected = value;
                RaisePropertyChanged(() => StraßeSelected);
            }
        }

        private void UpdateStraße(TextChangedEventArgs arg)
        {
            List<Straße> straßenToAdd = new List<Straße>(_allStraßen.Where(w => w.name.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)));

            _straßenFiltered.Clear();
            foreach (var item in straßenToAdd)
            {
                _straßenFiltered.Add(item);
            }
        }

        private void StraßeWasSelected(SelectedItemChangedEventArgs arg)
        {
            Straße straße = arg.SelectedItem as Straße;         
            StraßeSelected.Value = straße;
        }


        public int MinBaumNr
        {
            get
            {
                return _minBaumNr;
            }
            set
            {
                _minBaumNr = value;
                RaisePropertyChanged(() => MinBaumNr);
            }
        }

        public int MaxBaumNr
        {
            get
            {
                return _maxBaumNr;
            }
            set
            {
                _maxBaumNr = value;
                RaisePropertyChanged(() => MaxBaumNr);
            }
        }

        private async Task BackAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
        }

        private async Task ShowList()
        {
            await NavigationService.NavigateToAsync<KontrolleListeViewModel>();
        }
    }
}
