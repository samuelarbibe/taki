using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Mobile
{
    public partial class MainMenu : ContentPage
    {
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
            throw new NotImplementedException();
        }

        void MultiplayerButton2_Click(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void MultiplayerButton3_Click(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
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
