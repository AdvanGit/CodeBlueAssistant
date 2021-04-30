using Hospital.ViewModel;
using Hospital.WPF.Navigators;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Main : MetroWindow
    {
        private static UserControl _currentPage;
        
        private void GetControls(UserAccess userAccess)
        {
            switch (userAccess)
            {
                case UserAccess.admin:
                    MenuNavigator.Bodies.Add(new Registrator());
                    MenuNavigator.Bodies.Add(new Schedule());
                    CurrentPage = MenuNavigator.Bodies[0];
                    break;
                case UserAccess.doctor:
                    break;
                case UserAccess.registrator:
                    break;
                case UserAccess.manager:
                    break;
                default:
                    break;
            }
        }
        
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            GetControls(UserAccess.admin);
        }

        public static Navigator MenuNavigator { get; } = new Navigator(new ObservableCollection<UserControl>());
        public static Navigator TabNavigator { get; } = new Navigator(new ObservableCollection<UserControl>());

        public static UserControl CurrentPage
        {
            get => _currentPage; 
            set 
            {
                _currentPage = null;
                OnStaticPropertyChanged(nameof(CurrentPage)); //refresh bindings
                _currentPage = value;
                OnStaticPropertyChanged(nameof(CurrentPage)); 
            } 
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void OnStaticPropertyChanged(string prop = "") { StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop)); }
    }
}
