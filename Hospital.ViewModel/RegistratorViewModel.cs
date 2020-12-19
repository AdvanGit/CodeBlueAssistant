using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class RegistratorViewModel : MainViewModel
    {
        private readonly RegistratorDataServices registratorDataServices = new RegistratorDataServices(new HospitalDbContextFactory());

        private string _searchValue;
        public string SearchValue { get => _searchValue; set { _searchValue = value; OnPropertyChanged(nameof(SearchValue)); } }

        private Entry _selectedEntry;
        public Entry SelectedEntry { get => _selectedEntry; set { _selectedEntry = value; OnPropertyChanged(nameof(SelectedEntry)); } }
        public bool EntryLocker { get; private set; }

        private Patient _selectedPatient;
        public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        private RelayCommand _selectEntry;
        private RelayCommand _selectPatient;
        private RelayCommand _findDoctor;
        private RelayCommand _findPatient;

        public RelayCommand SelectEntry
        {
            get => _selectEntry ??= new RelayCommand(async obj =>
            {
                if (obj != null) await GetEntriesBy(obj);
            });
        }
        public RelayCommand SelectPatient { get => _selectPatient ??= new RelayCommand(obj => { if (obj != null) SelectedPatient = (Patient)obj; }); }
        public RelayCommand FindDoctor { get => _findDoctor ??= new RelayCommand(async obj => { if (SearchValue != null && SearchValue != "") await SearchDoctor(); }); }
        public RelayCommand FindPatient { get => _findPatient ??= new RelayCommand(async obj => { if (SearchValue != null && SearchValue != "") await SearchPatient(); }); }

        private async Task SearchPatient()
        {
            Patients.Clear();
            IEnumerable<Patient> result = await registratorDataServices.FindPatient(SearchValue);
            foreach (Patient patient in result) Patients.Add(patient);
        }
        private async Task SearchDoctor()
        {
            Doctors.Clear();
            IEnumerable<Entry> result = await registratorDataServices.FindDoctor(SearchValue);
            foreach (Entry entry in result) Doctors.Add(entry);
        }
        private async Task GetEntriesBy(object obj)
        {
            SelectedEntry = (Entry)obj;
            Entries.Clear();
            IEnumerable<Entry> result = await registratorDataServices.GetEntries(SelectedEntry.DoctorDestination, (SelectedEntry.TargetDateTime));
            foreach (Entry entry in result) Entries.Add(entry);
        }
    }
}
