using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using TakiApp.TakiService;

namespace TakiApp
{
    public partial class MainPage : ContentPage
    {
        ServiceClient serv;
        public MainPage()
        {
            InitializeComponent();
            serv = new ServiceClient();
            serv.LoginCompleted += Serv_LoginCompleted;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            serv.LoginAsync(Uname.Text, pass.Text);
        }

        private void Serv_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(async()=>
            {
                string msg = null;
                User usr = null;

                if (e.Error != null) { msg = e.Error.Message; }
                else if (e.Cancelled) { msg = "Request wasCancelled"; }
                else { usr = e.Result as User;
                    msg = usr.Username + " Logged in";
                }
                this.msg.Text = msg;
            });
        }
    }
}
