using System.Windows;
using System.Windows.Controls;

namespace CharuEval
{
    public partial class CloseButtonControl : UserControl
    {
        public CloseButtonControl()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}

