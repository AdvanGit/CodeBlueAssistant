using Hospital.Domain.Model;
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
using System.Windows.Controls;

namespace Hospital.UI.ViewModels
{
    public class RegistratorViewModel : INotifyPropertyChanged
    {
        public RegistratorViewModel()
        {
            CurrentRegTable = RegTables.ElementAt(0);
            CurrentRegPanel = new RegEntryPanel();
        }

        private UserControl _currentRegTable;
        private UserControl _currentRegPanel;
        private Patient _selectedPatient;
        private Patient _editingPatient;
        private Staff _selectedStaff;

        public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }
        public UserControl CurrentRegPanel { get => _currentRegPanel; set { _currentRegPanel = value; OnPropertyChanged(nameof(CurrentRegPanel)); } }
        public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }
        public Patient EditingPatient { get => _editingPatient; set { _editingPatient = value; OnPropertyChanged(nameof(EditingPatient)); } }
        public Staff SelectedStaff { get => _selectedStaff; set { _selectedStaff = value; OnPropertyChanged(nameof(SelectedStaff)); } }

        public ObservableCollection<Staff> Doctors { get; } = new ObservableCollection<Staff>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();
        public ObservableCollection<Belay> Belays { get; } = new ObservableCollection<Belay>();
        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();

        public List<UserControl> RegTables { get; } = new List<UserControl> { new Controls.RegDoctorTable(), new Controls.RegPatientTable() };

        private readonly GenericDataServices<Patient> dataServicesPatient = new GenericDataServices<Patient>(new HospitalDbContextFactory());

        private RelayCommand _insertData;
        private RelayCommand _selectRow;
        private RelayCommand _editUser;
        private RelayCommand _editCancel;
        private RelayCommand _createPatient;
        private RelayCommand _savePatient;

        public RelayCommand InsertData { get => _insertData ??= new RelayCommand(async obj => await GetData()); }
        public RelayCommand EditUser
        {
            get => _editUser ??= new RelayCommand(async obj => {
                EditingPatient = (Patient)SelectedPatient.Clone();
                CurrentRegPanel = new RegEditPanel();
                await GetBelays();
            });
        }
        public RelayCommand EditCancel { get => _editCancel ??= new RelayCommand(obj => CurrentRegPanel = new RegEntryPanel()); }
        public RelayCommand CreatePatient
        {
            get => _createPatient ??= new RelayCommand(async obj => { EditingPatient = new Patient(); CurrentRegPanel = new RegEditPanel(); await GetBelays(); });
        }
        public RelayCommand SavePatient { get => _savePatient ??= new RelayCommand(async obj => await dataServicesPatient.Update(EditingPatient.Id, EditingPatient)); }
        public RelayCommand SelectRow
        {
            get => _selectRow ??= new RelayCommand(obj => {
                if (obj.GetType() == typeof(Staff)) SelectedStaff = (Staff)obj;
                else if (obj.GetType() == typeof(Patient)) SelectedPatient = (Patient)obj;
            });
        }

        private async Task GetData()
        {
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                Doctors.Clear();
                var _doctors = await db.Staffs.Include(s => s.Department).ThenInclude(d => d.Title).ToListAsync();
                foreach (Staff staff in _doctors) Doctors.Add(staff);

                Patients.Clear();
                var _patients = await db.Patients.Include(p => p.Belay).ToListAsync();
                foreach (Patient patient in _patients) Patients.Add(patient);

                Entries.Clear();
                var _entries = await db.Entries.Include(e => e.Patient).Include(e => e.DoctorDestination).ToListAsync();
                foreach (Entry entry in _entries) Entries.Add(entry);
            }
        }
        private async Task GetBelays()
        {
            if (Belays.Count == 0)
                using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
                {
                    var _belays = await db.Belays.ToListAsync();
                    foreach (Belay belay in _belays) Belays.Add(belay);
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
