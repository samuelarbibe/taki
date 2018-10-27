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
using Form.TakiService;

namespace Form
{
    /// <summary>
    /// Interaction logic for Player4UC.xaml
    /// </summary>
    public partial class Player4UC : UserControl
    {
        private Player _currentPlayer;
        private CardList _hand;

        public Player4UC()
        {
            InitializeComponent();
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            _currentPlayer = currentPlayer;

            _hand = currentPlayer.Hand;

            DataContext = _currentPlayer;
        }
    }
}
