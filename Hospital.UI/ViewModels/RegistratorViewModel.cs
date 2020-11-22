using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.UI.ViewModels
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }

    class RegistratorViewModel : INotifyPropertyChanged
    {
        public RegistratorViewModel()
        {
            RegTables = new ObservableCollection<UserControl>
            {
                new Controls.RegDoctorTable(),
                new Controls.RegPatientTable()
            };
            CurrentRegTable = RegTables.ElementAt(0);
        }

        private UserControl _currentRegTable;
        public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }

        public ObservableCollection<Staff> Doctors { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<UserControl> RegTables { get; set; }

        private RelayCommand _testCommand;
        private RelayCommand _insertData;

        public RelayCommand TestCommand
        {
            get
            {
                return _testCommand ?? (_testCommand = new RelayCommand(obj => {MessageBox.Show("Команда");} )  );
            }
        }
        public RelayCommand InsertData { get => _insertData ?? (_insertData = new RelayCommand(obj => { GetData(); })); }

        public async void GetData()
        {
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                IEnumerable<Patient> patients = await db.Patients.Include(e=>e.Belay).ToListAsync();
                Patients = patients.ToObservableCollection();

                IEnumerable<Staff> doctors = await db.Staffs.Include(s => s.Department).ThenInclude(d => d.Title).ToListAsync();
                Doctors = doctors.ToObservableCollection();
            }
        }

        public async void GetServicesData()
        {
            IDataServices<Patient> PatientServices = new GenericDataServices<Patient>(new HospitalDbContextFactory());
            IEnumerable<Patient> patients = await PatientServices.GetAll();
            Patients = patients.ToObservableCollection();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
}
}
