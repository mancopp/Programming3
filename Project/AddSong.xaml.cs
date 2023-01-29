using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Project
{
	public partial class AddSong : Window
	{
		private string source;
		private string dest;
		private string fileName;
		private MainWindow mw;

		public AddSong(MainWindow mw)
		{
			InitializeComponent();
			this.mw = mw;
		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.jpg)|*.jpg";
			if (openFileDialog.ShowDialog() == true)
			{
				this.source = openFileDialog.FileName;
				this.fileName = Path.GetFileNameWithoutExtension(source);
				this.dest = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Images\Songs\" + Path.GetFileName(source));
				imgPhoto.Source = new BitmapImage(new Uri(source));
				svBtn.IsEnabled = true;
			}
		}

		private void SaveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
				File.Copy(source, dest);
				// Database.RunOneWayQuery($"INSERT INTO Songs VALUES ('{Database.formatStr(tbid.Text)}', '{Database.formatStr(tbtitle.Text)}', '{Database.formatStr(tblyrics.Text)}' , '{Database.formatStr(fileName)}', '{Database.formatStr(tbauthor.Text)}')");
				
				Database.InsertSong(tbid.Text, tbtitle.Text, tbauthor.Text, tblyrics.Text, fileName);
				
				mw.FetchData();
				this.Close();
			}
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
		}
	}
}