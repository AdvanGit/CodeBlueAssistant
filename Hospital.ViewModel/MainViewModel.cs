using System.ComponentModel;


namespace Hospital.ViewModel
{
    public enum UserAccess { admin, doctor, registrator, manager }

    public class MainViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
