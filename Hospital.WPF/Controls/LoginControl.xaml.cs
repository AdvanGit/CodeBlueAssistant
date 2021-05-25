using Hospital.ViewModel;
using Hospital.ViewModel.Notificator;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using Hospital.WPF.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Controls
{
    public partial class LoginControl : UserControl, INavigatorItem
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private LoginViewModel _vm = new LoginViewModel();
        public string Label => "Авторизация";
        public Type Type => typeof(LoginControl);

        public LoginControl()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        public Command Init => new Command(async obj => 
        {
            if (long.TryParse(obj.ToString(), out long phone))
            {
                if (await _vm.GetUser(phone))
                {
                    Main.MenuNavigator.Bodies.Clear();
                    Main.MenuNavigator.Bodies.Add(new Views.Registrator());
                    Main.MenuNavigator.Bodies.Add(new Views.Schedule());
                    Main.CurrentPage = Main.MenuNavigator.Bodies[0];
                }
            }
            else NotificationManager.AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(3), "Номер введен неверно", true));
        }, obj => (obj != null &&  obj.ToString().Length > 5 ));
    }
}
