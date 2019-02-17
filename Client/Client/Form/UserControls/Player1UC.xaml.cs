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
        public Player1UC() {

            InitializeComponent();
        }

        public Player CurrentPlayer { get; set; }

        public CardList Hand { get; set; }

        public Card SelectedCard()
        {
            return (Card) HandView?.SelectedItems[0];
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
            BackgroundActive.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(90, 0, 250, 0));
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
