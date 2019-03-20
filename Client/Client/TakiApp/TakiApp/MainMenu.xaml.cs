using System;
using System.Collections.Generic;
using Form;
using TakiApp;
using TakiApp.TakiService;
using TakiApp.UserControls;
using Xamarin.Forms;

namespace TakiApp
{
    public partial class MainMenu : ContentPage
    {
        public static User CurrentUser;
        //public static GamePage CurrentGamePage;

        public MainMenu()
        {
            InitializeComponent();

            //InfoUC = new UserInfo((Player)CurrentUser);

            BindingContext = CurrentUser;

            BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);

            int active = CurrentUser.Score % 1000;
            int full = 1000;

            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(active, GridUnitType.Star) });
            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(full - active, GridUnitType.Star) });

            GridProgressBar.BackgroundColor = Xamarin.Forms.Color.FromRgb(227, 227, 227);

            GridProgressBar.Padding = 1;

            BoxView progress = new BoxView();
            progress.BackgroundColor = Xamarin.Forms.Color.FromRgb(227,227,227);

            BoxView empty = new BoxView();
            empty.BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);

            GridProgressBar.Children.Add(progress, 0, 0);          
            GridProgressBar.Children.Add(empty, 1, 0);
        }

        public static GamePage CurrentGamePage { get; set; }

        void PlayButton_Click(object sender, EventArgs e)
        {
            PlayButton.IsVisible = false;
            MultiplayerButton_1.IsVisible = true;
            MultiplayerButton_2.IsVisible = true;
            MultiplayerButton_3.IsVisible = true;
            CancelButton.IsVisible = true;
        }

        void MultiplayerButton1_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoadingPage(2));
        }

        void MultiplayerButton2_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoadingPage(3));
        }

        void MultiplayerButton3_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoadingPage(4));
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            PlayButton.IsVisible = true;
            MultiplayerButton_1.IsVisible = false;
            MultiplayerButton_2.IsVisible = false;
            MultiplayerButton_3.IsVisible = false;
            CancelButton.IsVisible = false;
        }

        async void UserPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UserPage());
        }

        private void FriendsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FriendsPage());
        }
    }
}
