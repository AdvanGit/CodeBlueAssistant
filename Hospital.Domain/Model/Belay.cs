using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hospital.Domain.Model
{
    public class Belay : INotifyPropertyChanged
    {
        private string _title;
        private string _info;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }

        //public ObservableCollection<Patient> Patients { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
