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
        }

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
