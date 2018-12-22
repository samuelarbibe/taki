using System.Windows;

namespace Form.Dialogs
{
    /// <summary>
    /// Inter_action logic for ForceQuit.xaml
    /// </summary>
    public partial class ForceQuit : Window
    {
        public ForceQuit()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
