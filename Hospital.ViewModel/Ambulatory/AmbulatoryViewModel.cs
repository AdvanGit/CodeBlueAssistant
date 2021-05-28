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

        private bool _isEditable;
        private string _caption;
        private Entry _currentEntry;
        private DiagnosticViewModel _diagnosticViewModel = new DiagnosticViewModel();
        private TherapyViewModel _therapyViewModel = new TherapyViewModel();
        private EntryViewModel _entryViewModel;

        private async void GetEntry(int entryId)
        {
            IsLoading = true;
            try
            {
                CurrentEntry = await ambulatoryDataService.GetEntryById(entryId);
                if (CurrentEntry.EntryStatus == Enum.Parse<EntryStatus>("3")) IsEditable = true;
                else NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Карта доступна только для чтения", true));
                if (CurrentEntry.MedCard == null) _currentEntry.MedCard = new MedCard { Patient = _currentEntry.Patient };
                DiagnosticViewModel.Initialize(CurrentEntry);
                TherapyViewModel.Initialize(CurrentEntry);
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

        public string Caption { get => _caption; private set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public bool IsEditable { get => _isEditable; private set { _isEditable = value; OnPropertyChanged(nameof(IsEditable)); }}
        public int EntryId { get; }

        public Entry CurrentEntry { get => _currentEntry; private set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; private set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }
        public TherapyViewModel TherapyViewModel { get => _therapyViewModel; private set { _therapyViewModel = value; OnPropertyChanged(nameof(TherapyViewModel)); } }
        public EntryViewModel EntryViewModel { get => _entryViewModel; private set { _entryViewModel = value; OnPropertyChanged(nameof(EntryViewModel)); } }

    }
}
