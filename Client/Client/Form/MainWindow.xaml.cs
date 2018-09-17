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
using System.ServiceModel;
using Form.TakiService;



namespace Form
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServiceClient service;
        public static Frame BigFrame;

        public MainWindow()
        {
            InitializeComponent();
            service = new ServiceClient();
            BigFrame = MainFrame;
            BigFrame.Navigate(new LoginMenu());
        }
    }
}
