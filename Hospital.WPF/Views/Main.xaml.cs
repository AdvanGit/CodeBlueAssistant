using Hospital.ViewModel;
using Hospital.ViewModel.Factories;
using Hospital.WPF.Controls;
using Hospital.WPF.Navigators;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.WPF.Views
{
    public partial class Main : MetroWindow
    {
        private static INavigatorItem _currentPage;

        private readonly IRootViewModelFactory<LoginViewModel> _loginViewModelFactory;

        public Main(IRootViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            _loginViewModelFactory = loginViewModelFactory;
            InitializeComponent();
            DataContext = new MainViewModel();
            MenuNavigator.Bodies.Add(new Login(_loginViewModelFactory.CreateViewModel()));
            CurrentPage = MenuNavigator.Bodies[0];
        }

        public static Navigator MenuNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>());
        public static Navigator TabNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>());

        public static INavigatorItem CurrentPage
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
