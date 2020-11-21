using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


public enum UserAccess { admin, doctor, registrator, manager }

namespace Hospital.UI.ViewModels 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentPage;

        public ObservableCollection<UserControl> Pages { get; }
        public UserControl CurrentPage { get => _currentPage; set { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); } }

        public MainViewModel(UserAccess userAccess)
        {
            switch (userAccess)
            {
                case UserAccess.admin:
                    Pages = new ObservableCollection<UserControl>
                    {
                        new Views.Registrator(),
                        new Views.Schedule(),

                        new Views.Ambulatory()
                        
                    };
                    CurrentPage = Pages.ElementAt(0);
                    break;
                case UserAccess.doctor:
                    break;
                case UserAccess.registrator:
                    Pages = new ObservableCollection<UserControl>
                    {
                        new Views.Registrator(),
                    };
                    CurrentPage = Pages.ElementAt(0);
                    break;
                case UserAccess.manager:
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
