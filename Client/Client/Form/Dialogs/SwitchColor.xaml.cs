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
using System.Windows.Shapes;
using Form.TakiService;

namespace Form.Dialogs
{
    /// <summary>
    /// Inter_action logic for SwitchColor.xaml
    /// </summary>
    public partial class SwitchColor : Window
    {
        private Card _selectedColor;

        public Card SelectedColor
        {
            get => _selectedColor;
            set => _selectedColor = value;
        }

        public SwitchColor()
        {
            InitializeComponent();
        }

        private void RedButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = new Card()
            {
                Id = 30,
                VALUE = Card.Value.SwitchColor,
                COLOR = Card.Color.red,
                Image = "../Resources/Cards/card0030.png",
                Special = true
            };

            DialogResult = true;
        }

        private void YellowButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = new Card()
            {
                Id = 46,
                VALUE = Card.Value.SwitchColor,
                COLOR = Card.Color.yellow,
                Image = "../Resources/Cards/card0046.png",
                Special = true
            };

            DialogResult = true;
        }

        private void GreenButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = new Card()
            {
                Id = 14,
                VALUE = Card.Value.SwitchColor,
                COLOR = Card.Color.green,
                Image = "../Resources/Cards/card0014.png",
                Special = true
            };

            DialogResult = true;
        }

        private void BlueButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = new Card()
            {
                Id = 62,
                VALUE = Card.Value.SwitchColor,
                COLOR = Card.Color.blue,
                Image = "../Resources/Cards/card0062.png",
                Special = true
            };

            DialogResult = true;
        }
    }
}
