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
        private System.Threading.Timer dispatcherTimer;

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

            //_game = MainWindow.Service.StartGame(_p, _playerCount);

            var autoEvent = new AutoResetEvent(false);

            // Create a timer that invokes FindGame after one second, 
            // and every 1/2 second thereafter.
            Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n", DateTime.Now);
            var stateTimer = new Timer(FindGame, autoEvent, 2000, 2000);

            // When autoEvent signals the second time, dispose of the timer.
            autoEvent.WaitOne();
            stateTimer.Dispose();
            Console.WriteLine("\nDestroying timer.");


            void FindGame(Object stateInfo)
            {
                if (_game == null)
                {
                    status.Text = "player is in queue...";

                    if (_counter < 10)
                    {
                        service.StartGameAsync(_p, _playerCount);
                        _counter++;
                    }
                    else
                    {
                        autoEvent.Set();//signal the thread to stop
                        status.Text = "no game could be found... please try again";
                        _gameNotFound = true;
                    }
                }
                else // if game is found
                {
                    this.Navigation.PushModalAsync(new GamePage(_game));

                    autoEvent.Set();//signal the thread to stop
                }
            }
        }

        private void Serv_RequestCompleted(object sender, StartGameCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                string msg = null;

                if (e.Error != null) { msg = e.Error.Message; }
                else if (e.Result == null) { msg = "Searching..."; }
                else if (e.Cancelled) { msg = "No Games Found!"; }
                else
                {
                    _game = e.Result as Game;
                }
                this.status.Text = msg;
            });
        }



    }
}