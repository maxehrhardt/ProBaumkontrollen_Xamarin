using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProBaumkontrollen.ViewModels;
using ProBaumkontrollen.ViewModels.Base;


namespace ProBaumkontrollen.Views
{
    
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            //Initialize Visual States for User entry
            VisualStateManager.GoToState(userEntryStack, "NormalState");
            foreach (View child in userEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "NormalState");
            }

            //Initialize Visual States for Project entry
            VisualStateManager.GoToState(projectEntryStack, "NormalState");
            foreach (View child in projectEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "NormalState");
            }
        }

        private void FindProject_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(projectEntryStack, "FindProjectState");
            foreach (View child in projectEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "FindProjectState");
            }
        }

        private void AddProject_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(projectEntryStack, "AddProjectState");
            foreach (View child in projectEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "AddProjectState");
            }
        }

        private void AcceptDeclineProject_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(projectEntryStack, "NormalState");
            foreach (View child in projectEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "NormalState");
            }
        }
        private void FindUser_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(userEntryStack, "FindUserState");
            foreach (View child in userEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "FindUserState");
            }
        }

        private void AddUser_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(userEntryStack, "AddUserState");
            foreach (View child in userEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "AddUserState");
            }
        }

        private void AcceptDeclineUser_Clicked(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(userEntryStack, "NormalState");
            foreach (View child in userEntryStack.Children)
            {
                VisualStateManager.GoToState(child, "NormalState");
            }
        }
    }
}