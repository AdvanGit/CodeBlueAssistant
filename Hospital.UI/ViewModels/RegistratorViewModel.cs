using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.UI.Controls;
using Hospital.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
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

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();
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

        public RelayCommand InsertData { get => _insertData ??= new RelayCommand(async obj => await GetFreeEntries()); }
        public RelayCommand EditUser
        {
            get => _editUser ??= new RelayCommand(async obj =>
            {
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
            get => _selectRow ??= new RelayCommand(async obj =>
            {
                if (obj.GetType() == typeof(Staff)) {
                    SelectedStaff = (Staff)obj;
                    await GetEntries(SelectedStaff);
                    }
                else if (obj.GetType() == typeof(Patient)) SelectedPatient = (Patient)obj;
            });
        }

        private async Task GetData()
        {
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                Patients.Clear();
                var _patients = await db.Patients.Include(p => p.Belay).ToListAsync();
                foreach (Patient patient in _patients) Patients.Add(patient);
            }
        }

        private async Task GetFreeEntries()
        {
            Doctors.Clear();
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                List<Change> allChanges = await db.Changes
                    .Include(c=>c.Staff).ThenInclude(s=>s.Department).ThenInclude(d=>d.Title)
                    .ToListAsync();

                for (int i = 0; i < allChanges.Count; i++)
                {
                    Change change = allChanges[i];

                    List<Entry> emptyEntries = new List<Entry>();
                    foreach (DateTime time in change.GetTimes()) emptyEntries
                            .Add(new Entry { CreateDateTime = DateTime.Now, TargetDateTime = time, DoctorDestination=change.Staff });
                    
                    List<Entry> entries = await db.Entries
                        .Where(e=>e.DoctorDestination == change.Staff)
                        .Where(e=>e.TargetDateTime.Date == change.DateTimeStart.Date)
                        .ToListAsync();

                    emptyEntries.AddRange(entries);

                    var result = emptyEntries
                        .OrderBy(e => e.TargetDateTime)
                        .GroupBy(e => e.TargetDateTime)
                        .Select(e => e.Last())
                        .Where(e => e.EntryStatus == EntryStatus.Open)
                        .GroupBy(r => r.DoctorDestination)
                        .Select(r => r.FirstOrDefault());

                    if (result.Count() != 0)
                    {
                        allChanges.RemoveAll(c => c.Staff == change.Staff);
                        i--;
                    }

                    foreach (Entry entry in result) Doctors.Add(entry);
                }

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
        private async Task GetEntries(Staff selectedStaff)
        {
            Entries.Clear();
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                List<Entry> entries = await db.Entries.Where(e=>e.DoctorDestination==selectedStaff).Include(e=>e.Patient).ToListAsync();
                List<Entry> emptyEntries = new List<Entry>();

                var change = db.Changes.Where(c => c.Staff == selectedStaff).FirstOrDefault();
                //foreach (Change change in db.Changes.Where(c => c.Staff == selectedStaff).ToList())

                foreach (DateTime time in change.GetTimes()) emptyEntries.Add(new Entry { CreateDateTime = DateTime.Now, TargetDateTime = time });
                
                emptyEntries.AddRange(entries);
                var result = emptyEntries.OrderBy(e => e.TargetDateTime).GroupBy(e => e.TargetDateTime).Select(e => e.Last());
                foreach (Entry entry in result) Entries.Add(entry);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
