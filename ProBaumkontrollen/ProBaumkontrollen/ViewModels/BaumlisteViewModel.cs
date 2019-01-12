using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProBaumkontrollen.ViewModels;
using ProBaumkontrollen.ViewModels.Base;
using System.Collections.ObjectModel;
using ProBaumkontrollen.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProBaumkontrollen.ViewModels
{
    public class BaumlisteViewModel : ViewModelBase
    {
        public ICommand BackCommand => new Command(async () => await BackAsync());
        public ICommand ShowDetailsCommand => new Command(async () => await BaumDetailAsync());
        //public ICommand BaumItemWasSelectedCommand => new Command<SelectedItemChangedEventArgs>(BaumItemWasSelected);

        public BaumlisteViewModel()
        {
            Baumliste = new ObservableCollection<Baum>(DataService.GetBaumliste().ToList());
            BaumItemListe = new ObservableCollection<BaumItem>();
            CreateBaumItems();
        }

        ObservableCollection<Baum> _baumliste;
        public ObservableCollection<Baum> Baumliste
        {
            get { return _baumliste; }
            set
            {
                _baumliste = value;
                RaisePropertyChanged(() => Baumliste);
            }
            
        }

        ObservableCollection<BaumItem> _baumItemListe;
        public ObservableCollection<BaumItem> BaumItemListe
        {
            get { return _baumItemListe; }
            set
            {
                _baumItemListe = value;
                RaisePropertyChanged(() => BaumItemListe);
            }

        }

        BaumItem _baumItemSelected;
        public BaumItem BaumItemSelected
        {
            get { return _baumItemSelected; }
            set
            {
                _baumItemSelected = value;
                RaisePropertyChanged(() => BaumItemSelected);
            }
        }

        //private void BaumItemWasSelected(SelectedItemChangedEventArgs arg)
        //{
        //    BaumItem baumitem = arg.SelectedItem as BaumItem;

        //    BaumItemSelected = baumitem;
        //}

        private void CreateBaumItems()
        {
            foreach (var baum in Baumliste)
            {
                BaumItem baumItem = new BaumItem();
                baumItem.baum = baum;

                Straße straße = DataService.GetStraßeByID(baum.straßeId);
                baumItem.straße = straße;
                Baumart baumart = DataService.GetBaumartByID(baum.baumartId);
                baumItem.baumart = baumart;

                Kontrolle kontrolle = DataService.GetKontrolleByBaumID(baum.id);
                baumItem.kontrolle = kontrolle;
                

                baumItem.entwicklungsphase = DataService.GetEntwicklungsphaseByID(kontrolle.entwicklungsphaseID);
                baumItem.schädigungsgrad = DataService.GetSchädigungsgradByID(kontrolle.schädigungsgradID);
                baumItem.ausführenBis = DataService.GetAusführenBisByID(kontrolle.ausführenBisIDs);
                baumItem.baumhöhenbereich = DataService.GetBaumhöhenbereichByID(kontrolle.baumhöhe_bereichIDs);

                BaumItemListe.Add(baumItem);

            }
        }

        private async Task BackAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
        }

        private async Task BaumDetailAsync()
        {
            if (BaumItemSelected!=null)
            {
                await NavigationService.NavigateToAsync<BaumDetailsViewModel>(BaumItemSelected);
            }
            else
            {
                await DialogService.ShowAlertAsync("Es muss ein Baum ausgewählt werden!", "Hinweis", "OK");
            }
            
        }

    }


}
