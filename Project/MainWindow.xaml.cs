using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
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
        private ObservableCollection<Song> songList = new ObservableCollection<Song>();
        public Song CurrentSong;
        public MainWindow()
        {
            InitializeComponent();

            //ConnectDB();
            FetchData();

        }

        public void FetchData()
        {
            songList = Database.RunProcedure<Song>("GetAllSongs");

            lvSongs.ItemsSource = songList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvSongs.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Author", ListSortDirection.Ascending));
        }

        private void ConnectDB()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost;Database=SongsDB;Trusted_Connection=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            String sql;

            sql = "SELECT * FROM Songs";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                songList.Add(new Song(){ 
                SongId = (string)dataReader["SongId"],
                Title = (string)dataReader["Title"],
                Lyrics = (string)dataReader["Lyrics"],
                CoverPath = (string)dataReader["CoverPath"], 
                AuthorName = (string)dataReader["AuthorName"],
                });
            }

            dataReader.Close();
            cnn.Close();
        }

        private void SongsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Song;
            if (item != null)
            {
                new SongDisplay(item, this).Show();
            }
        }     
        
        private void OnAddSong(object sender, RoutedEventArgs e)
        {
            new AddSong(this).Show();
        }

        private void OnFetchData(object sender, RoutedEventArgs e)
        {
            FetchData();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.50;
            var col2 = 0.50;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }

    }
}
