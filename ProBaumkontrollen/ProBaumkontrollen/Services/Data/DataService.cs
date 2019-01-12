using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProBaumkontrollen.Models;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.IO;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
//using Plugin.FilePicker;
//using Plugin.FilePicker.Abstractions;
//using Plugin.Permissions;
//using Plugin.Permissions.Abstractions;

namespace ProBaumkontrollen.Services
{
    public class DataService : IDataService
    {
        static object locker = new object();
        SQLiteConnection database_connection;
        string databaseFolderPath;

        public DataService()
        {
            CreateTestProject();
            //    // Create The Project directory, (only used when filepicker is used)
            //    CreateProjectDirectory();

            //    // Get the Connection from the specific Platform-Based Projects
            //    //database_connection = DependencyService.Get<ISQLite>().GetConnection();

            //DependencyService.Get<IFileHelper>().DeleteFile("Test.db");
            //if (!DependencyService.Get<IFileHelper>().CheckForFile("Test.db"))
            //{
            //    CreateArbeitsDB("Test");

            //    string db_filepath = DependencyService.Get<IFileHelper>().GetLocalDatabasePath("Test.db");
            //    database_connection = new SQLiteConnection(db_filepath);

            //    //Testdaten einfügen
            //    Straße straße = new Straße();
            //    straße.name = "Teststraße";
            //    database_connection.Insert(straße);
            //    straße.name = "Klausstraße";
            //    database_connection.Insert(straße);

            //    Baumart baumart = new Baumart();
            //    baumart.NameDeutsch = "Spitzahorn";
            //    baumart.NameBotanisch = "Acer platanoides";
            //    database_connection.Insert(baumart);
            //    baumart.NameDeutsch = "Säulenpappel";
            //    baumart.NameBotanisch = "Populus nigra italica";
            //    database_connection.Insert(baumart);
            //    // Create the tables

            //    Schadsymptom schadsymptom = new Schadsymptom();
            //    schadsymptom.name = "Totholz";
            //    database_connection.Insert(schadsymptom);
            //}
            //else
            //{
            //    string db_filepath = DependencyService.Get<IFileHelper>().GetLocalDatabasePath("Test.db");
            //    database_connection = new SQLiteConnection(db_filepath);
            //}
        }

        //public async Task<string> ChooseProject()
        //{


        //    // Version that lets the user pick a file from storage
        //    //FileData fileData = await CrossFilePicker.Current.PickFile();
            
        //    //await CrossFilePicker.Current.PickFile();
        //    //if (fileData == null)
        //    //{
        //    //    return "";
        //    //}
        //    //else
        //    //{
        //    //    string name = fileData.FileName;

        //    //    try
        //    //    {
        //    //        database_connection = new SQLiteConnection(fileData.FilePath);
        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //        return "";
        //    //    }
        //    //    return name;

        //    //}

        //    //string fileName = fileData.FileName;

        //    //if (Device.RuntimePlatform == Device.Android &&
        //    //    !await this.CheckPermissionsAsync())
        //    //{
        //    //    return;
        //    //}
        //    //var pickedFile = await CrossFilePicker.Current.PickFile();

        //    //if (pickedFile!=null)
        //    //{
        //    //    string fileName = pickedFile.FileName;

        //    //}
        //    //string dbFilePath = await DependencyService.Get<IFileHelper>().PickFile();
        //}

        public async void CreateTestProject()
        {
            //DependencyService.Get<IFileHelper>().DeleteFile("Test.db");
            if (! await DependencyService.Get<IFileHelper>().CheckForFile("Test.db"))
            {
                CreateArbeitsDB("Test");

                string db_filepath = await DependencyService.Get<IFileHelper>().GetLocalDatabasePath("Test.db");
                database_connection = new SQLiteConnection(db_filepath);

                //Testdaten einfügen
                Straße straße = new Straße();
                straße.name = "Teststraße";
                database_connection.Insert(straße);
                straße.name = "Klausstraße";
                database_connection.Insert(straße);

                Baumart baumart = new Baumart();
                baumart.NameDeutsch = "Spitzahorn";
                baumart.NameBotanisch = "Acer platanoides";
                database_connection.Insert(baumart);
                baumart.NameDeutsch = "Säulenpappel";
                baumart.NameBotanisch = "Populus nigra italica";
                database_connection.Insert(baumart);
                // Create the tables

                Schadsymptom schadsymptom = new Schadsymptom();
                schadsymptom.name = "Totholz";
                database_connection.Insert(schadsymptom);
            }
            else
            {
                string db_filepath = await DependencyService.Get<IFileHelper>().GetLocalDatabasePath("Test.db");
                database_connection = new SQLiteConnection(db_filepath);
            }
        }

        public async void CreateProject(string projectName)
        {
            string projectFilePath = await DependencyService.Get<IFileHelper>().GetLocalDatabasePath(projectName);

            database_connection = new SQLiteConnection(projectFilePath);
        }

        public async Task<List<string>> GetAllProjects()
        {
            return await DependencyService.Get<IFileHelper>().GetAllDatabaseFiles();
        }

        public void SaveBaum(Baum baum,Kontrolle kontrolle)
        {
            lock (locker)
            {
                database_connection.Insert(baum);
                kontrolle.baumID = baum.id;
                database_connection.Insert(kontrolle);
            }
        }

        public void UpdateBaum(Baum baum, Kontrolle kontrolle)
        {
            lock (locker)
            {
                database_connection.Update(baum);
                database_connection.Update(kontrolle);                           
            }
        }

        public IEnumerable<Baum> GetBaumliste()
        {
            lock (locker)
            {
                return database_connection.Table<Baum>().ToList();
            }
        }

        /// <summary>
        /// Creates a basic database and fills the static tables
        /// </summary>
        /// <param name="projektname"></param>
        public async void CreateArbeitsDB(string projektname)
        {
            string db_filepath = await DependencyService.Get<IFileHelper>().GetLocalDatabasePath(projektname + ".db");
            SQLiteConnection connection_to_arbeitsDB = new SQLiteConnection(db_filepath);

            //Festlegen der Versionsnummer der DB
            connection_to_arbeitsDB.CreateTable<DBVersion>();
            DBVersion dbVersion = new DBVersion
            {
                id = 1,
                version = "2.0"
            };
            connection_to_arbeitsDB.Insert(dbVersion);

            connection_to_arbeitsDB.CreateTable<Baum>();
            connection_to_arbeitsDB.CreateTable<Kontrolle>();

            connection_to_arbeitsDB.CreateTable<Baumart>();
            //Werte müssen aus der Phone internen DB übertragen werden

            connection_to_arbeitsDB.CreateTable<Straße>();

            ////////
            ///Tabelle Baumhöhenbereiche erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<Baumhöhenbereiche>();
            Baumhöhenbereiche baumhöhenbereich = new Baumhöhenbereiche
            {
                id = 1,
                name = "0-5"
            };
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 2;
            baumhöhenbereich.name = "5-10";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 3;
            baumhöhenbereich.name = "10-15";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 4;
            baumhöhenbereich.name = "15-20";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 5;
            baumhöhenbereich.name = "20-25";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 6;
            baumhöhenbereich.name = "25-30";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 7;
            baumhöhenbereich.name = "30-35";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            baumhöhenbereich.id = 8;
            baumhöhenbereich.name = ">35";
            connection_to_arbeitsDB.Insert(baumhöhenbereich);

            ////////
            ///Tabelle Entwicklungsphase erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<Entwicklungsphase>();
            Entwicklungsphase entwicklungsphase = new Entwicklungsphase
            {
                id = 1,
                name = "Jugendphase"
            };
            connection_to_arbeitsDB.Insert(entwicklungsphase);

            entwicklungsphase.id = 2;
            entwicklungsphase.name = "Reifephase";
            connection_to_arbeitsDB.Insert(entwicklungsphase);

            entwicklungsphase.id = 3;
            entwicklungsphase.name = "Alterungsphase";
            connection_to_arbeitsDB.Insert(entwicklungsphase);

            ////////
            ///Tabelle Schadsymptome erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<Schadsymptom>();

            ////////
            ///Tabelle Maßnahmen erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<Maßnahmen>();


            ////////
            ///Tabelle Schädigungsgrad erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<Schädigungsgrad>();
            Schädigungsgrad schädigungsgrad = new Schädigungsgrad
            {
                id = 1,
                name = "Gesund"
            };
            connection_to_arbeitsDB.Insert(schädigungsgrad);

            schädigungsgrad.id = 2;
            schädigungsgrad.name = "Leicht geschädigt";
            connection_to_arbeitsDB.Insert(schädigungsgrad);

            schädigungsgrad.id = 3;
            schädigungsgrad.name = "Stärker geschädigt";
            connection_to_arbeitsDB.Insert(schädigungsgrad);

  

            ////////
            ///Tabelle AusführenBis erstellen
            ////////
            connection_to_arbeitsDB.CreateTable<AusführenBis>();
            AusführenBis ausführenBis = new AusführenBis
            {
                id = 1,
                name = "Sofort"
            };
            connection_to_arbeitsDB.Insert(ausführenBis);

            ausführenBis.id = 2;
            ausführenBis.name = "Zeitnah";
            connection_to_arbeitsDB.Insert(ausführenBis);

            ausführenBis.id = 3;
            ausführenBis.name = "Bis zur nächsten Kontrolle";
            connection_to_arbeitsDB.Insert(ausführenBis);


            connection_to_arbeitsDB.Close();
        }


        #region Functions on Database Elements
        public IEnumerable<Baumhöhenbereiche> GetAllBaumhöhenbereiche()
        {
            lock (locker)
            {
                return database_connection.Table<Baumhöhenbereiche>().ToList();
            }
        }

        public IEnumerable<Straße> GetAllStraßen()
        {
            lock (locker)
            {
                return database_connection.Table<Straße>().ToList();
            }
        }

        public Straße GetStraßeByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<Straße>("SELECT * FROM tabStrassen WHERE id=?", id).ToList().FirstOrDefault();
            }
        }

        public Straße GetStraßeByName(string name)
        {
            lock (locker)
            {
                return database_connection.Query<Straße>("SELECT * FROM tabStrassen WHERE name=?", name).ToList().FirstOrDefault();
            }
        }
        public IEnumerable<Baumart> GetAllBaumarten()
        {
            lock (locker)
            {
                return database_connection.Table<Baumart>().ToList();
            }
        }

        public IEnumerable<Entwicklungsphase> GetAllEntwicklungsphasen()
        {
            lock (locker)
            {
                return database_connection.Table<Entwicklungsphase>().ToList();
            }
        }

        public IEnumerable<Schädigungsgrad> GetAllSchädigungsgrade()
        {
            lock (locker)
            {
                return database_connection.Table<Schädigungsgrad>().ToList();
            }
        }

        public IEnumerable<AusführenBis> GetAllAusführenBis()
        {
            lock (locker)
            {
                return database_connection.Table<AusführenBis>().ToList();
            }
        }



        public Baumart GetBaumartByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<Baumart>("SELECT * FROM tabBaumart WHERE id=?", id).ToList().FirstOrDefault();
            }
        }

        public Baumart GetBaumartByName(string nameDt, string nameBt)
        {
            lock (locker)
            {
                return database_connection.Query<Baumart>("SELECT * FROM tabBaumart WHERE NameDeutsch=? AND NameBotanisch=?", nameDt,nameBt).ToList().FirstOrDefault();
            }
        }

        public Entwicklungsphase GetEntwicklungsphaseByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<Entwicklungsphase>("SELECT * FROM tabEntwicklungsphase WHERE id=?", id).ToList().FirstOrDefault();
            }
        }

        public Schädigungsgrad GetSchädigungsgradByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<Schädigungsgrad>("SELECT * FROM tabSchaedigungsgrad WHERE id=?", id).ToList().FirstOrDefault();
            }
        }

        public AusführenBis GetAusführenBisByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<AusführenBis>("SELECT * FROM tabAusfuehrenBis WHERE id=?", id).ToList().FirstOrDefault();
            }
        }

        public Baumhöhenbereiche GetBaumhöhenbereichByID(int id)
        {
            lock (locker)
            {
                return database_connection.Query<Baumhöhenbereiche>("SELECT * FROM tabBaumhoehenbereiche WHERE id=?", id).ToList().FirstOrDefault();
            }
        }


        public Schadsymptom GetSchadsymptom(string name)
        {
            lock(locker)
            {
                return database_connection.Query<Schadsymptom>("SELECT * FROM tabSchadsymptome WHERE name=?", name).FirstOrDefault();
            }
        }

        public IEnumerable<Schadsymptom> GetAllSchadsymptome()
        {
            lock(locker)
            {
                return database_connection.Table<Schadsymptom>().ToList();
            }
        }

        public void AddSchadsymptome(List<Schadsymptom> list)
        {
            lock (locker)
            {
                foreach (var item in list)
                {
                    if (database_connection.Query<Schadsymptom>("SELECT * FROM tabSchadsymptome WHERE name=?", item.name).FirstOrDefault() == null)
                    {
                        database_connection.Insert(item);
                    }
                }
            }
        }

        public Maßnahmen GetMaßnahmen(string name)
        {
            lock (locker)
            {
                return database_connection.Query<Maßnahmen>("SELECT * FROM tabMassnahmen WHERE name=?", name).FirstOrDefault();
            }
        }

        public IEnumerable<Maßnahmen> GetAllMaßnahmen()
        {
            lock (locker)
            {
                return database_connection.Table<Maßnahmen>().ToList();
            }
        }

        public void AddMaßnahmen(List<Maßnahmen> list)
        {
            lock (locker)
            {
                foreach (var item in list)
                {
                    if (database_connection.Query<Maßnahmen>("SELECT * FROM tabMassnahmen WHERE name=?", item.name).FirstOrDefault() == null)
                    {
                        database_connection.Insert(item);
                    }
                }
            }
        }

        public Kontrolle GetKontrolleByBaumID(int baumID)
        {
            lock (locker)
            {
                return database_connection.Query<Kontrolle>("SELECT * FROM tabKontrolle WHERE baumID=?", baumID).ToList().FirstOrDefault();
            }
        }

        #endregion

        public int Insert(object obj)
        {
            return database_connection.Insert(obj);
        }
        //public IEnumerable<Kontrolle> GetKontrollenToBaum(Baum baum)
        //{
        //    lock(locker)
        //    {
        //        return ;
        //    }
        //}

        //public IEnumerable<string> GetAllKronenzustände()
        //{
        //    lock (locker)
        //    {
        //        return;
        //    }
        //}

    }
}
