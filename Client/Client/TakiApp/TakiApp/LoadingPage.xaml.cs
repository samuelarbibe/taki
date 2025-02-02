﻿using System;
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
        private ServiceClient _service;
        private Game _game;
        private int _playerCount;
        private Player _p;
        private User _cu;
        private int _counter;
        private bool _gameNotFound;
        private bool _busy;

        public bool Busy { get => _busy; set => _busy = value; }

        public LoadingPage()
        {
            BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);
        }

        public LoadingPage(int playerCount)
		{
            InitializeComponent();
            BackgroundColor = Xamarin.Forms.Color.FromRgb(80, 155, 208);

            _service = new ServiceClient();
            _service.StartGameCompleted += Serv_RequestCompleted;

            _counter = 0;
            _playerCount = playerCount;
            _cu = MainMenu.CurrentUser;
            _gameNotFound = false;

            BindingContext = this;

            Busy = true;

            

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
                ProfileImage = _cu.ProfileImage,
                Score = _cu.Score,
                Admin = _cu.Admin,
                Wins = _cu.Wins,
                Losses = _cu.Losses,
                TempScore = 0
            };

            _service.StartGameAsync(_p, _playerCount);
        }

        private void Serv_RequestCompleted(object sender, StartGameCompletedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                string msg = null;

                if(e.Error != null) { Console.WriteLine(e.Error); }
                else if (e.Cancelled) { Console.WriteLine( " Canceled:"+e.Cancelled.ToString()); }
                else if (e.Result != null)
                {
                    _game = e.Result as Game;
                    msg = "Connected";
                    Busy = false;

                    Navigation.PushModalAsync(new GamePage(_game));

                }

                else if (_counter < 20)
                {
                    if (e.Error != null) { msg = e.Error.Message; }
                    else if (e.Result == null)
                    {
                        Console.WriteLine(_counter);
                        Thread.Sleep(500);
                        msg = "player is in queue...";
                        _counter++;
                        _service.StartGameAsync(_p, _playerCount);
                    }
                }
                else
                {
                    msg = "No Games Found!";
                    Busy = false;
                }
                status.Text = msg;
            });
        }


        private async void CancelButton_Click(object sender, EventArgs e)
        {
            _counter = 20;
            Busy = false;
            await Navigation.PopModalAsync(true);
        }

    }
}