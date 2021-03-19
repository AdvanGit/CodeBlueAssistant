using System;
using System.Windows;

namespace Hospital.WPF.Controls
{
    public partial class ConfirmDialog : Window
    {
        public ConfirmDialog(Action<object> action, string message)
        {
            Message = message;
            _action = action;
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            ShowDialog();
        }

        private Action<object> _action;

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ConfirmDialog), new PropertyMetadata(""));

        private void Button_ClickOk(object sender, RoutedEventArgs e)
        {
            _action.Invoke(_action);
            Close();
        }
        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
