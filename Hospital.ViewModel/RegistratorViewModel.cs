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
        private RegistratorDataServices registratorDataServices = new RegistratorDataServices(new HospitalDbContextFactory());

        private string _searchValue;
        public string SearchValue { get => _searchValue; set { _searchValue = value; OnPropertyChanged(nameof(SearchValue)); } }

        private Entry _selectedEntry;
        public Entry SelectedEntry { get => _selectedEntry; set { _selectedEntry = value; OnPropertyChanged(nameof(SelectedEntry)); } }

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();

        private RelayCommand _findByString;
        private RelayCommand _selectEntry;

        public RelayCommand SelectEntry { get => _selectEntry ??= new RelayCommand(async obj => { if (obj != null) await GetEntriesBy(obj); }); }
        public RelayCommand FindByString { get => _findByString ??= new RelayCommand(async obj => { if (SearchValue != null) await SearchByString(); }); }


        private async Task SearchByString()
        {
            SelectedEntry = null;
            Doctors.Clear();
            IEnumerable<Entry> result = await registratorDataServices.FindByString(SearchValue);
            foreach (Entry entry in result) Doctors.Add(entry);
        }
        private async Task GetEntriesBy(object obj)
        {
            SelectedEntry = (Entry)obj;
            Entries.Clear();
            IEnumerable<Entry> result = await  registratorDataServices.GetEntries(((Entry)obj).DoctorDestination, ((Entry)obj).TargetDateTime);
            foreach (Entry entry in result) Entries.Add(entry);
        }
    }
}
