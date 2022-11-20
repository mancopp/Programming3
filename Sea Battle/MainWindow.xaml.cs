using System;
using System.Collections.Generic;
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

namespace Sea_Battle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player2 win_player2 = new Player2();
        PlayerBoards playerBoards = new PlayerBoards();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = playerBoards;
            win_player2.DataContext = playerBoards;
            win_player2.Show();
        }

        private void Button_Click_Set(object sender, RoutedEventArgs e)
        {
            if (((PlayerBoards)this.DataContext).gameStarted || ((PlayerBoards)this.DataContext).readyPlayer1) return;
            Button btn = (Button)sender;
            int i = Grid.GetColumn(btn) - 1 + 10 * (Grid.GetRow(btn) - 1);
            ((PlayerBoards)this.DataContext).Set(((PlayerBoards)this.DataContext).BoardPlayer1, i);
        }

        private void Button_Click_Shoot(object sender, RoutedEventArgs e)
        {
            if (!((PlayerBoards)this.DataContext).turnPlayer1 || !((PlayerBoards)this.DataContext).gameStarted) return;
            Button btn = (Button)sender;
            int i = Grid.GetColumn(btn) - 1 + 10 * (Grid.GetRow(btn) - 1);
            ((PlayerBoards)this.DataContext).Shoot(((PlayerBoards)this.DataContext).BoardPlayer2, ((PlayerBoards)this.DataContext).EnemyPlayer1, i, 1);
            if (((PlayerBoards)this.DataContext).winner > 0) MessageBox.Show("Game finished. Winner: Player " + ((PlayerBoards)this.DataContext).winner);
        }

        private void Button_Click_Ready(object sender, RoutedEventArgs e)
        {
            if (((PlayerBoards)this.DataContext).readyPlayer1) return;

            string checkResult = ((PlayerBoards)this.DataContext).LayoutIsValid(((PlayerBoards)this.DataContext).BoardPlayer1, true);
            if (checkResult == "")
            {
                MessageBox.Show("Alignment is valid! " + ((PlayerBoards)this.DataContext).getShipTilesLocation(true));
                ((PlayerBoards)this.DataContext).readyPlayer1 = true;
            }
            else
            {
                MessageBox.Show("Invalid Layout!\n" + checkResult);
            }
        }
    }
}
