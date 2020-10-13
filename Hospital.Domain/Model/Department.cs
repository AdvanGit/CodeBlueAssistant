using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hospital.Domain.Model
{
    public class Department : INotifyPropertyChanged
    {
        private string _title;
        private Adress _adress;
        private Staff _manager;
        private ObservableCollection<Change> _changes;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public Adress Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }
        public Staff Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class Change : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}