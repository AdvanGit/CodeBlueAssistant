using Hospital.ViewModel.Factories;
using Hospital.WPF.Navigators;
using Hospital.WPF.Services.States;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.WPF.Views
{
    public partial class Main : MetroWindow
    {
        private static INavigatorItem _currentPage;

        public static ViewStateManager ViewStateManager { get; private set; }
        public static Navigator TabNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>());

        public Main(IRootViewModelFactory viewModelFactory)
        {
            DataContext = viewModelFactory.CreateMainViewModel();

            ViewStateManager = new ViewStateManager(viewModelFactory);
            CurrentPage = ViewStateManager.Navigator.CurrentBody;

            InitializeComponent();
        }

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
