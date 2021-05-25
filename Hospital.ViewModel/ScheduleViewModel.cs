using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hospital.ViewModel
{
    public class ScheduleViewModel : MainViewModel
    {
        private ScheduleDataServices scheduleDataServices = new ScheduleDataServices(new HospitalDbContextFactory());

        public DateTime SelectedDate { get => _selectedDate; set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); GetEntry(value); } }
        private DateTime _selectedDate;

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        public ObservableCollection<Entry> Entries { get; } = new ObservableCollection<Entry>();

        public ScheduleViewModel()
        {
            SelectedDate = new DateTime(2021, 05, 03);
        }

        private async void GetEntry(DateTime date)
        {
            IsLoading = true;
            try
            {
                Entries.Clear();
                var result = await scheduleDataServices.GetEntriesByDate(CurrentStuffId, date);
                foreach (Entry entry in result) Entries.Add(entry);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex,4);
            }
            IsLoading = false;
        }
    }
}
