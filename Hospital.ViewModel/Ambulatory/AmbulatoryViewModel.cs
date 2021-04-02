using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class AmbulatoryViewModel : MainViewModel
    {
        public AmbulatoryViewModel()
        {
            GetEntry(3);
        }

        private static readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        private DiagnosticViewModel _diagnosticViewModel;
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }

        private TherapyViewModel _therapyViewModel;
        public TherapyViewModel TherapyViewModel { get => _therapyViewModel; set { _therapyViewModel = value; OnPropertyChanged(nameof(TherapyViewModel)); } }

        private async void GetEntry(int entryId)
        {
            Task<Entry> task = ambulatoryDataService.GetEntryById(entryId);
            CurrentEntry = await task;
            if (task.IsCompleted)
            {
                DiagnosticViewModel = new DiagnosticViewModel(CurrentEntry);
                TherapyViewModel = new TherapyViewModel(CurrentEntry);
            }
        }
    }
}
