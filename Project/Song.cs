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
        private string _songId;
        private string _title;
        private string _lyrics;
        private string _coverPath;
        private string _authorId;
        private string _authorName;

        public string SongId
        {
            get { return _songId; }
            set 
            { 
                if(_songId != value)
                {
                    _songId = value;
                    NotifyPropertyChanged("SongId", value);
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged("Title", value);
                }
            }
        }

        public string Lyrics
        {
            get { return _lyrics; }
            set
            {
                if (_lyrics != value)
                {
                    _lyrics = value;
                    NotifyPropertyChanged("Lyrics", value);
                }
            }
        }

        public string CoverPath
        {
            get { return _coverPath; }
            set
            {
                if (_coverPath != value)
                {
                    _coverPath = "Images/Songs/" + value + ".jpg";
                    NotifyPropertyChanged("CoverPath", value);
                }
            }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set
            {
                if (_authorName != value)
                {
                    _authorName = value;
                    NotifyPropertyChanged("AuthorName", value);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName, string value)
        {
            if(PropertyChanged != null)
            {
                if (propName == "Auhtor") propName = "AuthorName";
                Database.RunOneWayQuery($"UPDATE Songs SET {propName}='{Database.formatStr(value)}' WHERE SongId = {this.SongId}");
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
