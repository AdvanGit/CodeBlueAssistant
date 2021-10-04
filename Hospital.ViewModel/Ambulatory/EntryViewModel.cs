using Hospital.Domain.Filters;
using Hospital.Domain.Model;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{

    public class EntryViewModel : MainViewModel
    {
        private readonly EntryDataService _entryDataServices;

        private Entry _currentEntry;
        private Entry _selectedEntry;
        private Entry _entryOut;
        private EntrySearchFilter _filter = new EntrySearchFilter
        {
            DepartmentType = DepartmentType.Ambulatory,
            IsAdress = true,
            IsDepartment = true,
            IsQualification = true,
            IsName = true,
            IsNearest = true,
            IsFree = true,
            DateTime = DateTime.Now
        };

        public EntryViewModel(EntryDataService entryDataServices)
        {
            _entryDataServices = entryDataServices;
        }

        public void Initialize(Entry currentEntry)
        {
            CurrentEntry = currentEntry;
        }

        public ObservableCollection<Entry> FindedEntries { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> BySelectEntries { get; } = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> FilteredCollection { get; } = new ObservableCollection<Entry>();

        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }
        public Entry SelectedEntry { get => _selectedEntry; set { _selectedEntry = value; OnPropertyChanged(nameof(SelectedEntry)); } }
        public Entry EntryOut { get => _entryOut; set { _entryOut = value; OnPropertyChanged(nameof(EntryOut)); } }

        public EntrySearchFilter Filter { get => _filter; set { _filter = value; OnPropertyChanged(nameof(Filter)); } }

        public async void FindEntry(string searchValue)
        {
            IsLoading = true;
            try
            {
                FindedEntries.Clear();
                var res = await _entryDataServices.FindDoctor(searchValue, Filter);
                FindedEntries.Clear();
                if (res.Count() > 0) foreach (Entry entry in res) FindedEntries.Add(entry);
                else NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Ничего не найдено"));
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 6);
            }
            IsLoading = false;
        }
        public async void FindBySelect(object item)
        {
            SelectedEntry = (Entry)item;
            IsLoading = true;
            try
            {
                BySelectEntries.Clear();
                FilteredCollection.Clear();
                var res = await _entryDataServices.GetEntries(SelectedEntry.DoctorDestination.Id, SelectedEntry.TargetDateTime.Date);
                BySelectEntries.Clear();
                FilteredCollection.Clear();
                foreach (Entry _entry in res) BySelectEntries.Add(_entry);
                foreach (Entry _entry in res.Where(e => e.EntryStatus == EntryStatus.Открыта)) FilteredCollection.Add(_entry);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 6);
            }
            IsLoading = false;
        }
        public void SetEntryOut(object item) => EntryOut = (Entry)item;

        public async Task ToAbsence()
        {
            try
            {
                CurrentEntry.EntryStatus = EntryStatus.Неявка;

                await _entryDataServices.UpdateEntry(CurrentEntry);
                NotificationManager.AddItem(new NotificationItem(NotificationType.Success, TimeSpan.FromSeconds(2), "Запись успешно обновлена"));
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 6);
            }
        }

        public void ApplyNextEntry()
        {
            try
            {
                EntryOut.Registrator = CurrentEntry.DoctorDestination;
                EntryOut.Patient = CurrentEntry.Patient;
                EntryOut.CreateDateTime = DateTime.Now;
                EntryOut.EntryStatus = EntryStatus.Ожидание;
                CurrentEntry.MedCard.TherapyDoctor = CurrentEntry.DoctorDestination;
                CurrentEntry.MedCard.Option = "test option";
                EntryOut.MedCard = CurrentEntry.MedCard;
                CurrentEntry.EntryStatus = EntryStatus.Закрыта;
                CurrentEntry.EntryOut = EntryOut;
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 6);
            }
        }
    }
}
