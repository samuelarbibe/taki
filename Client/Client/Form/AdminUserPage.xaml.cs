using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Form.TakiService;
using MaterialDesignThemes.Wpf;

namespace Form
{
    /// <summary>
    /// Inter_action logic for AdminUserPage.xaml
    /// </summary>
    public partial class AdminUserPage : Page
    {
        User tempUser = new User();
        UserList dataList;
        UserList ChangedDataList;

        public AdminUserPage()
        {
            InitializeComponent();
            this.DataContext = tempUser;
            Style = (Style)FindResource(typeof(Page));
            show_btn_Click(null, null);
            ChangedDataList = new UserList();
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
            if (DataGrid.SelectedItem == null) {
                SubmitBtn.Visibility = Visibility.Collapsed;
                return;
            }

            //change submit/update button
            UpdateBtn.Visibility = Visibility.Visible;

            tempUser = DataGrid.SelectedItem as User;
            this.DataContext = tempUser;

            if (tempUser.Id != 999)
            {
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
                    DataGrid.Height = 370;
                    GameGrid.Height = 0;
                }
                else
                {
                    GameGrid.Visibility = Visibility.Visible;
                    DataGrid.Height = 170;
                    GameGrid.Height = 170;
                    State.Text = "Success";
                    State.Foreground = Brushes.Gray;
                    State.FontSize = 11;
                    GameGrid.ItemsSource = dataList;
                }
            }
            DeleteBtn.Visibility = Visibility.Visible;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ChangedDataList.Add((User)DataGrid.SelectedItem);//add the edited row to rows to be updated

                // rowIndex has the row index
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems != null)
            {
                int x = 0;
                foreach (var u in DataGrid.SelectedItems)
                {
                    tempUser = u as User;
                    x += MainWindow.Service.DeleteUser(tempUser);
                }
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


        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            dataList.Add(new User() {
                FirstName = "*",
                LastName = "*",
                Username = "*",
                Password = "*",
                Level = 0,
                Score = 0,
                Id = 999,
                Losses = 0,
                Wins = 0,
                Admin = false
            });

            DataGrid.ItemsSource = dataList;

            int index = DataGrid.Items.Count - 1;
            object item = DataGrid.Items[index];
            DataGrid.SelectedItem = item;
            DataGrid.ScrollIntoView(item);

            SubmitBtn.Visibility = Visibility.Visible;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            User u = DataGrid.SelectedItem as User;
            if (u.Username != null && u.Password != null && u.FirstName != null && u.LastName != null)
            {

                bool ok = MainWindow.Service.Register(u.FirstName, u.LastName, u.Username, u.Password);

                if (ok)//ok
                {
                    show_btn_Click(null, null);
                }
                else
                {
                    State.Text = "Error Inserting";
                    State.Foreground = Brushes.Red;
                    State.FontSize = 9;
                }
            }
            else
            {
                State.Text = "Fill All!";
                State.Foreground = Brushes.Red;
                State.FontSize = 9;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChangedDataList.Count > 0)
            {
                int x = 0;

                foreach(User u in ChangedDataList)
                {
                    x += MainWindow.Service.UpdateUser(u);
                }
                
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
                ChangedDataList = new UserList();
            }
            else
            {
                State.Text = "Nothing to Update!";
                State.Foreground = Brushes.Red;
                State.FontSize = 9;
            }

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
