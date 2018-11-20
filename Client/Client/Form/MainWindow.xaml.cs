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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ServiceClient Service;
        public static Frame BigFrame;
        public static GamePage CurrentGamePage;

        private static User currentUser; 

        public static User CurrentUser {
            get => currentUser;
            set => currentUser = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            Service = new ServiceClient();
            BigFrame = MainFrame;
            BigFrame.Navigate(new LoginMenu());
            this.DataContext = currentUser;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (CurrentGamePage != null) CurrentGamePage.PlayerQuit();
        }
    }
}
