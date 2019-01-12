using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using ProBaumkontrollen.ViewModels.Base;
using Xamarin.Forms;
using System.ComponentModel;

namespace ProBaumkontrollen.Models
{
    [Table("tabBaeume")]
    public class Baum : INotifyPropertyChanged
    {
        private int _id ;
        private string _benutzer ;
        private string _projekt ;
        private int _straßeId ;
        private int _baumNr ;
        private int _plakettenNr ;
        private int _baumartId ;
        private string _erstelldatum ;

        [PrimaryKey, AutoIncrement]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id= value;
                OnPropertyChanged(nameof(id));
            }
        }
        public int baumNr
        {
            get
            {
                return _baumNr;
            }
            set
            {
                _baumNr = value;
                OnPropertyChanged(nameof(baumNr));
            }
        }
        
        public string benutzer
        {
            get
            {
                return _benutzer;
            }
            set
            {
                _benutzer = value;
                OnPropertyChanged(nameof(benutzer));
            }
        }
        public string projekt
        {
            get
            {
                return _projekt;
            }
            set
            {
                _projekt = value;
                OnPropertyChanged(nameof(projekt));
            }
        }
        public int straßeId
        {
            get
            {
                return _straßeId;
            }
            set
            {
                _straßeId = value;
                OnPropertyChanged(nameof(straßeId));
            }
        }

        public int plakettenNr
        {
            get
            {
                return _plakettenNr;
            }
            set
            {
                _plakettenNr = value;
                OnPropertyChanged(nameof(plakettenNr));
            }
        }
        public int baumartId
        {
            get
            {
                return _baumartId;
            }
            set
            {
                _baumartId = value;
                OnPropertyChanged(nameof(baumartId));
            }
        }
        public string erstelldatum
        {
            get
            {
                return _erstelldatum;
            }
            set
            {
                _erstelldatum = value;
                OnPropertyChanged(nameof(erstelldatum));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }

    }
}
