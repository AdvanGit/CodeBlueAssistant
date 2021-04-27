using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class AmbulatoryViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        private DiagnosticViewModel _diagnosticViewModel;
        private TherapyViewModel _therapyViewModel;
        private EntryViewModel _entryViewModel;

        private async void GetEntry(int entryId)
        {
            Task<Entry> task = ambulatoryDataService.GetEntryById(entryId);
            CurrentEntry = await task;
            if (task.IsCompleted)
            {
                DiagnosticViewModel = new DiagnosticViewModel(CurrentEntry, ambulatoryDataService);
                TherapyViewModel = new TherapyViewModel(CurrentEntry, ambulatoryDataService);
                EntryViewModel = new EntryViewModel(CurrentEntry, ambulatoryDataService);
            }
        }

        public AmbulatoryViewModel()
        {
            GetEntry(53);
        }

        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }
        public TherapyViewModel TherapyViewModel { get => _therapyViewModel; set { _therapyViewModel = value; OnPropertyChanged(nameof(TherapyViewModel)); } }
        public EntryViewModel EntryViewModel { get => _entryViewModel; set { _entryViewModel = value; OnPropertyChanged(nameof(EntryViewModel)); } }


    }
}
