﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TakiApp.TakiService;
using TakiApp.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TakiApp.UserControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TableUc : ContentView
	{
        public event EventHandler TakeCardFromDeckButtonClicked;
        public event EventHandler PassCardToStackButtonClicked;
        private readonly SourceConverter _converter = new SourceConverter();
        private static readonly RNGCryptoServiceProvider RngCsp = new RNGCryptoServiceProvider();

        public Player CurrentPlayer { get; set; }

        public CardList Deck { get; set; }

        public CardList Stack { get; set; }

        public void UpdateUi(Player p)
        {
            CurrentPlayer = p;

            BindingContext = Deck.LastOrDefault();

            PassCardButton.Source = null;
            PassCardButton.Source = _converter.Convert(Deck.LastOrDefault().Image, null, null, null).ToString();
        }


        public TableUc()
        {
            InitializeComponent();
        }


        public Card GetCardFromStack()
        {
            Card temp = Deck[RollDice((byte)(Deck.Count - 2))];
            //Deck.Remove(temp);

            return temp;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;

            Deck = CurrentPlayer.Hand; //set the deck as the table's hand

            BindingContext = Deck.LastOrDefault(); // set the last card of the player's hand as the displayed card

        }

        public static byte RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                RngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }


        public void DeckButton_OnClick(object sender, EventArgs e)
        {
            //Null check makes sure the main page is attached to the event
            TakeCardFromDeckButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public void CanTakeCardFromDeck()
        {
            DeckButton.IsEnabled = true;
            PassCardButton.IsEnabled = true;
        }

        public void CannotTakeCardFromDeck()
        {
            DeckButton.IsEnabled = false;
            PassCardButton.IsEnabled = false;
        }


        private void PassCardButton_Click(object sender, EventArgs e)
        {
            PassCardToStackButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}