using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProBaumkontrollen.Models;

namespace ProBaumkontrollen.Services
{
    public interface IDataService
    {
        //Task<string> ChooseProject();
        //void CreateProject();

        void SaveBaum(Baum baum,Kontrolle kontrolle);
        void UpdateBaum(Baum baum, Kontrolle kontrolle);
        IEnumerable<Baum> GetBaumliste();
        IEnumerable<Baumhöhenbereiche> GetAllBaumhöhenbereiche();

        IEnumerable<Straße> GetAllStraßen();
        Straße GetStraßeByID(int id);
        Straße GetStraßeByName(string name);

        IEnumerable<Baumart> GetAllBaumarten();
        Baumart GetBaumartByID(int id);
        Baumart GetBaumartByName(string nameDt, string nameBt);

        IEnumerable<Entwicklungsphase> GetAllEntwicklungsphasen();
        IEnumerable<Schädigungsgrad> GetAllSchädigungsgrade();
        IEnumerable<AusführenBis> GetAllAusführenBis();

        
        
        Entwicklungsphase GetEntwicklungsphaseByID(int id);
        Schädigungsgrad GetSchädigungsgradByID(int id);
        AusführenBis GetAusführenBisByID(int id);
        Baumhöhenbereiche GetBaumhöhenbereichByID(int id);

        Schadsymptom GetSchadsymptom(string name);
        IEnumerable<Schadsymptom> GetAllSchadsymptome();
        void AddSchadsymptome(List<Schadsymptom> list);

        Maßnahmen GetMaßnahmen(string name);
        IEnumerable<Maßnahmen> GetAllMaßnahmen();
        void AddMaßnahmen(List<Maßnahmen> list);

        Kontrolle GetKontrolleByBaumID(int baumID);
        int Insert(object obj);
    }
}
