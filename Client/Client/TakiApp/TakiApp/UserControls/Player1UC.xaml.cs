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
        public Player1UC()
        {

            InitializeComponent();
        }

        public Player CurrentPlayer { get; set; }
        public CardList Hand { get; set; }
        public Card SelectedCard()
        {
            return (Card)HandView?.SelectedItem;
        }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            BindingContext = CurrentPlayer;

            HandView.ItemsSource = null;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }

        public void SetAsActive()
        {
            MyGrid.BackgroundColor = Color.FromRgba(90, 0, 250, 0);
        }

        public void SetAsNonActive()
        {
            MyGrid.BackgroundColor = Color.Transparent;
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