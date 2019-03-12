using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        ServiceClient _service;
        UserList _friends;

        public FriendsPage()
        {
            InitializeComponent();
            _service = new ServiceClient();

            _service.GetAllUserFriendsCompleted += service_RequestCompleted;

            _service.GetAllUserFriendsAsync(MainMenu.CurrentUser.Id);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void service_RequestCompleted(object sender, GetAllUserFriendsCompletedEventArgs e)
        {
            _friends = new UserList();

            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = null;

                if (e.Error != null)
                {
                    msg = e.Error.Message;
                }
                else if (e.Result == null) { msg = "Wrong Username or Password";}
                else if (e.Cancelled) { msg = "Didn't Work!"; }
                else
                {
                    _friends = e.Result as UserList;
                    FriendsListView.ItemsSource = _friends;
                }
                Console.WriteLine(msg);
            });
        }

        private void RemoveFriendButton_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            User u2 = b.BindingContext as User;

            _service.RemoveFriendAsync(MainMenu.CurrentUser, u2);

            FriendsListView.ItemsSource = null;

            _friends.Remove(_friends.FirstOrDefault(u => u.Id == u2.Id));

            FriendsListView.ItemsSource = _friends;
        }
    }
}