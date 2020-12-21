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

        //private string _searchValue;
        //public string SearchValue { get => _searchValue; set { _searchValue = value; OnPropertyChanged(nameof(SearchValue)); } }

        private Entry _selectedEntry;
        private Patient _selectedPatient;
        private Patient _editingPatient;

        public Entry SelectedEntry { get => _selectedEntry; set { _selectedEntry = value; OnPropertyChanged(nameof(SelectedEntry)); } }
        public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }
        public Patient EditingPatient { get => _editingPatient; set { _editingPatient = value; OnPropertyChanged(nameof(EditingPatient)); } }

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        private RelayCommand _selectEntry;
        private RelayCommand _selectPatient;
        //private RelayCommand _findDoctor;
        //private RelayCommand _findPatient;
        private RelayCommand _editPatient;

        public RelayCommand SelectEntry { get => _selectEntry ??= new RelayCommand(async obj => { if (obj != null) await GetEntriesBy(obj); }); }
        public RelayCommand SelectPatient { get => _selectPatient ??= new RelayCommand(obj => { if (obj != null) SelectedPatient = (Patient)obj; }); }
        public RelayCommand EditPatient { get => _editPatient ??= new RelayCommand(execute: p => EditingPatient = SelectedPatient, canExecute: p => { return SelectedPatient != null; }); }
        //public RelayCommand FindDoctor { get => _findDoctor ??= new RelayCommand(async obj => { if (SearchValue != null && SearchValue != "") await SearchDoctor(SearchValue); }); }
        //public RelayCommand FindPatient { get => _findPatient ??= new RelayCommand(async obj => { if (SearchValue != null && SearchValue != "") await SearchPatient(SearchValue); }); }

        public async Task SearchPatient(string value)
        {
            if (value != null && value != "")
            {
                Patients.Clear();
                IEnumerable<Patient> result = await registratorDataServices.FindPatient(value);
                foreach (Patient patient in result) Patients.Add(patient);
            }
        }
        public async Task SearchDoctor(string value)
        {
            if (value != null && value != "")
            {
                Doctors.Clear();
                IEnumerable<Entry> result = await registratorDataServices.FindDoctor(value);
                foreach (Entry entry in result) Doctors.Add(entry);
            }
        }
        public async Task GetEntriesBy(object obj)
        {
            SelectedEntry = (Entry)obj;
            Entries.Clear();
            IEnumerable<Entry> result = await registratorDataServices.GetEntries(SelectedEntry.DoctorDestination, (SelectedEntry.TargetDateTime));
            foreach (Entry entry in result) Entries.Add(entry);
        }
    }
}
