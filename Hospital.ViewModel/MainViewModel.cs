using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System.ComponentModel;


namespace Hospital.ViewModel
{
    public enum UserAccess { admin, doctor, registrator, manager }

    public class MainViewModel : INotifyPropertyChanged

    {
        public static int CurrentStuffId { get; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
