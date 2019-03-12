using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakiApp.TakiService;
using TakiApp.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace TakiApp.UserControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Player3Uc : ContentView
	{
        private ServiceClient _service;

        public Player3Uc()
        {
            InitializeComponent();

            //add a row for all cards
            MyGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }

        public Player CurrentPlayer { get; set; }
        public CardList Hand { get; set; }
        public List<ImageButton> ImageButtonList { get; set; }

        public void UpdateUi(Player p)
        {
            CurrentPlayer = p;

            BindingContext = CurrentPlayer;

            Hand = null;

            Hand = CurrentPlayer.Hand;

            SetHandView();
        }

        private void SetHandView()
        {
            SourceConverter converter = new SourceConverter();
            ImageButtonList = new List<ImageButton>();

            MyGrid.Children.Clear();
            MyGrid.ColumnDefinitions.Clear();


            for (int i = 0; i < Hand.Count; i++)
            {
                ImageButton temp = new ImageButton();

                MyGrid.ColumnSpacing = -30;

                temp.Source = temp.Source = "card0068.png";
                temp.BackgroundColor = Color.Transparent;

                ImageButtonList.Add(temp);

                MyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                MyGrid.Children.Add(temp, i, 0);

                MyGrid.Children[i].HorizontalOptions = LayoutOptions.FillAndExpand;

                MyGrid.Children[i].HeightRequest = MyGrid.Height;

                MyGrid.Children[i].WidthRequest = MyGrid.Children[i].Height / 1.9;
            }
        }

        public void SetAsActive()
        {
            MyGrid.BackgroundColor = Color.FromRgba(0, 250, 0, 0.3);
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