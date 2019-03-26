using TakiApp.TakiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TakiApp.Utilities;

namespace TakiApp.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfo : ContentView
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        public void SetPlayer(Player p1)
        {
            this.BindingContext = p1;
        }

        private void Image_Clicked(object sender, EventArgs e)
        {

        }
    }
}