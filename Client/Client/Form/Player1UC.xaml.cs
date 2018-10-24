using Form.TakiService;
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

namespace Form
{
    /// <summary>
    /// Interaction logic for Player1UC.xaml
    /// </summary>
    public partial class Player1UC : UserControl
    {
        private CardList _hand;
        private Player _currentPlayer;

        public Player1UC() {

            InitializeComponent();
        }

        public Player1UC(Player currentPlayer)
        {
            InitializeComponent();
            _currentPlayer = currentPlayer;
            _hand = _currentPlayer.Hand;

            DataContext = _currentPlayer;

            //cardListItemControl.ItemsSource = _hand;
        }
    }
}
