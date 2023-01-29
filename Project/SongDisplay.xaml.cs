using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project
{
    public partial class SongDisplay : Window
    {
        private bool editMode = false;
        private TextBox[] controls;
        private Song item;
        private MainWindow mw;
        public SongDisplay(Song item, MainWindow mw)
        {
            InitializeComponent();
            this.item = item;
            this.mw = mw;
            DataContext = item;
            controls = new TextBox[] { ttl, ath, lrc };
            //imgPhoto.Source = new BitmapImage(new Uri(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, item.CoverPath)));

            BitmapImage image = new BitmapImage();

            using (var stream = File.OpenRead(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, item.CoverPath)))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            imgPhoto.Source = image;
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            editMode = !editMode;
            setCtrEdit(editMode);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Database.RunOneWayQuery($"Delete from Songs where SongId = '{item.SongId}'");

                Database.RunOneWayProcedure("DeleteSongById", item.SongId);
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, item.CoverPath));
                mw.FetchData();
                this.Close();
                MessageBox.Show($"The song was deleted from the database.");
            }
            
        }

        private void setCtrEdit(bool val)
        {
            int thickness = 0;
            if (val) thickness = 1;
            foreach (TextBox ctr in controls)
            {
                ctr.IsReadOnly = !val;
                ctr.BorderThickness = new Thickness(thickness);
            }
            editBtn.Content = val ? "Save" : "Edit";
        }
    }
}
