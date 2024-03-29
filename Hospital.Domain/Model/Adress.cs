﻿
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hospital.Domain.Model
{
    public class Adress : INotifyPropertyChanged
    {
        private string _city, _street, _number;
        private int? _room;

        public string City { get => _city; set { _city = value; OnPropertyChanged("City"); } }
        public string Street { get => _street; set { _street = value; OnPropertyChanged("Street"); } }
        public string Number { get => _number; set { _number = value; OnPropertyChanged("Number"); } }
        public int? Room { get => _room; set { _room = value; OnPropertyChanged("Room"); } }

        public string ToLongString()
        {
            string res = $"г.{_city} ул.{_street} д.{_number}";
            if (_room != null && _room != 0) res += $" кв.{_room}";
            return res;
        }

        public string ToShortString()
        {
            string res = $"ул.{_street} д.{_number}";
            if (_room != null && _room != 0) res += $" кв.{_room}";
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
