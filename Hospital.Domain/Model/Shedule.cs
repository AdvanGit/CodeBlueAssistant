using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hospital.Domain.Model
{
    public class Schedule : INotifyPropertyChanged
    {
        private Staff _staff;
        private Change _change;
        private DateTime _targetTime;
        //private Transfer _transfer;

        public int Id { get; set; }
        public Staff Staff
        {
            get => _staff;
            set
            {
                _staff = value;
                OnPropertyChanged("Staff");
            }
        }
        public Change Change
        {
            get => _change;
            set
            {
                _change = value;
                OnPropertyChanged("Change");
            }
        }
        public DateTime TargetTime
        {
            get => _targetTime;
            set
            {
                _targetTime = value;
                OnPropertyChanged("TargetTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
