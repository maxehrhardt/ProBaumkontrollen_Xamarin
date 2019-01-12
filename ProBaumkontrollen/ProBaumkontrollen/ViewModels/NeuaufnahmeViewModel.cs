using ProBaumkontrollen.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProBaumkontrollen.Models;
using ProBaumkontrollen.Services;
using ProBaumkontrollen.Validations;
using System.Collections.ObjectModel;
using ProBaumkontrollen.Models.Datenbank;

namespace ProBaumkontrollen.ViewModels
{
    public class NeuaufnahmeViewModel : ViewModelBase
    {
        private ValidatableObject<Baum> _baum;
        private ValidatableObject<Kontrolle> _kontrolle;
        private ObservableCollection<Baumart> _allBaumarten;
        private ObservableCollection<Baumart> _baumartDeutschFiltered;
        private ObservableCollection<Baumart> _baumartBotanischFiltered;
        private ValidatableObject<Baumart> _baumartSelected;
        private ObservableCollection<Baumhöhenbereiche> _allBaumhöhenbereiche;
        private ObservableCollection<Entwicklungsphase> _allEntwicklungsphasen;
        private Entwicklungsphase _entwicklungsphaseSelected;
        private ObservableCollection<Schädigungsgrad> _allSchädigungsgrade;
        private Schädigungsgrad _schädigungsgradSelected;
        private ObservableCollection<Straße> _allStraßen;
        private ObservableCollection<Straße> _straßenFiltered;
        private ValidatableObject<Straße> _straßeSelected;
        private Baumhöhenbereiche _baumhöhenbereichSelected;
        private ObservableCollection<Schadsymptom> _schadsymptomeFiltered;
        private ObservableCollection<Schadsymptom> _schadsymptomeSelected;
        private ObservableCollection<Schadsymptom> _allSchadsymptome;
        private string _schadsymptom;
        private Verkehrssicherheit _verkehrssicherheit;
        private ObservableCollection<Maßnahmen> _maßnahmenFiltered;
        private ObservableCollection<Maßnahmen> _maßnahmenSelected;
        private ObservableCollection<Maßnahmen> _allMaßnahmen;
        private string _maßnahmen;
        private ObservableCollection<AusführenBis> _allAusführenBis;
        private AusführenBis _ausführenBisSelected;

        public ICommand SaveCommand => new Command(async () => await SaveAsync());
        public ICommand BackCommand => new Command(async () => await BackAsync());
        public ICommand UpdateStraßeCommand => new Command<TextChangedEventArgs>(UpdateStraße);
        public ICommand StraßeWasSelectedCommand => new Command<SelectedItemChangedEventArgs>(StraßeWasSelected);
        public ICommand AddStraßeCommand => new Command(AddStraße);
        public ICommand DecreaseBaumNrCommand => new Command(DecreaseBaumNr);
        public ICommand IncreaseBaumNrCommand => new Command(IncreaseBaumNr);
        public ICommand DecreasePlakettenNrCommand => new Command(DecreasePlakettenNr);
        public ICommand IncreasePlakettenNrCommand => new Command(IncreasePlakettenNr);
        public ICommand UpdateBaumartDeutschCommand => new Command<TextChangedEventArgs>(UpdateBaumartDeutsch);    
        public ICommand BaumartDeutschWasSelectedCommand => new Command<ItemTappedEventArgs>(BaumartDeutschWasSelected);
        public ICommand UpdateBaumartBotanischCommand => new Command<TextChangedEventArgs>(UpdateBaumartBotanisch);
        public ICommand BaumartBotanischWasSelectedCommand => new Command<ItemTappedEventArgs>(BaumartBotanischWasSelected);
        public ICommand BaumhöheCompletedCommand => new Command(BaumhöheCompleted);
        public ICommand AddBaumartCommand => new Command(AddBaumart);
        public ICommand SchadsymptomHinzufügenCommand => new Command<string>(SchadsymptomHinzufügen);
        public ICommand SchadsymptomWasSelectedCommand => new Command<ItemTappedEventArgs>(SchadsymptomWasSelected);
        public ICommand UpdateSchadsymptomCommand => new Command<TextChangedEventArgs>(UpdateSchadsymptom);
        public ICommand DeleteSchadsymptomSelectedCommand => new Command(DeleteSchadsymptomSelected);
        public ICommand ChangeVerkehrssicherheitCommand => new Command(ChangeVerkehrssicherheit);
        public ICommand MaßnahmenHinzufügenCommand => new Command<string>(MaßnahmenHinzufügen);
        public ICommand MaßnahmenWasSelectedCommand => new Command<ItemTappedEventArgs>(MaßnahmenWasSelected);
        public ICommand UpdateMaßnahmenCommand => new Command<TextChangedEventArgs>(UpdateMaßnahmen);
        public ICommand DeleteMaßnahmeSelectedCommand => new Command(DeleteMaßnahmeSelected);

        public NeuaufnahmeViewModel()
        {
            //Einen neuen baum und eine Kontrolle als Validatable Object erzeugen
            _baum = new ValidatableObject<Baum>();
            _baum.Value = new Baum();
            _baum.Validations.Add(new BaumIsValidRule());        
            _baum.Value.erstelldatum= DateTime.Now.ToString("dd.MM.yyyy");

            _kontrolle = new ValidatableObject<Kontrolle>();
            _kontrolle.Value = new Kontrolle();
            
            _kontrolle.Value.kontrollintervall = 2;
            _kontrolle.Value.kontrolldatum = DateTime.Now.ToString("dd.MM.yyyy");
            _kontrolle.Value.stammanzahl = 1;
           


            _straßeSelected = new ValidatableObject<Straße>();
            _straßeSelected.Value = new Straße();
            _straßeSelected.Validations.Add(new StraßeIsValidRule());        
            _straßenFiltered = new ObservableCollection<Straße>(DataService.GetAllStraßen());
            _allStraßen = new ObservableCollection<Straße>(DataService.GetAllStraßen());


            _allBaumhöhenbereiche = new ObservableCollection<Baumhöhenbereiche>(DataService.GetAllBaumhöhenbereiche());
            _allEntwicklungsphasen = new ObservableCollection<Entwicklungsphase>(DataService.GetAllEntwicklungsphasen());
            _allSchädigungsgrade = new ObservableCollection<Schädigungsgrad>(DataService.GetAllSchädigungsgrade());

            _allBaumarten = new ObservableCollection<Baumart>(DataService.GetAllBaumarten());
            _baumartDeutschFiltered = new ObservableCollection<Baumart>(DataService.GetAllBaumarten());
            _baumartBotanischFiltered = new ObservableCollection<Baumart>(DataService.GetAllBaumarten());
            _baumartSelected = new ValidatableObject<Baumart>();
            _baumartSelected.Value = new Baumart();

            _allSchadsymptome= new ObservableCollection<Schadsymptom>(DataService.GetAllSchadsymptome());
            _schadsymptomeFiltered= new ObservableCollection<Schadsymptom>(DataService.GetAllSchadsymptome());          
            _schadsymptomeSelected = new ObservableCollection<Schadsymptom>();

            _verkehrssicherheit = Verkehrssicherheit.ungesetzt;

            _allMaßnahmen = new ObservableCollection<Maßnahmen>(DataService.GetAllMaßnahmen());
            _maßnahmenFiltered = new ObservableCollection<Maßnahmen>(DataService.GetAllMaßnahmen());
            _maßnahmenSelected = new ObservableCollection<Maßnahmen>();

            _allAusführenBis = new ObservableCollection<AusführenBis>(DataService.GetAllAusführenBis());
        }
        
        public ValidatableObject<Baum> Baum
        {
            get
            {
                return _baum;
            }
            set
            {
                _baum = value;
                RaisePropertyChanged(() => Baum);
            }
        }
        
        public ValidatableObject<Kontrolle> Kontrolle
        {
            get
            {
                return _kontrolle;
            }
            set
            {
                _kontrolle = value;
                RaisePropertyChanged(() => Kontrolle);
            }
        }

        public ObservableCollection<Baumart> AllBaumarten
        {
            get
            {
                return _allBaumarten;
            }
            set
            {
                _allBaumarten = value;
                RaisePropertyChanged(() => AllBaumarten);
            }
        }
        public ObservableCollection<Baumhöhenbereiche> AllBaumhöhenbereiche
        {
            get
            {
                return _allBaumhöhenbereiche;
            }
            set
            {
                _allBaumhöhenbereiche = value;
                RaisePropertyChanged(() => AllBaumhöhenbereiche);
            }
        }
        public ObservableCollection<Entwicklungsphase> AllEntwicklungsphasen
        {
            get
            {
                return _allEntwicklungsphasen;
            }
            set
            {
                _allEntwicklungsphasen = value;
                RaisePropertyChanged(() => AllEntwicklungsphasen);
            }
        }
        public ObservableCollection<Schädigungsgrad> AllSchädigungsgrade
        {
            get
            {
                return _allSchädigungsgrade;
            }
            set
            {
                _allSchädigungsgrade = value;
                RaisePropertyChanged(() => AllSchädigungsgrade);
            }
        }
        public ObservableCollection<AusführenBis> AllAusführenBis
        {
            get
            {
                return _allAusführenBis;
            }
            set
            {
                _allAusführenBis = value;
                RaisePropertyChanged(() => AllAusführenBis);
            }
        }

        public AusführenBis AusführenBisSelected
        {
            get
            {
                return _ausführenBisSelected;
            }
            set
            {
                _ausführenBisSelected = value;
                RaisePropertyChanged(() => AusführenBisSelected);
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

        public ObservableCollection<Baumart> BaumartDeutschFiltered
        {
            get
            {
                return _baumartDeutschFiltered;
            }
            set
            {
                _baumartDeutschFiltered = value;
                RaisePropertyChanged(() => BaumartDeutschFiltered);
            }

        }

        public ObservableCollection<Baumart> BaumartBotanischFiltered
        {
            get
            {
                return _baumartBotanischFiltered;
            }
            set
            {
                _baumartBotanischFiltered = value;
                RaisePropertyChanged(() => BaumartBotanischFiltered);
            }

        }

        public ValidatableObject<Baumart> BaumartSelected
        {
            get
            {
                return _baumartSelected;
            }
            set
            {
                _baumartSelected = value;
                RaisePropertyChanged(() => BaumartSelected);
            }
        }

        public Baumhöhenbereiche BaumhöhenbereichSelected
        {
            get
            {
                return _baumhöhenbereichSelected;
            }
            set
            {
                _baumhöhenbereichSelected = value;
                RaisePropertyChanged(() => BaumhöhenbereichSelected);
            }
        }

        public Entwicklungsphase EntwicklungsphaseSelected
        {
            get
            {
                return _entwicklungsphaseSelected;
            }
            set
            {
                _entwicklungsphaseSelected = value;
                RaisePropertyChanged(() => EntwicklungsphaseSelected);
            }
        }

        public Schädigungsgrad SchädigungsgradSelected
        {
            get
            {
                return _schädigungsgradSelected;
            }
            set
            {
                _schädigungsgradSelected = value;
                RaisePropertyChanged(() => SchädigungsgradSelected);
            }
        }

        public ObservableCollection<Schadsymptom> AllSchadsymptome
        {
            get
            {
                return _allSchadsymptome;
            }
            set
            {
                _allSchadsymptome = value;
                DataService.AddSchadsymptome(value.ToList());           
                RaisePropertyChanged(() => AllSchadsymptome);
            }
        }

        public ObservableCollection<Schadsymptom> SchadsymptomeFiltered
        {
            get
            {
                return _schadsymptomeFiltered;
            }
            set
            {
                _schadsymptomeFiltered = value;
                RaisePropertyChanged(() => SchadsymptomeFiltered);
            }
        }

        public ObservableCollection<Schadsymptom> SchadsymptomeSelected
        {
            get
            {
                return _schadsymptomeSelected;
            }
            set
            {
                _schadsymptomeSelected = value;
                RaisePropertyChanged(() => SchadsymptomeSelected);
            }
        }

        public string Schadsymptom
        {
            get
            {
                return _schadsymptom;
            }
            set
            {
                _schadsymptom = value;
                RaisePropertyChanged(() => Schadsymptom);
            }
        }


        public Verkehrssicherheit Verkehrssicherheit
        {
            get
            {
                return _verkehrssicherheit;
            }
            set
            {
                if (value==Verkehrssicherheit.ja)
                {
                    Kontrolle.Value.verkehrssicher = true;
                }
                else
                {
                    if (value==Verkehrssicherheit.nein)
                    {
                        Kontrolle.Value.verkehrssicher = false;
                    }
                }
                _verkehrssicherheit = value;
                RaisePropertyChanged(() => Verkehrssicherheit);
            }
        }

        public ObservableCollection<Maßnahmen> AllMaßnahmen
        {
            get
            {
                return _allMaßnahmen;
            }
            set
            {
                _allMaßnahmen = value;
                DataService.AddMaßnahmen(value.ToList());
                RaisePropertyChanged(() => AllMaßnahmen);
            }
        }

        public ObservableCollection<Maßnahmen> MaßnahmenFiltered
        {
            get
            {
                return _maßnahmenFiltered;
            }
            set
            {
                _maßnahmenFiltered = value;
                RaisePropertyChanged(() => MaßnahmenFiltered);
            }
        }

        public ObservableCollection<Maßnahmen> MaßnahmenSelected
        {
            get
            {
                return _maßnahmenSelected;
            }
            set
            {
                _maßnahmenSelected = value;
                RaisePropertyChanged(() => MaßnahmenSelected);
            }
        }

        public string Maßnahmen
        {
            get
            {
                return _maßnahmen;
            }
            set
            {
                _maßnahmen = value;
                RaisePropertyChanged(() => Maßnahmen);
            }
        }


        private async Task SaveAsync()
        {
            Baum.Value.straßeId = StraßeSelected.Value.id;
             
            if (EntwicklungsphaseSelected!=null)
            {
                Kontrolle.Value.entwicklungsphaseID = EntwicklungsphaseSelected.id;
            }

            Baum.Value.baumartId = BaumartSelected.Value.ID;

            if (BaumhöhenbereichSelected!=null)
            {
                Kontrolle.Value.baumhöhe_bereichIDs = BaumhöhenbereichSelected.id;
            }

            foreach (var item in SchadsymptomeSelected)
            {
                Kontrolle.Value.schadsymptome += item.name+"; ";
            }
            if (Kontrolle.Value.schadsymptome!=null)
            {
                Kontrolle.Value.schadsymptome = Kontrolle.Value.schadsymptome.TrimEnd(' ', ';');
            }

            if (SchädigungsgradSelected!=null)
            {
                Kontrolle.Value.schädigungsgradID = SchädigungsgradSelected.id;
            }


            foreach (var item in MaßnahmenSelected)
            {
                Kontrolle.Value.maßnahmen += item.name + "; ";
            }
            if (Kontrolle.Value.maßnahmen!=null)
            {
                Kontrolle.Value.maßnahmen = Kontrolle.Value.maßnahmen.TrimEnd(' ', ';');
            }

            if (AusführenBisSelected!=null)
            {
                Kontrolle.Value.ausführenBisIDs = AusführenBisSelected.id;
            }
                     
            

            _straßeSelected.Validate();
            if (_baum.Validate())
            {
               DataService.SaveBaum(_baum.Value,_kontrolle.Value);
               await DialogService.ShowAlertAsync("Der Baum und die zugehörige Kontrolle wurden erfolgreich gespeichert.", "Info", "OK");
            }
            
        }

        private async Task BackAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
        }

        private void UpdateStraße(TextChangedEventArgs arg)
        {
            List<Straße>  straßenToAdd = new List<Straße>(_allStraßen.Where(w => w.name.StartsWith(arg.NewTextValue,StringComparison.CurrentCultureIgnoreCase)));

            _straßenFiltered.Clear();
            foreach (var item in straßenToAdd)
            {
                _straßenFiltered.Add(item);
            }                      
        }

        private void StraßeWasSelected(SelectedItemChangedEventArgs arg)
        {
            Straße straße = arg.SelectedItem as Straße;
            //Baum.Value.straßeId = straße.id;
            StraßeSelected.Value = straße;
        }

        private async void AddStraße()
        {           
            if (DataService.GetStraßeByName(StraßeSelected.Value.name)!=null)
            {
                await DialogService.ShowAlertAsync("Die Straße \"" + StraßeSelected.Value.name + "\" befindet sich bereits in der Datenbank. Sie muss nicht hinzugefügt werden.","Hinweis","OK");
            }
            else
            {
                if (await DialogService.ShowConfirmAsync("Soll die Straße \"" + StraßeSelected.Value.name + "\" der Datenbank hinzugefügt werden?", "Straße hinzufügen?"))
                {
                    DataService.Insert(StraßeSelected.Value);
                    StraßeSelected.Value = DataService.GetStraßeByName(StraßeSelected.Value.name);

                    Straße straßeToAdd = new Straße();
                    straßeToAdd.name = StraßeSelected.Value.name;
                    straßeToAdd.id = StraßeSelected.Value.id;
                    _allStraßen.Add(straßeToAdd);
                    await DialogService.ShowAlertAsync("Die Straße \"" + StraßeSelected.Value.name + "\" wurde der Datenbank hinzugefügt.", "Hinweis", "OK");
                }
            }
        }

        private void UpdateBaumartDeutsch(TextChangedEventArgs arg)
        {
            List<Baumart> baumartenToAdd = _allBaumarten.Where(w => w.NameDeutsch.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)).ToList();
            //ObservableCollection<Baumart> baumartenToAdd = new ObservableCollection<Baumart>(_allBaumarten.Where(w => w.NameDeutsch.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)));
            _baumartDeutschFiltered.Clear();
            foreach (var item in baumartenToAdd)
            {
                _baumartDeutschFiltered.Add(item);
            }
        }

        private void BaumartDeutschWasSelected(ItemTappedEventArgs arg)
        {
            //Baumart baumart = arg.SelectedItem as Baumart;
            Baumart baumart = arg.Item as Baumart;
            //Baum.Value.baumartId = baumart.ID;
            BaumartSelected.Value = baumart;
        }

        private void UpdateBaumartBotanisch(TextChangedEventArgs arg)
        {
            List<Baumart> baumartenToAdd = _allBaumarten.Where(w => w.NameBotanisch.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)).ToList();
            //ObservableCollection <Baumart> baumartenToAdd = new ObservableCollection<Baumart>(_allBaumarten.Where(w => w.NameBotanisch.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)));
            _baumartBotanischFiltered.Clear();
            foreach (var item in baumartenToAdd)
            {
                _baumartBotanischFiltered.Add(item);
            }
        }

        private void BaumartBotanischWasSelected(ItemTappedEventArgs arg)
        {
            Baumart baumart = arg.Item as Baumart;
            //Baum.Value.baumartId = baumart.ID;
            BaumartSelected.Value = baumart;
        }

        private async void AddBaumart()
        {
            if (DataService.GetBaumartByName(BaumartSelected.Value.NameDeutsch, BaumartSelected.Value.NameBotanisch) != null)
            {
                await DialogService.ShowAlertAsync("Die Baumart \"" + BaumartSelected.Value.NameDeutsch +" - " + BaumartSelected.Value.NameBotanisch + "\" befindet sich bereits in der Datenbank. Sie muss nicht hinzugefügt werden.", "Hinweis", "OK");
            }
            else
            {
                if (await DialogService.ShowConfirmAsync("Soll die Baumart \"" + BaumartSelected.Value.NameDeutsch + " - " + BaumartSelected.Value.NameBotanisch + "\" der Datenbank hinzugefügt werden?", "Baumart hinzufügen?"))
                {
                    DataService.Insert(BaumartSelected.Value);
                    BaumartSelected.Value = DataService.GetBaumartByName(BaumartSelected.Value.NameDeutsch, BaumartSelected.Value.NameBotanisch);

                    Baumart baumartToAdd = new Baumart();
                    baumartToAdd.NameDeutsch = BaumartSelected.Value.NameDeutsch;
                    baumartToAdd.NameBotanisch = BaumartSelected.Value.NameBotanisch;
                    baumartToAdd.ID = BaumartSelected.Value.ID;
                    _allBaumarten.Add(baumartToAdd);
                    await DialogService.ShowAlertAsync("Die Baumart \"" + BaumartSelected.Value.NameDeutsch + " - " + BaumartSelected.Value.NameBotanisch + "\" wurde der Datenbank hinzugefügt.", "Hinweis", "OK");
                }
            }
        }

        private void DecreaseBaumNr()
        {
            if (_baum.Value.baumNr>0)
            {
                _baum.Value.baumNr--;               
            }
        }

        private void IncreaseBaumNr()
        {
            _baum.Value.baumNr++;          
        }

        private void DecreasePlakettenNr()
        {
            if ( _baum.Value.plakettenNr > 0)
            {
                _baum.Value.plakettenNr--;
            }
        }

        private void IncreasePlakettenNr()
        {
            _baum.Value.plakettenNr++;
        }

        public void BaumhöheCompleted()
        {
            int untere_grenze;
            int obere_grenze;
            string[] var;
            int höhe=(int) _kontrolle.Value.baumhöhe;
            foreach (var item in _allBaumhöhenbereiche)
            {
                if (item.name.Contains(">"))
                {
                    _baumhöhenbereichSelected = item;
                    RaisePropertyChanged(() => BaumhöhenbereichSelected);
                    break;
                }
                else
                {
                    var = item.name.Split('-');
                    untere_grenze = Convert.ToInt32(var[0]);
                    obere_grenze = Convert.ToInt32(var[1]);
                }

               
                if (höhe >= untere_grenze && höhe<obere_grenze)
                {
                    
                    _baumhöhenbereichSelected = item;
                    RaisePropertyChanged(() => BaumhöhenbereichSelected);
                    break;
                }
            }
        }

        private void UpdateSchadsymptom(TextChangedEventArgs arg)
        {
            List<Schadsymptom> schadsymptomToAdd = AllSchadsymptome.Where(w => w.name.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)).ToList();

            SchadsymptomeFiltered.Clear();
            foreach (var item in schadsymptomToAdd)
            {
                SchadsymptomeFiltered.Add(item);
            }
        }

        public async void SchadsymptomHinzufügen(string parameter)
        {
            if (parameter==null)
            {
                await DialogService.ShowAlertAsync("Es muss ein Schadsymptom eingegeben werden, um es hinzuzufügen.", "Hinweis", "OK");
                return;
            }
            if (SchadsymptomeSelected.Where(s=>s.name==parameter).ToList().Count != 0)
            {
                if (!await DialogService.ShowConfirmAsync("Es wurde bereits ein Schadsymptom mit dem eingegebenen Namen ausgewählt, soll es nochmal hinzugefügt werden?","Hinweis"))
                {
                    return;
                }
            }

            Schadsymptom schadsymptom = DataService.GetSchadsymptom(parameter);
            if (schadsymptom==null)
            {
                schadsymptom = new Schadsymptom();
                schadsymptom.name = parameter;
                if (await DialogService.ShowConfirmAsync("Soll das Schadsymptom \"" + parameter + "\" der Datenbank hinzugefügt werden, um später als Vorschlag angezeigt zu werden?", "Schadsymptom Hinzufügen"))
                {
                    DataService.AddSchadsymptome(new List<Schadsymptom> { schadsymptom });
                    schadsymptom = DataService.GetSchadsymptom(parameter);
                    AllSchadsymptome.Add(schadsymptom);              
                }
            }
            SchadsymptomeSelected.Add(schadsymptom);
            Schadsymptom = "";
        }

        private async void SchadsymptomWasSelected(ItemTappedEventArgs arg)
        {
            Schadsymptom schadsymptom = arg.Item as Schadsymptom;
            if (!SchadsymptomeSelected.Contains(schadsymptom))
            {
                SchadsymptomeSelected.Add(schadsymptom);
            }     
            else
            {
                await DialogService.ShowAlertAsync("Das Schadsymptom wurde bereits hinzugefügt.", "Hinweis", "OK");
            }
            
        }

        private void DeleteSchadsymptomSelected(object obj)
        {
            Schadsymptom schadsymptom = obj as Schadsymptom;
            SchadsymptomeSelected.Remove(schadsymptom);
        }


        public void ChangeVerkehrssicherheit()
        {
            if (Verkehrssicherheit==Verkehrssicherheit.ungesetzt)
            {
                Verkehrssicherheit = Verkehrssicherheit.ja;
            }
            else
            {
                if (Verkehrssicherheit==Verkehrssicherheit.nein)
                {
                    Verkehrssicherheit = Verkehrssicherheit.ja;
                }
                else
                {
                    Verkehrssicherheit = Verkehrssicherheit.nein;
                }
            }
        }

        private void UpdateMaßnahmen(TextChangedEventArgs arg)
        {
            List<Maßnahmen> maßnahmenToAdd = AllMaßnahmen.Where(w => w.name.StartsWith(arg.NewTextValue, StringComparison.CurrentCultureIgnoreCase)).ToList();

            MaßnahmenFiltered.Clear();
            foreach (var item in maßnahmenToAdd)
            {
                MaßnahmenFiltered.Add(item);
            }
        }

        public async void MaßnahmenHinzufügen(string parameter)
        {

            if (parameter == null || parameter.Length==0)
            {
                await DialogService.ShowAlertAsync("Es muss eine Maßnahme eingegeben werden, um sie hinzuzufügen.", "Hinweis", "OK");
                return;
            }
            if (MaßnahmenSelected.Where(s => s.name == parameter).ToList().Count != 0)
            {
                if (!await DialogService.ShowConfirmAsync("Es wurde bereits eine Maßnahme mit dem eingegebenen Namen ausgewählt, soll sie nochmal hinzugefügt werden?", "Hinweis"))
                {
                    return;
                }
            }

            Maßnahmen maßnahme = DataService.GetMaßnahmen(parameter);
            if (maßnahme == null)
            {
                maßnahme = new Maßnahmen();
                maßnahme.name = parameter;
                if (await DialogService.ShowConfirmAsync("Soll die Maßnahme \"" + parameter + "\" der Datenbank hinzugefügt werden, um später als Vorschlag angezeigt zu werden?", "Maßnahme Hinzufügen"))
                {
                    DataService.AddMaßnahmen(new List<Maßnahmen> { maßnahme });
                    maßnahme = DataService.GetMaßnahmen(parameter);
                    AllMaßnahmen.Add(maßnahme);
                }
            }
            MaßnahmenSelected.Add(maßnahme);
            Maßnahmen = "";


        }

        private async void MaßnahmenWasSelected(ItemTappedEventArgs arg)
        {
            Maßnahmen maßnahmen = arg.Item as Maßnahmen;
            if (!MaßnahmenSelected.Contains(maßnahmen))
            {
                MaßnahmenSelected.Add(maßnahmen);
            }
            else
            {
                await DialogService.ShowAlertAsync("Die Maßnahme wurde bereits hinzugefügt.", "Hinweis", "OK");
            }
                
        }

        private void DeleteMaßnahmeSelected(object obj)
        {
            Maßnahmen maßnahme = obj as Maßnahmen;
            MaßnahmenSelected.Remove(maßnahme);
            
        }
    }
}
