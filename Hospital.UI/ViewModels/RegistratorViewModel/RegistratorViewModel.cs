using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.UI.Controls.Registrator;
using Hospital.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hospital.UI.ViewModels.RegistratorViewModel
{
    public class RegistratorViewModel : INotifyPropertyChanged
    {
        public RegistratorViewModel()
        {
            
            CurrentRegTable = RegTables[0];
        }

        private string _searchValue;
        public string SearchValue { get => _searchValue; set { _searchValue = value; OnPropertyChanged(nameof(SearchValue)); } }

        private UserControl _currentRegTable;
        public UserControl CurrentRegTable { get => _currentRegTable; set { _currentRegTable = value; OnPropertyChanged(nameof(CurrentRegTable)); } }
        public List<UserControl> RegTables { get; } = new List<UserControl> { new RegDoctorTable(), new RegPatientTable(), new RegEntryTable() };

        public ObservableCollection<Entry> Doctors { get; } = new ObservableCollection<Entry>();

        private RelayCommand _findByString;
        public RelayCommand FindByString
        {
            get => _findByString ??= new RelayCommand(async obj => { if (SearchValue != null) await GetFreeEntries(); });
        }

        private async Task GetFreeEntries()
        {
            Doctors.Clear();
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            {
                string[] words = SearchValue.Split(' ');

                var allChanges = await db.Changes
                    .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .AsAsyncEnumerable()
                    .Where(c => 
                    ((
                        (words.Any(word => c.Staff.FirstName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                        (words.Any(word => c.Staff.MidName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                        (words.Any(word => c.Staff.LastName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                        (words.Any(word => (c.Staff.Qualification != null) && (c.Staff.Qualification.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) +
                        (words.Any(word => (c.Staff.Department.Title.Title != null) && (c.Staff.Department.Title.Title.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) +
                        (words.Any(word => (c.Staff.Department.Adress.Street != null) && (c.Staff.Department.Adress.Street.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0)
                        >= words.Count())
                    ))
                    .ToListAsync();

                for (int i = 0; i < allChanges.Count; i++)
                {
                    Change change = allChanges[i];

                    List<Entry> emptyEntries = new List<Entry>();
                    foreach (DateTime time in change.GetTimes()) emptyEntries
                            .Add(new Entry { CreateDateTime = DateTime.Now, TargetDateTime = time, DoctorDestination = change.Staff });

                    List<Entry> entries = await db.Entries.AsQueryable()
                        .Where(e => e.DoctorDestination == change.Staff)
                        .Where(e => e.TargetDateTime.Date == change.DateTimeStart.Date)
                        .ToListAsync();

                    emptyEntries.AddRange(entries);

                    var result = emptyEntries
                        .OrderBy(e => e.TargetDateTime)
                        .GroupBy(e => e.TargetDateTime)
                        .Select(e => e.Last())
                        .Where(e => e.EntryStatus == EntryStatus.Open)
                        .GroupBy(r => r.DoctorDestination)
                        .Select(r => r.FirstOrDefault());

                    if (result.Count() != 0)
                    {
                        allChanges.RemoveAll(c => c.Staff == change.Staff);
                        i--;
                    }

                    foreach (Entry entry in result) Doctors.Add(entry);
                }

            }
        }








        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
