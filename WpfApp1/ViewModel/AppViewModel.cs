using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace WpfApp1.ViewModel
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private Page currentPage;
        public ObservableCollection<Page> Pages { get; }

        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage == value) return;
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public AppViewModel()
        {

            Pages = new ObservableCollection<Page>
            {
               new View.Visit(),
               new View.Registration()
            };
            currentPage = Pages.ElementAt(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
