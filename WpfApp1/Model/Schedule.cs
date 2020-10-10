using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1.Model
{
    class Schedule : INotifyPropertyChanged
    {
        private ObservableCollection<DateTime> _dateTimes;
        private Staff _staff; 


        //public ObservableCollection<DateTime> GetFree()
        //{


        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
