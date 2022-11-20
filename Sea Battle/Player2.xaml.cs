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
using System.Windows.Shapes;

namespace Sea_Battle
{
    /// <summary>
    /// Interaction logic for Player2.xaml
    /// </summary>
    public partial class Player2 : Window
    {
        public Player2()
        {
            InitializeComponent();
        }

        private void Button_Click_Set(object sender, RoutedEventArgs e)
        {
            if (((PlayerBoards)this.DataContext).gameStarted || ((PlayerBoards)this.DataContext).readyPlayer2) return;
            Button btn = (Button)sender;
            int i = Grid.GetColumn(btn) - 1 + 10 * (Grid.GetRow(btn) - 1);
            ((PlayerBoards)this.DataContext).Set(((PlayerBoards)this.DataContext).BoardPlayer2, i);
        }

        private void Button_Click_Shoot(object sender, RoutedEventArgs e)
        {
            if (((PlayerBoards)this.DataContext).turnPlayer1 || !((PlayerBoards)this.DataContext).gameStarted) return;
            Button btn = (Button)sender;
            int i = Grid.GetColumn(btn) - 1 + 10 * (Grid.GetRow(btn) - 1);
            ((PlayerBoards)this.DataContext).Shoot(((PlayerBoards)this.DataContext).BoardPlayer1, ((PlayerBoards)this.DataContext).EnemyPlayer2 , i, 2);
            if (((PlayerBoards)this.DataContext).winner > 0) MessageBox.Show("Game finished. Winner: Player " + ((PlayerBoards)this.DataContext).winner);
        }

        private void Button_Click_Ready(object sender, RoutedEventArgs e)
        {
            if (((PlayerBoards)this.DataContext).readyPlayer2) return;

            string checkResult = ((PlayerBoards)this.DataContext).LayoutIsValid(((PlayerBoards)this.DataContext).BoardPlayer2, false);
            if (checkResult == "")
            {
                MessageBox.Show("Alignment is valid! " + ((PlayerBoards)this.DataContext).getShipTilesLocation(false));
                ((PlayerBoards)this.DataContext).readyPlayer2 = true;
            }
            else
            {
                MessageBox.Show("Invalid Layout!\n" + checkResult);
            }
        }
    }
}
