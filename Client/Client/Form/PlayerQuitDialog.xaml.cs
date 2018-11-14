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

namespace Form
{
    /// <summary>
    /// Interaction logic for ExitDialog.xaml
    /// </summary>
    public partial class PlayerQuitDialog : Window
    {
        public PlayerQuitDialog()
        {
            InitializeComponent();
        }

        public PlayerQuitDialog(string Username)
        {
            InitializeComponent();
            ResponseTextBox.Text = "Player " + Username + " has Quitted.";
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
