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
    /// Interaction logic for Player1UC.xaml
    /// </summary>
    public partial class Player1UC : UserControl
    {
        private CardList _hand;
        private Player _currentPlayer;
        private bool _active;

        public Player1UC() {

            InitializeComponent();
        }

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

    }
}
