using TakiApp.TakiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Form.Dialogs;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TakiApp.Utilities;

namespace TakiApp.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfo : ContentView
    {
        ServiceClient _service;
        private Player p;

        public UserInfo()
        {
            InitializeComponent();
        }

        public void SetPlayer(Player p1)
        {
            p = p1;
            this.BindingContext = p1;
        }

        private void Image_Clicked(object sender, EventArgs e)
        {
            _service = new ServiceClient();
            _service.AreFriendsCompleted += Serv_CheckCompleted;

            _service.AreFriendsAsync(p.UserId, MainMenu.CurrentUser.Id);
        }

        private void Serv_CheckCompleted(object sender, AreFriendsCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = null;

                if (e.Error != null)
                {
                    msg = e.Error.Message;
                    Console.WriteLine(msg);
                }
                else if (e.Result == null) { msg = "Wrong Username or Password"; }
                else if (e.Cancelled) { msg = "Didn't Work!"; Console.WriteLine(msg); }
                else
                {
                    Prompt(true);
                }
                Console.WriteLine(msg);
            });
        }

        private async void Prompt(bool isFriend)
        {
            var answer = await DisplayAlert("Quit", "Are you sure you want to quit? all progress will be lost", "Yes", "No");

            if (answer)
            {
                PlayerQuit();
                await Navigation.PushModalAsync(new MainMenu());
            }
        }
    }
}