using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hospital.Domain.Model
{
    public class DomainObject : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
