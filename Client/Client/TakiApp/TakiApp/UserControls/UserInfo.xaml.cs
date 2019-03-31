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
        public delegate void UserInfoRequestedEventHandler(object sender, EventArgs e, bool isFriend, Player p1, User u2);

        public event UserInfoRequestedEventHandler UserInfoRequested;
        ServiceClient _service;
        Player p;

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
                    UserInfoRequested?.Invoke(sender, EventArgs.Empty, e.Result, p, MainMenu.CurrentUser);
                }
                Console.WriteLine(msg);
            });
        }
    }
}