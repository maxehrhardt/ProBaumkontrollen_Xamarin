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
        public ICommand ChooseUserCommand => new Command(async () => await ChooseUser());
        public ICommand AddUserCommand => new Command(async () => await AddUser());
        public ICommand ChooseProjectCommand => new Command(async () => await ChooseProject());
        public ICommand AddProjectCommand => new Command(async () => await AddProject());

        #region Properties
        private string _selectedUserName;
        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set
            {
                _selectedUserName = value;
                RaisePropertyChanged(() => SelectedUserName);
            }
        }

        private string _enteredUserName;
        public string EnteredUserName
        {
            get { return _enteredUserName; }
            set
            {
                _enteredUserName = value;
                RaisePropertyChanged(() => EnteredUserName);
            }
        }

        private string _activeUser;
        public string ActiveUser
        {
            get { return _activeUser; }
            set
            {
                _activeUser = value;
                RaisePropertyChanged(() => ActiveUser);
            }
        }

        private ObservableCollection<string> _allUsers;
        public ObservableCollection<string> AllUsers
        {
            get { return _allUsers; }
            set
            {
                _allUsers = value;
                RaisePropertyChanged(() => AllUsers);
            }
        }

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
            // Only for Test purpose, normally all projects in internal app storage should be loaded
            // and the active project shpuld be read out the app settings
            AllProjects = new ObservableCollection<string>();
            AllProjects.Add("ErstesProjekt");

            ActiveProject = "";

            AllUsers = new ObservableCollection<string>();
            AllUsers.Add("Max");
            ActiveUser = "";

        }

        private async Task ChooseUser()
        {
            ActiveUser = SelectedUserName;
        }

        private async Task AddUser()
        {
            // Check for validity 
            if (string.IsNullOrWhiteSpace(_enteredUserName))
            {
                await DialogService.ShowAlertAsync("Es muss ein Benutzer angegeben werden.", "Warnung", "OK");
                return;
            }

            if (_allUsers.Contains(_enteredUserName))
            {
                await DialogService.ShowAlertAsync("Der Benutzer \"" + _enteredUserName + "\" existiert bereits. Bitte geben sie einen anderen Namen ein.", "Warnung", "OK");
                return;
            }

            _allUsers.Add(_enteredUserName);
            ActiveUser = _enteredUserName;
            await DialogService.ShowAlertAsync("Der Benutzer \"" + _enteredUserName + "\" wurde angelegt.", "Hinweis", "OK");
            //RaisePropertyChanged(() => AllUsers);
            //DataService.CreateUser();
        }

        private async Task ChooseProject()
        {
            ActiveProject = SelectedProjectName;
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
