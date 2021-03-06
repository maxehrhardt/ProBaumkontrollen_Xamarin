﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ProBaumkontrollen.Services;
using System.IO;
using ProBaumkontrollen.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace ProBaumkontrollen.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public void DeleteFile(string name)
        {

            File.Delete(GetLocalFilePath(name));
        }
    }
}