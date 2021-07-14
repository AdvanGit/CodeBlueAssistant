using Hospital.ViewModel;
using Hospital.ViewModel.Notificator;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Login : UserControl, INavigatorItem
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private LoginViewModel _vm = new LoginViewModel();
        public string Label => "Авторизация";
        public Type Type => GetType();

        public Login()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        public Command Init => new Command(async obj =>
        {
            if (long.TryParse(obj.ToString(), out long phone))
            {
                if (await _vm.CheckUser(phone))
                {
                    Main.MenuNavigator.Bodies.Clear();
                    Main.MenuNavigator.Bodies.Add(new Registrator());
                    Main.MenuNavigator.Bodies.Add(new Schedule());
                    Main.CurrentPage = Main.MenuNavigator.Bodies[0];
                }
            }
            else NotificationManager.AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(3), "Номер введен неверно", true));
        }, obj => (obj != null && obj.ToString().Length > 5));
    }
}
