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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        GameLogic _GameLogic = new GameLogic();
        bool ClickCanBePerformed = true;

        private void GameFieldClickHandler(object sender, RoutedEventArgs e)
        {
            if (!ClickCanBePerformed) return;

            var field = (Button)sender;
            if (!String.IsNullOrWhiteSpace(field.Content?.ToString())) return;
            field.Content = _GameLogic.CurrentPlayer;

            var coordinates = field.Tag.ToString()?.Split(',');
            var xVal = int.Parse(coordinates[0]);
            var yVal = int.Parse(coordinates[1]);
            var buttonPos = new Position() {x = xVal, y = yVal};
            _GameLogic.UpdateBoard(buttonPos, _GameLogic.CurrentPlayer);

            if (_GameLogic.PlayerWin())
            {
                WinScreen.Text = $"Player {_GameLogic.CurrentPlayer} wins!";
                WinScreen.Visibility = Visibility.Visible;
                ClickCanBePerformed = false;
            }
            _GameLogic.SetNextPlayer();
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            ClickCanBePerformed = true;

            foreach (var el in MainBoard.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Content = String.Empty;
                }
            }
            _GameLogic = new GameLogic();
            WinScreen.Visibility = Visibility.Collapsed;
        }

    }
}
