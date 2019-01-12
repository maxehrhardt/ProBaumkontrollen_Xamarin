using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProBaumkontrollen.Services;
using Xamarin.Forms;
using System.IO;
using ProBaumkontrollen.Droid;
using System.Threading.Tasks;

[assembly: Dependency(typeof(FileHelper))]
namespace ProBaumkontrollen.Droid
{
    public class FileHelper : IFileHelper
    {
        readonly string ProjectFolderPath = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator + "ProBaumkontrollen" + Java.IO.File.Separator;

        public async Task<string> GetLocalDatabasePath(string filename)
        {
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(GetDatabaseFolderPath(), filename);           
        }

        public async void DeleteFile(string name)
        {

            File.Delete( await GetLocalDatabasePath(name));
        }

        public async Task<bool> CheckForFile(string filename)
        {
            return File.Exists(await GetLocalDatabasePath(filename));
        }

        public async Task<List<string>> GetAllDatabaseFiles()
        {
            return Directory.GetFiles(GetDatabaseFolderPath()).ToList();
            // Version that works if files are stored in removable storage
            //return Directory.GetFiles(ProjectFolderPath, "*.sqlite").ToList();
        }

        private string GetDatabaseFolderPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData, System.Environment.SpecialFolderOption.Create);
        }

        #region Experimental
        public async void CreateProjectFolder()
        {
            if (!Directory.Exists(ProjectFolderPath))
            {
                Directory.CreateDirectory(ProjectFolderPath);
            }
        }

        /// <summary>
        /// Alternative to CrossFilePicker
        /// </summary>
        /// <returns></returns>
        public async Task<string> PickFile()
        {

            string path = Android.OS.Environment.ExternalStorageDirectory + Java.IO.File.Separator;
            var filesList = Directory.GetFiles(path);
            return "";
        } 
        #endregion
    }
}