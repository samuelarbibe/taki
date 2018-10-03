using Form.TakiService;
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

namespace Form
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        Game g = null;
        int playerCount;
        int counter = 0;
        public LoadingPage(int playerCount)
        {

            this.playerCount = playerCount;

            InitializeComponent();

            g = MainWindow.Service.StartGame(MainWindow.CurrentUser, playerCount);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += findGame;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            void findGame(object sender, EventArgs e)
            {

                if (g == null )
                {
                    if (counter < 10)
                    {
                        g = MainWindow.Service.StartGame(MainWindow.CurrentUser, playerCount);
                        counter++;
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                        MainWindow.BigFrame.Navigate(new MainMenu(true));
                    }
                }
                else if (g != null)
                {
                    dispatcherTimer.Stop();
                    MainWindow.BigFrame.Navigate(new GamePage(g));
                }
            }

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
