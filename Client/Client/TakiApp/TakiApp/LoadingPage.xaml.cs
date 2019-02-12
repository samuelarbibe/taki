using Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TakiApp.TakiService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : ContentPage
	{
        private ServiceClient service;
        private Game _game;
        private int _playerCount;
        private Player _p;
        private User _cu;
        private int _counter;
        private bool _gameNotFound;

        public LoadingPage (int playerCount)
		{

            service = new ServiceClient();
            service.StartGameCompleted += Serv_RequestCompleted;

            _counter = 0;
            _playerCount = playerCount;
            _cu = MainMenu.CurrentUser;
            _gameNotFound = false;

            InitializeComponent();

            SearchGame();
        }

        private void SearchGame()
        {
            _counter = 0;
            _p = new Player()
            {
                UserId = _cu.Id,
                FirstName = _cu.FirstName,
                LastName = _cu.LastName,
                Username = _cu.Username,
                Password = _cu.Password,
                Level = _cu.Level,
                Score = _cu.Score,
                Admin = _cu.Admin,
                Wins = _cu.Wins,
                Losses = _cu.Losses,
                TempScore = 0
            };

            service.StartGameAsync(_p, _playerCount);
        }

        private void Serv_RequestCompleted(object sender, StartGameCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                string msg = null;

                if (e.Result != null)
                {
                    _game = e.Result as Game;
                    msg = "Found";
                    //await this.Navigation.PushModalAsync(new GamePage(_game));
                }

                else if (_counter < 20)
                {
                    if (e.Error != null) { msg = e.Error.Message; }
                    else if (e.Result == null)
                    {
                        msg = "player is in queue...";
                        service.StartGameAsync(_p, _playerCount);
                        _counter++;
                    }
                    else if (e.Cancelled)
                    {
                        msg = "canceled";
                        _counter = 20;
                    }
                }
                else
                {
                    msg = "No Games Found!";
                }
                this.status.Text = msg;
            });
        }



    }
}