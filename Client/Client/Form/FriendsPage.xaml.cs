﻿using Form.TakiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes;

namespace Form
{
    /// <summary>
    /// Interaction logic for FriendsPage.xaml
    /// </summary>
    public partial class FriendsPage : Page
    {
        private UserList _ul;

        public FriendsPage()
        {
            InitializeComponent();

            _ul = MainWindow.Service.GetAllUserFriends(MainWindow.CurrentUser.Id);

            View.ItemsSource = _ul;
        }

        protected void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                BackButton.Content = "cannot go back...";
            }
        }

        private void RemoveFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            User u2 = b.DataContext as User;

            MainWindow.Service.RemoveFriend(MainWindow.CurrentUser, u2);

            View.ItemsSource = null;

            _ul.Remove(_ul.Find(u => u.Id == u2.Id));

            View.ItemsSource = _ul;
        }

        private void MutualGamesButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            User u2 = b.DataContext as User;

            NavigationService.Navigate(new GameHistoryUserPage(MainWindow.CurrentUser.Id, u2.Id));
        }
    }
}
