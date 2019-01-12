using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProBaumkontrollen.ViewModels.Base;
using ProBaumkontrollen.Services;
using System.Threading.Tasks;

using ProBaumkontrollen.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProBaumkontrollen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitApp();

            if (Device.RuntimePlatform == Device.UWP)
            {
                //MainPage = new NavigationPage(new MainView());
                InitNavigation();
            }
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());


        }

        private void InitApp()
        {
            //UseMockServices = Settings.UseMocks;
            //ViewModelLocator.RegisterDependencies(UseMockServices);
            ViewModelLocator.RegisterDependencies(false);
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
