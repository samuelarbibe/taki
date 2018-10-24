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
        Game _currentGame;
        User _currentUser = MainWindow.CurrentUser;
        Player _currentPlayer;
        Player _table;
        PlayerList _playersList = new PlayerList();
        private int _playersCount;
        private int _currentPlayerIndex;

        public GamePage(Game game)
        {
            InitializeComponent();
            _currentGame = game;

            reorderPlayerList();

            DataContext = _playersList;

            uc1 = new Player1UC(_currentPlayer);

            //BoxOne.Text = _currentPlayer.Username;

            switch (_currentGame.Players.Count)
            {
                case 3:
                    uc3 = new Player3UC();
                    uctable = new TableUC();
                    break;
                //case 4:
                //    BoxTwo.Text = _playersList[1].Username;
                //    BoxThree.Text = _playersList[2].Username;
                //    BoxFive.Text = _playersList[3].Username;
                //    break;
                //case 5:
                //    BoxTwo.Text = _playersList[1].Username;
                //    BoxThree.Text = _playersList[2].Username;
                //    BoxFour.Text = _playersList[3].Username;
                //    BoxFive.Text = _playersList[4].Username;
                //    break;
            }

        }


        // this function re-arranges the player in a particular way, to make sure that:
        // - list[First] is the current player
        // - all the players after him are arranged in order
        // - list[Last] is the table
        public void reorderPlayerList()
        {
            _playersCount = _currentGame.Players.Count;

            _currentPlayerIndex = _currentGame.Players.FindIndex(p => p.UserId == _currentUser.Id);

            for (int i = 0; i < _playersCount - 1; i++)
            {
                if (_currentPlayerIndex >= _playersCount - 1)
                {
                    _playersList.Add(_currentGame.Players[_currentPlayerIndex % (_playersCount - 1)]);
                }
                else
                {
                    _playersList.Add(_currentGame.Players[_currentPlayerIndex]);
                }

                _currentPlayerIndex++;
            }

            _playersList.Add(_currentGame.Players.Find(q => q.Username == "table"));

            _currentPlayer = _playersList[0];

            _table = _playersList[_playersCount - 1];
        }
    }
}
