using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hospital.Domain.Model
{
    public class DomainObject : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
