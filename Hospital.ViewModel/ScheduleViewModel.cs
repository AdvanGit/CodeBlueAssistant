using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Ambulatory;
using Hospital.ViewModel.Factories;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class ScheduleViewModel : MainViewModel
    {
        private readonly AmbulatoryViewModelFactory _ambulatoryViewModelFactory;
        private readonly ScheduleDataService _scheduleDataServices;

        public ScheduleViewModel(ScheduleDataService scheduleDataServices, AmbulatoryViewModelFactory ambulatoryViewModelFactory)
        {
            _scheduleDataServices = scheduleDataServices;
            _ambulatoryViewModelFactory = ambulatoryViewModelFactory;
            SelectedDate = new DateTime(2021, 05, 03);
        }

        public DateTime SelectedDate { get => _selectedDate; set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); GetEntry(value).ConfigureAwait(true); } }
        private DateTime _selectedDate;

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();


        private async Task GetEntry(DateTime date)
        {
            IsLoading = true;
            try
            {
                Entries.Clear();
                var result = await _scheduleDataServices.GetEntriesByDate(CurrentStuffId, date);
                Entries.Clear();
                foreach (Entry entry in result) Entries.Add(entry);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoading = false;
        }

        public AmbulatoryViewModel CreateAmbulatoryViewModel()
        {
            return _ambulatoryViewModelFactory.CreateViewModel(CurrentEntry.Id);
        }
    }
}
