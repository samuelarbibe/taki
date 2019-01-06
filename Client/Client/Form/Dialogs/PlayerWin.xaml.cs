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

namespace Form.Dialogs
{
    /// <summary>
    /// Interaction logic for PlayerWin.xaml
    /// </summary>
    public partial class PlayerWin : Window
    {
        public PlayerWin(string winnerName)
        {
            InitializeComponent();
            ResponseTextBox.Text = "Player " + winnerName + " Has Won the Game";
        }


        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
