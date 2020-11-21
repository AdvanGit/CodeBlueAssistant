using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Hospital.UI.ViewModels
{
    class RegistratorViewModel : INotifyPropertyChanged
    {
        private UserControl _currentRegTable;
        public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }

        public ObservableCollection<UserControl> RegTables { get; set; }

        public RegistratorViewModel()
        {
            RegTables = new ObservableCollection<UserControl>
            {
                new Controls.RegDoctorTable(),
                new Controls.RegPatientTable()
            };
            CurrentRegTable = RegTables.ElementAt(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
