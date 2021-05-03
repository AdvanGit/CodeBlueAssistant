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

        public LoginControl()
        {
            InitializeComponent();
        }

        public string Label => "Авторизация";
        public Type Type => typeof(LoginControl);

        public Command Init => new Command(async obj => 
        {
            long phone;
            if (long.TryParse(obj.ToString(), out phone))
            {
                if (await MainViewModel.GetUser(phone))
                {
                    Main.MenuNavigator.Bodies.Clear();
                    Main.MenuNavigator.Bodies.Add(new Views.Registrator());
                    Main.MenuNavigator.Bodies.Add(new Views.Schedule());
                    Main.CurrentPage = Main.MenuNavigator.Bodies[0];
                }
            }
            else NotificationManager.AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(3), "Номер введен неверно", true));
        }, obj => (obj != null &&  obj.ToString().Length == 11 ));


    }
}
