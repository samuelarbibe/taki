using System.Windows;

namespace Form.Dialogs
{
    /// <summary>
    /// Inter_action logic for ExitDialog.xaml
    /// </summary>
    public partial class ExitDialog : Window
    {
        public ExitDialog()
        {
            InitializeComponent();
        }

        private void QuitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
