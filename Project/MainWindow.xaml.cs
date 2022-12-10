using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Song> songList;
        public Song CurrentSong;
        public MainWindow()
        {
            InitializeComponent();

            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Song>));
            using (Stream s = File.OpenRead("d:/songList.xml")) songList = (ObservableCollection<Song>)xs.Deserialize(s);

            /*
            songList = new ObservableCollection<Song>()
            {
                new Song(){Title="Stairway to Heaven", Author="Led Zeppelin", IsFavourite=true},
                new Song(){Title="Sweet Child O' Mine", Author="Guns N' Roses", IsFavourite=true},
                new Song(){Title="Welcome To The Jungle", Author="Guns N' Roses", IsFavourite=true},
                new Song(){Title="Miracle Man", Author="Oliver Tree", IsFavourite=false},
                new Song(){Title="Cash Machine", Author="Oliver Tree", IsFavourite=true},
            };
            */

            lvSongs.ItemsSource = songList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvSongs.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Author", ListSortDirection.Ascending));

        }

        public void addItemToList()
        {
            DataChecker();
            songList.Add(CurrentSong);
            SaveHandler();
        }

        public void DataChecker()
        {
            if (CurrentSong.Author == null || CurrentSong.Author == "" || CurrentSong.Title == null || CurrentSong.Title == "")
            {
                MessageBox.Show("Incorrect data");
                return;
            }
            SaveHandler();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            SaveHandler();
            MessageBox.Show("Saved.");
        }

        public void SaveHandler()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Song>));
            using (Stream s = File.Create("d:/songList.xml")) xs.Serialize(s, songList);
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CurrentSong = new Song();
            AddWindow addWindow = new AddWindow(this);
            addWindow.Show();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            CurrentSong = (Song)lvSongs.SelectedItem;
            if (CurrentSong == null) return;
            EditWindow editWindow = new EditWindow(this);
            editWindow.Show();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Song selSong = (Song)lvSongs.SelectedItem;
            if(selSong == null) return;
            songList.Remove(selSong);
            SaveHandler();
        }
    }
}
