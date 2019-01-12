using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBaumkontrollen.Services
{
    public interface IFileHelper
    {
        Task<string> GetLocalDatabasePath(string filename);
        void DeleteFile(string name);
        Task<bool> CheckForFile(string filename);
        Task<List<string>> GetAllDatabaseFiles();

        void CreateProjectFolder();
        Task<string> PickFile();
    }
}
