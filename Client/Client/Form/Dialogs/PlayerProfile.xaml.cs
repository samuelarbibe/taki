using Form.TakiService;
using System.Windows;

namespace Form.Dialogs
{
    /// <summary>
    /// Inter_action logic for ExitDialog.xaml
    /// </summary>
    public partial class PlayerProfile : Window
    {
        public PlayerProfile(User user)
        {
            InitializeComponent();

            this.DataContext = user;

            if (MainWindow.Service.AreFriends(user.Id, MainWindow.CurrentUser.Id))
            {
                AddFriend.Visibility = Visibility.Collapsed;
                AreFriends.Text = "Your Are Already Friends With Him";
            }
        }

        private void QuitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void RequestButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
