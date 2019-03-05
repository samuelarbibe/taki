using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Form;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
        private User _cu = MainMenu.CurrentUser;
        private ServiceClient _service;

        public UserPage()
        {
            InitializeComponent();

            BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);

            _service = new ServiceClient();

            BindingContext = _cu;

            int active = _cu.Score % 1000;
            int full = 1000;

            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(active, GridUnitType.Star) });
            GridProgressBar.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(full - active, GridUnitType.Star) });

            GridProgressBar.BackgroundColor = Xamarin.Forms.Color.Silver;

            GridProgressBar.Padding = 1;

            Label progress = new Label();
            progress.Text = active.ToString() + "/" + full.ToString(); ;
            progress.BackgroundColor = Xamarin.Forms.Color.FromRgb(277,277,277);

            Label empty = new Label();
            empty.BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);
            

            GridProgressBar.Children.Add(progress, 0, 0);
            GridProgressBar.Children.Add(empty, 1, 0);


            //if (_cu.Admin)
            //{
            //    AdminButton.Visibility = Visibility;
            //}
            //else
            //{
            //    AdminButton.Visibility = Visibility.Hidden;
            //}
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Logout_Button_Click(object sender, EventArgs e)
        {
            MainMenu.CurrentUser = null;
            _service.LogoutAsync(_cu.Id);

            for (var counter = 1; counter < Navigation.NavigationStack.Count - 2; counter++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
            await Navigation.PopModalAsync();
        }

        private void Admin_Button_Click(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new AdminUserPage());
        }

        private void Game_Button_Click(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new GameHistoryPage());
        }
    }
}