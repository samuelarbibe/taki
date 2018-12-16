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
using System.Windows.Media.Animation;
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
        public event EventHandler TakeCardFromDeckButtonClicked;
        public event EventHandler PassCardToStackButtonClicked;



        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        public CardList Deck { get => _deck; set => _deck = value; }

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

            _stack = CurrentPlayer.Hand;

            DataContext = _stack.LastOrDefault();
        }
        
        public void DeckButton_OnClick(object sender, EventArgs e)
        {
            //Null check makes sure the main page is attached to the event
            if (this.TakeCardFromDeckButtonClicked != null)
                this.TakeCardFromDeckButtonClicked(this, EventArgs.Empty);
        }

        public void CanTakeCardFromDeck()
        {
            DeckButton.IsHitTestVisible = true;
            PassCardButton.IsHitTestVisible = true;
        }

        public void CannotTakeCardFromDeck()
        {
            DeckButton.IsHitTestVisible = false;
            PassCardButton.IsHitTestVisible = false;
        }

        private void PassCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PassCardToStackButtonClicked != null)
                this.PassCardToStackButtonClicked(this, EventArgs.Empty);
        }
    }
}
