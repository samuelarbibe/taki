using System.Windows.Controls;
using System.Windows.Media;
using Form.Dialogs;
using Form.TakiService;
using Color = System.Windows.Media.Color;

namespace Form.UserControls
{
    /// <summary>
    /// Inter_action logic for Player2UC.xaml
    /// </summary>
    public partial class Player2UC : UserControl
    {
        private CardList _hand;

        public Player2UC()
        {
            InitializeComponent();
        }

        public Player CurrentPlayer { get; set; }

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

        private void Username_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            User temp = MainWindow.Service.GetUserById(CurrentPlayer.UserId);

            PlayerProfile dialog = new PlayerProfile(temp);

            if (dialog.ShowDialog() == true)
            {
                MainWindow.Service.MakeFriends(MainWindow.CurrentUser, temp);
            }
        }

    }
}
