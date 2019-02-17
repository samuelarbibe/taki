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
        public Card SelectedColor { get; set; }

        public SwitchColor()
        {
            InitializeComponent();
        }

        private void RedButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = new Card()
            {
                Id = 30,
                Value = Value.SwitchColor,
                Color = TakiService.Color.Red,
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
                Value = Value.SwitchColor,
                Color = TakiService.Color.Red,
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
                Value = Value.SwitchColor,
                Color = TakiService.Color.Green,
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
                Value = Value.SwitchColor,
                Color = TakiService.Color.Blue,
                Image = "../Resources/Cards/card0062.png",
                Special = true
            };

            DialogResult = true;
        }
    }
}
