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
    /// Interaction logic for TableUC.xaml
    /// </summary>
    public partial class TableUC : UserControl
    {
        private CardList _deck;
        private Player _currentPlayer;
        private CardList _stack;

        public TableUC()
        {
            InitializeComponent();
        }

        //public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            _currentPlayer = currentPlayer;

            _deck = _currentPlayer.Hand;
            _stack = new CardList();

            _stack.Add(_deck.Last());
            _deck.RemoveAt(_deck.Count - 1);

            DataContext = _stack.LastOrDefault();
        }
    }
}
