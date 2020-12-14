using System.ComponentModel;


namespace Hospital.ViewModel
{
    public enum UserAccess { admin, doctor, registrator, manager }

    public class MainViewModel : INotifyPropertyChanged

    {
        //private RelayCommand _createData;

        //public RelayCommand CreateData
        //{
        //    get => _createData ?? (_createData = new RelayCommand(obj =>
        //        { Task.Run(() => (new ContentCreate(new HospitalDbContextFactory()))); }));
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
