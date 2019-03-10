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
    public partial class Player1Uc : ContentView
    {
        public Player1Uc()
        {
            InitializeComponent();

            //add a row for all cards
            MyGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }

        public Player CurrentPlayer { get; set; }
        public CardList Hand { get; set; }
        public List<ImageButton> ImageButtonList { get; set; }
        public Card SelectedCard { get; set; }

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

                temp.Clicked += new EventHandler(ImageButtonClicked);

                temp.Source = ImageSource.FromResource(EmbeddedSourcesConverter.Convert(Hand[i].Image));
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

        private void ImageButtonClicked(object sender, EventArgs e)
        {
            ImageButton temp = sender as ImageButton;
            SelectedCard = Hand[ImageButtonList.IndexOf(temp)]; // Card list and Button list are at matching indexes
        }
    }
}