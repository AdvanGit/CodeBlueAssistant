using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.View;

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

            CurrentPage = new View.Visit();

            Pages = new ObservableCollection<Page>
            {
               new View.Visit(), new View.Registration()
            };
            CurrentPage = new View.Visit();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
