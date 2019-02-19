using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace TakiApp.UserControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Player3UC : ContentView
	{
        private ServiceClient Service;

        private CardList _hand;

        public Player3UC()
        {
            InitializeComponent();

            Service = new ServiceClient();
        }


        public Player CurrentPlayer { get; set; }

        public void UpdateUI(Player p)
        {
            CurrentPlayer = p;

            BindingContext = CurrentPlayer;

            HandView.ItemsSource = null;

            HandView.ItemsSource = CurrentPlayer.Hand;

        }

        public void SetAsActive()
        {
            MyGrid.BackgroundColor = Color.FromRgba(60, 0, 250, 0);
        }

        public void SetAsNonActive()
        {
            MyGrid.BackgroundColor = Color.Transparent;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            _hand = currentPlayer.Hand;

            BindingContext = CurrentPlayer;

            HandView.ItemsSource = CurrentPlayer.Hand;
        }

        //private void Username_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    User temp = Service.GetUserById(CurrentPlayer.UserId);

        //    PlayerProfile dialog = new PlayerProfile(temp);

        //    if (dialog.ShowDialog() == true)
        //    {
        //        Service.MakeFriendsAsync(MainMenu.CurrentUser, temp);
        //    }
        //} 
    }
}