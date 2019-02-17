using System;
using System.Windows.Controls;
using System.Windows.Media;
using Form.TakiService;

namespace Form.UserControls
{
    /// <summary>
    /// Inter_action logic for Player1UC.xaml
    /// </summary>
    public partial class Player1UC : UserControl
    {
        private CardList _hand;
        private Player _currentPlayer;
        private bool _active;


        public Player1UC() {

            InitializeComponent();
        }

        public event EventHandler ProfileButtonClicked;
        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        public bool Active { get => _active; set => _active = value; }
        public CardList Hand { get => _hand; set => _hand = value; }

        public Card SelectedCard()
        {
            try
            {
                return (Card)HandView.SelectedItems[0];
            }
            catch
            {
                return null;
            }
        }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            DataContext = CurrentPlayer;

            HandView.ItemsSource = null;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }

        public void SetAsActive()
        {
            BackgroundActive.Fill = new SolidColorBrush(Color.FromArgb(90, 0, 250, 0));
        }

        public void SetAsNonActive()
        {
            BackgroundActive.Fill = null;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {

            CurrentPlayer = currentPlayer;

            Hand = currentPlayer.Hand;

            DataContext = CurrentPlayer;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }

        private void Username_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
