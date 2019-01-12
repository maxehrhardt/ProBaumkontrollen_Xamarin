using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Acr.UserDialogs;

namespace ProBaumkontrollen.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {     
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);          
        }

        public Task<bool> ShowConfirmAsync(string message, string title)
        {
            ConfirmConfig config = new ConfirmConfig();
            config.UseYesNo();
            config.Message = message;
            config.Title = title;
            config.OkText = "Ja";
            config.CancelText = "Nein";
            
            return UserDialogs.Instance.ConfirmAsync(config);
        }
        public void ShowMessage()
        {
            //ActionSheetConfig sheet = new ActionSheetConfig();
            //sheet.Add("Hallo");
            //UserDialogs.Instance.ActionSheet(sheet);
            //return UserDialogs.Instance.ActionSheetAsync("Titel","Abbrechen","Zerstören")
        }
    }
}
