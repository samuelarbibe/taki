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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Game currentGame;

        public GamePage(Game game)
        {
            InitializeComponent();
            currentGame = game;
            this.DataContext = currentGame;
            FirstPlayer.Text = currentGame.Players[0].Username;
            SecondPlayer.Text = currentGame.Players[1].Username;
        }
    }
}
