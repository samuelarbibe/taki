using System;
using System.Collections.Generic;
using TakiApp;
using TakiApp.TakiService;
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

            BindingContext = CurrentUser;

            BackgroundImage = "wallpaper.jpg";

            int active = CurrentUser.Score % 1000;
            int full = 1000;

            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(active, GridUnitType.Star) });
            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(full - active, GridUnitType.Star) });

            GridProgressBar.BackgroundColor = Xamarin.Forms.Color.FromRgb(227, 227, 227);

            GridProgressBar.Padding = 1;

            BoxView Progress = new BoxView();
            Progress.BackgroundColor = Xamarin.Forms.Color.FromRgb(227,227,227);

            BoxView Empty = new BoxView();
            Empty.BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);

            GridProgressBar.Children.Add(Progress, 0, 0);          
            GridProgressBar.Children.Add(Empty, 1, 0);
        }

        public static GamePage CurrentGamePage { get; set; }

        void PlayButton_Click(object sender, System.EventArgs e)
        {
            PlayButton.IsVisible = false;
            MultiplayerButton_1.IsVisible = true;
            MultiplayerButton_2.IsVisible = true;
            MultiplayerButton_3.IsVisible = true;
            CancelButton.IsVisible = true;
        }

        void MultiplayerButton1_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new LoadingPage(2));
        }

        void MultiplayerButton2_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new LoadingPage(3));
        }

        void MultiplayerButton3_Click(object sender, System.EventArgs e)
        {
            this.Navigation.PushModalAsync(new LoadingPage(4));
        }

        void CancelButton_Click(object sender, System.EventArgs e)
        {
            PlayButton.IsVisible = true;
            MultiplayerButton_1.IsVisible = false;
            MultiplayerButton_2.IsVisible = false;
            MultiplayerButton_3.IsVisible = false;
            CancelButton.IsVisible = false;
        }
    }
}
