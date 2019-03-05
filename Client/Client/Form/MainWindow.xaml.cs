using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.ServiceModel;
using Form.TakiService;


namespace Form
{
    /// <summary>
    /// Inter_action logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ServiceClient Service;
        public static Frame BigFrame;
        public static GamePage CurrentGamePage;

        public static User CurrentUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Service = new ServiceClient();
            BigFrame = MainFrame;
            BigFrame.Navigate(new LoginMenu());
            DataContext = CurrentUser;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            CurrentGamePage?.PlayerQuit();

            if(CurrentUser != null) Service.Logout(CurrentUser.Id);
        }
    }
}
