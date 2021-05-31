using Hospital.ViewModel.Notificator;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Controls
{
    public partial class NotificationArea : UserControl
    {
        public NotificationArea()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotificationManager.Cancel();
        }
    }
}
