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
        User currentUser = MainWindow.CurrentUser;
        Player currentPlayer;
        Player table;
        PlayerList playersList = new PlayerList();

        public GamePage(Game game)
        {
            InitializeComponent();
            currentGame = game;
            this.DataContext = currentGame;

            playersList.Add(currentGame.Players.Find(p => p.Id == currentUser.Id)); //  add the current player as the first player in the local player list

            playersList.AddRange(currentGame.Players.FindAll(p => p.Id != currentUser.Id && p.Username != "table"));

            currentPlayer = playersList[0];

            table = currentGame.Players.Find(p => p.Username == "table");

            BoxFive.Text = "Table";

            BoxOne.Text = currentPlayer.Username;

            switch (currentGame.Players.Count)
            {
                case 3:
                    BoxThree.Text = playersList[1].Username;
                    break;
                case 4:
                    BoxTwo.Text = playersList[1].Username;
                    BoxThree.Text = playersList[2].Username;
                    break;
                case 5:
                    BoxTwo.Text = playersList[1].Username;
                    BoxThree.Text = playersList[2].Username;
                    BoxFour.Text = playersList[3].Username;
                    break;
            }
        }
    }
}
