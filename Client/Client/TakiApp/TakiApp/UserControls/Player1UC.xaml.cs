using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakiApp.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Player1UC : ContentView
    {
        private CardList _hand;
        private Player _currentPlayer;
        private bool _active;

        public Player1UC()
        {

            InitializeComponent();
        }

        public Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        public bool Active { get => _active; set => _active = value; }
        public CardList Hand { get => _hand; set => _hand = value; }

        //public Card SelectedCard()
        //{
        //    try
        //    {
        //        return (Card)HandView.SelectedItems[0];
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            BindingContext = CurrentPlayer;

            HandView.ItemsSource = null;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }

        public void SetAsActive()
        {
            //BackgroundActive.Fill = new SolidColorBrush(Color.FromArgb(90, 0, 250, 0));
        }

        public void SetAsNonActive()
        {
            //BackgroundActive.Fill = null;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            Hand = currentPlayer.Hand;

            BindingContext = CurrentPlayer;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }
    }
}