using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.UI.Controls;
using Hospital.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

    public class RegistratorViewModel : INotifyPropertyChanged
    {
        public RegistratorViewModel()
        {
            RegTables = new List<UserControl>
            {
                new Controls.RegDoctorTable(),
                new Controls.RegPatientTable()
            };
            CurrentRegTable = RegTables.ElementAt(0);
            CurrentRegPanel = new RegEntryPanel();
            dataServicesPatient = new GenericDataServices<Patient>(new HospitalDbContextFactory());
        }

        private UserControl _currentRegTable;
        private UserControl _currentRegPanel;
        private Patient _selectedPatient;
        private Patient _editingPatient;
        private Staff _selectedStaff;
        private RelayCommand _insertData;
        private RelayCommand _editUser;
        private RelayCommand _editCancel;
        private RelayCommand _createPatient;
        private RelayCommand _savePatient;
        private GenericDataServices<Patient> dataServicesPatient;

        public IEnumerable<Belay> Belays { get; private set; }

        public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }
        public UserControl CurrentRegPanel { get => _currentRegPanel; set { _currentRegPanel = value; OnPropertyChanged(nameof(CurrentRegPanel)); } }
        public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }
        public Patient EditingPatient { get => _editingPatient; set { _editingPatient = value; OnPropertyChanged(nameof(EditingPatient)); } }

        public Staff SelectedStaff { get => _selectedStaff; set { _selectedStaff = value; OnPropertyChanged(nameof(SelectedStaff)); } }

        public RelayCommand InsertData { get => _insertData ??= new RelayCommand(async obj => await GetData()); }
        public RelayCommand EditUser
        {
            get => _editUser ??= new RelayCommand(obj =>
                {
                    EditingPatient = (Patient)_selectedPatient.Clone();
                    CurrentRegPanel = new RegEditPanel();
                });
        }
        public RelayCommand EditCancel { get => _editCancel ??= new RelayCommand(obj => { CurrentRegPanel = new RegEntryPanel(); }); }
        public RelayCommand CreatePatient
        {
            get => _createPatient ??= new RelayCommand(async obj => { EditingPatient = new Patient(); CurrentRegPanel = new RegEditPanel(); await GetBelays(); });
        }
        public RelayCommand SavePatient { get => _savePatient ??= new RelayCommand(async obj => await dataServicesPatient.Update(EditingPatient.Id, EditingPatient)); }

        public ObservableCollection<Staff> Doctors { get; set; }
        public List<UserControl> RegTables { get; set; }

        public ObservableCollection<Patient> Patients { get; set; }

        public async Task GetData()
        {
            await using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                var _patients = await db.Patients.Include(p => p.Belay).ToListAsync();
                Belays ??= await db.Belays.ToListAsync();
                Patients = _patients.ToObservableCollection();
                var _doctors = await db.Staffs.Include(s => s.Department).ThenInclude(d => d.Title).ToListAsync();
                Doctors = _doctors.ToObservableCollection();
            }
        }
        public async Task GetBelays()
        {
            if (Belays == null)
                await using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
                {
                    Belays = await db.Belays.ToListAsync();
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
