using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Form.TakiService;

namespace Form
{
    /// <summary>
    /// Inter_action logic for AdminUserPage.xaml
    /// </summary>
    public partial class AdminUserPage : Page
    {
        User tempUser = new User();
        UserList dataList;

        public AdminUserPage()
        {
            InitializeComponent();
            this.DataContext = tempUser;
            Style = (Style)FindResource(typeof(Page));
        }

        private void show_btn_Click(object sender, RoutedEventArgs e)
        {
            tempUser = null;
            dataList = MainWindow.Service.GetAllUsers();

            if (dataList.Count == 0)
            {
                State.Text = "Error Retrieving Data";
                State.Foreground = Brushes.Red;
                State.FontSize = 9;
            }
            else
            {
                State.Text = "Success";
                State.Foreground = Brushes.Gray;
                State.FontSize = 11;
                DataGrid.ItemsSource = dataList;
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DataGrid.SelectedItem == null) return;

                //change submit/update button
                SubmitBtn.Visibility = Visibility.Hidden;
                UpdateBtn.Visibility = Visibility.Visible;

                tempUser = DataGrid.SelectedItem as User;
                this.DataContext = tempUser;

                GameList dataList = MainWindow.Service.GetAllUserGames(tempUser.Id);

                if (dataList == null)
                {
                    State.Text = "Error Retrieving Lesson Data";
                    State.Foreground = Brushes.Red;
                    State.FontSize = 9;
                }
                else if (dataList.Count == 0)
                {
                    State.Text = "No Game History";
                    State.Foreground = Brushes.Red;
                    State.FontSize = 9;
                    GameGrid.Visibility = Visibility.Hidden;
                    GameGridSize.Height = new GridLength(0);
                    GameGridBottomMargin.Height = new GridLength(0);
                }
                else
                {
                    GameGrid.Visibility = Visibility.Visible;
                    GameGridSize.Height = new GridLength(120);
                    GameGridBottomMargin.Height = new GridLength(0);
                    State.Text = "Success";
                    State.Foreground = Brushes.Gray;
                    State.FontSize = 11;
                    GameGrid.ItemsSource = dataList;
                }

            DeleteBtn.Visibility = Visibility.Visible;
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                tempUser = DataGrid.SelectedItem as User;

                int x = MainWindow.Service.DeleteUser(tempUser);

                if (x > 0)//ok
                {
                    dataList.Remove(tempUser);
                    DataGrid.ItemsSource = null;
                    DataGrid.ItemsSource = dataList;
                    tempUser = null;
                    State.Text = "Deleted";
                    State.Foreground = Brushes.Red;
                    State.FontSize = 11;
                }
                else
                {
                    State.Text = "Error Deleting";
                    State.Foreground = Brushes.Red;
                    State.FontSize = 9;
                }
            }
            else {
                State.Text = "please select an item to delete";
                tempUser = null;
            }
        }


        private void InsertSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Service.Register(tempUser.FirstName, tempUser.LastName, tempUser.Username, tempUser.Password);
            int x = MainWindow.Service.SaveChanges();

            if (x > 0)//ok
            {
                if (dataList == null)
                {
                    dataList = new UserList();
                }
                dataList.Add(tempUser);
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = dataList;
                tempUser = null;
                State.Text = "Inserted";
                State.Foreground = Brushes.Red;
                State.FontSize = 11;
            }
            else
            {
                State.Text = "Error Inserting";
                State.Foreground = Brushes.Red;
                State.FontSize = 9;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

            int x = MainWindow.Service.UpdateUser(tempUser);

            if (x > 0)//ok
            {
                if (dataList == null)
                {
                    dataList = new UserList();
                }
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = dataList;
                tempUser = null;
                State.Text = "Updated";
                State.Foreground = Brushes.Red;
                State.FontSize = 11;
            }
            else
            {
                State.Text = "Error Updating";
                State.Foreground = Brushes.Red;
                State.FontSize = 9;
            }
        }

        private void CreateBtnStudent_Click(object sender, RoutedEventArgs e)
        {
            tempUser = new User();
            this.DataContext = tempUser;
            SubmitBtn.Visibility = Visibility.Visible;
            UpdateBtn.Visibility = Visibility.Hidden;
            Form.Visibility = Visibility.Visible;
            FormGridSize.Height = new GridLength(100);
            GameGridBottomMargin.Height = new GridLength(0);
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                BackButton.Content = "cannot go back...";
            }
        }
    }
}
