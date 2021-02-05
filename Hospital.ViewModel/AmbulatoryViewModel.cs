using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;

namespace Hospital.ViewModel
{
    public class AmbulatoryViewModel : MainViewModel
    {
        private AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        public AmbulatoryViewModel()
        {
            GetEntry(1);
        }

        private async void GetEntry(int entryId)
        {
            CurrentEntry = await ambulatoryDataService.GetEntriesById(1);
        }
    }
}
