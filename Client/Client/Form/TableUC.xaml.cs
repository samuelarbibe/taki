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
        public event EventHandler StatusUpdated;

        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;
            DataContext = _stack.LastOrDefault();
        }


        public TableUC()
        {
            InitializeComponent();
        }


        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            _deck = CurrentPlayer.Hand;
            _stack = new CardList();

            _stack.Add(_deck.First());


            _deck.RemoveAt(0);

            DataContext = _stack.LastOrDefault();
        }
        
        private void DeckButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Null check makes sure the main page is attached to the event
            StatusUpdated?.Invoke(this, new EventArgs());
        }
    }
}
