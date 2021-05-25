using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;

namespace Hospital.ViewModel.Ambulatory
{
    public class AmbulatoryViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private string _caption;
        private Entry _currentEntry;
        private DiagnosticViewModel _diagnosticViewModel = new DiagnosticViewModel();
        private TherapyViewModel _therapyViewModel;
        private EntryViewModel _entryViewModel;

        private async void GetEntry(int entryId)
        {
            IsLoading = true;
            try
            {
                CurrentEntry = await ambulatoryDataService.GetEntryById(entryId);
                if (CurrentEntry.MedCard == null) _currentEntry.MedCard = new MedCard { Patient = _currentEntry.Patient, TherapyDoctor = _currentEntry.DoctorDestination };
                DiagnosticViewModel.Initialize(CurrentEntry);
                TherapyViewModel = new TherapyViewModel(CurrentEntry);
                EntryViewModel = new EntryViewModel(CurrentEntry);
                Caption = CurrentEntry.TargetDateTime.ToShortTimeString();
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 5);
            }
            IsLoading = false;
        }

        public AmbulatoryViewModel(int entryId)
        {
            EntryId = entryId;
            GetEntry(entryId);
        }

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public int EntryId { get; }

        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }
        public TherapyViewModel TherapyViewModel { get => _therapyViewModel; set { _therapyViewModel = value; OnPropertyChanged(nameof(TherapyViewModel)); } }
        public EntryViewModel EntryViewModel { get => _entryViewModel; set { _entryViewModel = value; OnPropertyChanged(nameof(EntryViewModel)); } }

    }
}
