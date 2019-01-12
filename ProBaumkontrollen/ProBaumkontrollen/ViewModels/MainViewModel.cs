using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProBaumkontrollen.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using ProBaumkontrollen.Validations;

namespace ProBaumkontrollen.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());
        public ICommand NeuaufnahmeCommand => new Command(async () => await NeuaufnahmeAsync());
        public ICommand BaumlisteCommand => new Command(async () => await BaumlisteAsync());
        public ICommand KontrolleCommand => new Command(async () => await KontrolleAsync());
        public ICommand ChooseProjectCommand => new Command(async () => await ChooseProject());
        public ICommand AddProjectCommand => new Command(async () => await AddProject());




        #region Properties

        private string _selectedProjectName;
        public string SelectedProjectName
        {
            get { return _selectedProjectName; }
            set
            {
                _selectedProjectName = value;
                RaisePropertyChanged(() => SelectedProjectName);
            }
        }

        private string _enteredProjectName;
        public string EnteredProjectName
        {
            get { return _enteredProjectName; }
            set
            {
                _enteredProjectName = value;
                RaisePropertyChanged(() => EnteredProjectName);
            }
        }

        private string _activeProject;
        public string ActiveProject
        {
            get { return _activeProject; }
            set
            {
                _activeProject = value;
                RaisePropertyChanged(() => ActiveProject);
            }
        }

        private ObservableCollection<string> _allProjects;
        public ObservableCollection<string> AllProjects
        {
            get { return _allProjects; }
            set
            {
                _allProjects = value;
                RaisePropertyChanged(() => AllProjects);
            }
        }
        #endregion Properties


        public MainViewModel()
        {
            AllProjects = new ObservableCollection<string>();
            AllProjects.Add("ErstesProjekt");

            ActiveProject = "";

        }

        private async Task ChooseProject()
        {
            ActiveProject = SelectedProjectName;
            //_activeProject = await DataService.ChooseProject();
        }

        private async Task AddProject()
        {
            // Check for validity 
            if (string.IsNullOrWhiteSpace(_enteredProjectName))
            {
                await DialogService.ShowAlertAsync("Es muss ein Projektname angegeben werden.","Warnung", "OK");
                return;
            }

            if (_allProjects.Contains(_enteredProjectName))
            {
                await DialogService.ShowAlertAsync("Das Projekt \""+_enteredProjectName+"\" existiert bereits. Bitte geben sie einen anderen Namen ein.","Warnung", "OK");
                return;
            }

            _allProjects.Add(_enteredProjectName);
            ActiveProject = _enteredProjectName;
            await DialogService.ShowAlertAsync("Das Projekt \"" + _enteredProjectName + "\" wurde angelegt.", "Hinweis", "OK");
            //RaisePropertyChanged(() => AllProjects);
            //DataService.CreateProject();
        }

        #region Navigation
        private async Task SettingsAsync()
        {
            await NavigationService.NavigateToAsync<SettingsViewModel>();
        }

        private async Task NeuaufnahmeAsync()
        {
            await NavigationService.NavigateToAsync<NeuaufnahmeViewModel>();
        }

        private async Task BaumlisteAsync()
        {
            await NavigationService.NavigateToAsync<BaumlisteViewModel>();
        }

        private async Task KontrolleAsync()
        {
            await NavigationService.NavigateToAsync<KontrolleFilterViewModel>();
        }

        #endregion Navigation
    }
}
