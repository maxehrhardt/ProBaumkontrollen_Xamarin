using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProBaumkontrollen.Services;

using Windows.Storage;
using Xamarin.Forms;
using ProBaumkontrollen.UWP;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace ProBaumkontrollen.UWP
{

    public class FileHelper: IFileHelper
    {

        public async Task<string> GetLocalDatabasePath(string filename)
        {
            StorageFolder databaseFolder = await GetDatabaseFolder();
            return Path.Combine(databaseFolder.Path, filename);
        }

        public async void DeleteFile(string name)
        {

            File.Delete(await GetLocalDatabasePath(name));
        }

        public async Task<bool> CheckForFile(string filename)
        {
            return File.Exists(await GetLocalDatabasePath(filename));
        }        

        public async Task<List<string>> GetAllDatabaseFiles()
        {
            StorageFolder databaseFolder = await GetDatabaseFolder();
            IReadOnlyList<StorageFile> fileList = await databaseFolder.GetFilesAsync();

            List<string> fileNameList= new List<string>();

            foreach (var file in fileList)
            {
                fileNameList.Add(file.DisplayName);
            }

            return fileNameList;

            // Version that works if the databases are stored in external storage
            //StorageFolder storageFolder = await Windows.Storage.KnownFolders.RemovableDevices.GetFolderAsync("Probaumkontrollen");
            
            //return Directory.GetFiles(storageFolder.Path, "*.sqlite").ToList();
        }

        private async Task<StorageFolder> GetDatabaseFolder()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            return await localFolder.CreateFolderAsync("Datenbanken", CreationCollisionOption.OpenIfExists);
        }

        #region Experimental
        public async void CreateProjectFolder()
        {
            await Windows.Storage.KnownFolders.DocumentsLibrary.CreateFolderAsync("ProBaumkontrollen", CreationCollisionOption.OpenIfExists);
        }

        public async Task<string> PickFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".sqlite");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            return file?.Path;
        }
        #endregion Experimental
    }
}
