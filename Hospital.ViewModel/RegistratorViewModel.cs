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
        private RelayCommand _findByString;


        public string SearchValue { get => _searchValue; set { _searchValue = value; OnPropertyChanged(nameof(SearchValue)); } }

        public RelayCommand FindByString { get => _findByString ??= new RelayCommand(async obj => { if (SearchValue != null) await SearchByString(); }); }

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();

        private async Task SearchByString()
        {
            Doctors.Clear();
            IEnumerable<Entry> result = await registratorDataServices.FindByString(SearchValue);
            foreach (Entry entry in result) Doctors.Add(entry);
        }
    }
}
