using System.Windows.Controls;
using System.Windows.Media;
using Form.TakiService;

namespace Form.UserControls
{
    /// <summary>
    /// Inter_action logic for Player4UC.xaml
    /// </summary>
    public partial class Player4UC : UserControl
    {
        private Player _currentPlayer;
        private CardList _hand;

        public Player4UC()
        {
            InitializeComponent();
        }

        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;
            DataContext = CurrentPlayer;
            CardListItemControl.ItemsSource = null;

            CardListItemControl.ItemsSource = CurrentPlayer.Hand;

        }

        public void SetAsActive()
        {
            BackgroundActive.Fill = new SolidColorBrush(Color.FromArgb(60, 0, 250, 0));
        }

        public void SetAsNonActive()
        {
            BackgroundActive.Fill = null;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            _hand = currentPlayer.Hand;

            DataContext = CurrentPlayer;
            CardListItemControl.ItemsSource = CurrentPlayer.Hand;

        }
    }
}
