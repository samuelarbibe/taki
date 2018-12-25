using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Form.TakiService;

namespace Form.UserControls
{
    /// <summary>
    /// Inter_action logic for TableUC.xaml
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
        public CardList Stack { get => _stack; set => _stack = value; }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            DataContext = Stack.LastOrDefault();
        }


        public TableUC()
        {
            InitializeComponent();
        }


        public Card GetCardFromStack() {

            if (Deck.Count == 0)
            {
                Deck = MainWindow.Service.BuildDeck();
            }

            Card temp = Deck.Last();
            Deck.Remove(temp);
            return temp;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            Stack = CurrentPlayer.Hand;

            DataContext = Stack.LastOrDefault();

            Deck = MainWindow.Service.BuildDeck();
        }
        
        public void DeckButton_OnClick(object sender, EventArgs e)
        {
            //Null check makes sure the main page is attached to the event
            TakeCardFromDeckButtonClicked?.Invoke(this, EventArgs.Empty);
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
            this.PassCardToStackButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
