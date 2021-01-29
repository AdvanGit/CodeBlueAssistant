using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Filters;
using Hospital.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class RegistratorViewModel : MainViewModel
    {
        private readonly RegistratorDataServices registratorDataServices = new RegistratorDataServices(new HospitalDbContextFactory());
        private readonly GenericDataServices<Belay> genericDataServicesBelay = new GenericDataServices<Belay>(new HospitalDbContextFactory());
        private readonly GenericDataServices<Patient> genericDataServicesPatient = new GenericDataServices<Patient>(new HospitalDbContextFactory());
        private readonly GenericDataServices<Entry> genericDataServicesEntry = new GenericDataServices<Entry>(new HospitalDbContextFactory());

        private Entry _selectedEntry;
        private Patient _selectedPatient;
        private Patient _editingPatient;

        public Entry SelectedEntry { get => _selectedEntry; set { _selectedEntry = value; OnPropertyChanged(nameof(SelectedEntry)); } }
        public Patient SelectedPatient { get => _selectedPatient; set { _selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); } }
        public Patient EditingPatient { get => _editingPatient; set { _editingPatient = value; OnPropertyChanged(nameof(EditingPatient)); } }

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();
        public ObservableCollection<Belay> Belays { get; } = new ObservableCollection<Belay>();

        private RegistratorFilter _filter = new RegistratorFilter() 
        {
            IsName = true, IsFree = true, IsGroup=true, IsDepartment = true, IsAdress = true, IsQualification = true, DateTime = DateTime.Now 
        };
        public RegistratorFilter Filter { get => _filter; set { _filter = value; OnPropertyChanged(nameof(Filter)); } } 

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
                IEnumerable<Entry> result = await registratorDataServices.FindDoctor(value, _filter);
                foreach (Entry entry in result) Doctors.Add(entry);
            }
        }
        public async Task GetEntries(object obj)
        {
            if (obj != null)
            {
                SelectedEntry = (Entry)obj;
                Entries.Clear();
                IEnumerable<Entry> result = await registratorDataServices.GetEntries(SelectedEntry.DoctorDestination, (SelectedEntry.TargetDateTime));
                foreach (Entry entry in result) Entries.Add(entry);
            }
        }
        public async Task GetBelays()
        {
            if (Belays.Count == 0)
            {
                IEnumerable<Belay> result = await genericDataServicesBelay.GetAll();
                foreach (Belay belay in result) Belays.Add(belay);
            }
        }
        public async Task SavePatient()
        {
            await genericDataServicesPatient.Update(EditingPatient.Id, EditingPatient);
            SelectedPatient = EditingPatient;
            Patients.Clear();
            Patients.Add(await genericDataServicesPatient.GetById(SelectedPatient.Id));
        }
        public async Task CreateEntry()
        {
                SelectedEntry.Patient = SelectedPatient;
                SelectedEntry.Registrator = SelectedEntry.DoctorDestination; //---заглушка отсутсвия данных аккаунта
                SelectedEntry.EntryStatus = EntryStatus.Await;
                await genericDataServicesEntry.Update(SelectedEntry.Id, SelectedEntry);
                SelectedEntry = null;
                SelectedPatient = null;
        } //check on exist баг создания записи, если уже есть запись на текущее время. вследствии механизма автоматического выбора. сделать проверку, и предложение заменить, проапдейтить запись



        public void SelectEntity(object entity)
        {
            if (entity != null && entity.GetType() == typeof(Patient)) SelectedPatient = (Patient)entity;
            else if (entity != null && entity.GetType() == typeof(Entry)) SelectedEntry = (Entry)entity;
        }
        public void EditPatient(bool isNew)
        {
            if (isNew) EditingPatient = new Patient();
            else EditingPatient = (Patient)SelectedPatient.Clone();
        }
    }
}
