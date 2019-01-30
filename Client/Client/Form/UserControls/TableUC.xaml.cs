using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Form.TakiService;
using System.Security.Cryptography;


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
        private Random rand = new Random();
        public event EventHandler TakeCardFromDeckButtonClicked;
        public event EventHandler PassCardToStackButtonClicked;
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        public CardList Deck { get => _deck; set => _deck = value; }
        public CardList Stack { get => _stack; set => _stack = value; }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            DataContext = Deck.LastOrDefault();
        }


        public TableUC()
        {
            InitializeComponent();
        }


        public Card GetCardFromStack() {

            Card temp = Deck[RollDice((byte)(Deck.Count - 2))];
            Deck.Remove(temp);

            return temp;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            Deck = CurrentPlayer.Hand; //set the deck as the table's hand

            DataContext = Deck.LastOrDefault(); // set the last card of the player's hand as the displayed card

        }

        public static byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
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
