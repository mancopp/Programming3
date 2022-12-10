using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Song : INotifyPropertyChanged
    {
        private string _title;
        private string _author;
        private bool _isFavourite;
        //private string _coverArtPath;

        public string Title
        {
            get { return _title; }
            set 
            { 
                if(_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }

        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                if (_isFavourite != value)
                {
                    _isFavourite = value;
                    NotifyPropertyChanged("IsFavourite");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
