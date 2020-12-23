using Hospital.Domain.Model;
using Hospital.EntityFramework.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class RegistratorDataServices
    {
        private readonly HospitalDbContextFactory _contextFactory;

        public RegistratorDataServices(HospitalDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Entry>> FindDoctor(string _string, RegistratorFilter filter)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                //парсинг строки
                string[] words = Regex.Replace(_string, @"\s+", " ").Split(' ');
                List<Entry> result = new List<Entry>();

                // поиск по стороке
                var allChanges = await db.Changes
                    .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .AsAsyncEnumerable()
                    .Where(c => (filter.IsDate ? (c.DateTimeStart.Date == filter.DateTime.Date) : (true))) //true => c.DatetimeStart.Date >= filter.DateTime.Date
                    .Where(c => (
                            ((filter.IsName) ? ((words.Any(word => c.Staff.FirstName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                                                (words.Any(word => c.Staff.MidName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                                                (words.Any(word => c.Staff.LastName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0)) : 0) +
                            ((filter.IsQualification) ? (words.Any(word => (c.Staff.Qualification != null) && (c.Staff.Qualification.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0):0) +
                            ((filter.IsDepartment) ? (words.Any(word => (c.Staff.Department.Title.Title != null) && (c.Staff.Department.Title.Title.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0): 0) +
                            ((filter.IsAdress) ? (words.Any(word => (c.Staff.Department.Adress.Street != null) && (c.Staff.Department.Adress.Street.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0): 0)
                            >= words.Count()))
                    .ToListAsync();

                //перевод смен в записи и сопоставление с таблицей записей
                for (int i = 0; i < allChanges.Count; i++)
                {
                    Change change = allChanges[i];

                    //генерация виртуальных записей на основе смены
                    List<Entry> emptyEntries = new List<Entry>();
                    foreach (DateTime time in change.GetTimes()) emptyEntries
                            .Add(new Entry { CreateDateTime = DateTime.Now, TargetDateTime = time, DoctorDestination = change.Staff });

                    //поиск существующих записей на основе смены
                    List<Entry> entries = await db.Entries.AsQueryable()
                        .Where(e => e.DoctorDestination == change.Staff)
                        .Where(e => e.TargetDateTime.Date == change.DateTimeStart.Date)
                        .ToListAsync();

                    //объединение записей
                    emptyEntries.AddRange(entries);

                    //группировка с заменой совпадений
                    var _result = emptyEntries
                        .OrderBy(e => e.TargetDateTime)
                        .GroupBy(e => e.TargetDateTime)
                        .Select(e => e.Last())
                        .Where(e => (filter.IsFree) ? e.EntryStatus == EntryStatus.Open : true ) 
                        .GroupBy(r => r.DoctorDestination)
                        .Select(r => r.FirstOrDefault());

                    //если записи найдены, то все последующие смены этого доктора удаляются из очереди
                    if (filter.IsGroup && _result.Count() != 0)
                    {
                        allChanges.RemoveAll(c => c.Staff == change.Staff);
                        i--;
                    }

                    //добавление записей в общий список
                    result.AddRange(_result);
                }
                return result;
            }
        }

        public async Task<IEnumerable<Patient>> FindPatient(string _string)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                string[] words = Regex.Replace(_string, @"\s+", " ").Split(' ');

                return await db.Patients.Include(p => p.Belay)
                    .AsAsyncEnumerable()
                    .Where(p =>
                       ((
                           (words.Any(word => p.FirstName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                           (words.Any(word => p.MidName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                           (words.Any(word => p.LastName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                           (words.Any(word => (p.PhoneNumber != 0) && (p.PhoneNumber.ToString().Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0)
                           >= words.Count())
                      ))
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Entry>> GetEntries(Staff selectedStaff, DateTime date)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {

                List<Entry> entries = await db.Entries
                    .AsQueryable()
                    .Where(e => e.DoctorDestination == selectedStaff)
                    .Where(e => e.TargetDateTime.Date == date.Date)
                    .Include(e => e.Patient)
                    .Include(e => e.Registrator)
                    .Include(e => e.MedCard)
                    .ToListAsync();
                List<Entry> result = new List<Entry>();

                foreach (Change change in db.Changes
                    .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Where(c => c.Staff == selectedStaff)
                    .Where(c => c.DateTimeStart.Date == date.Date)
                    .ToList())
                    foreach (DateTime time in change.GetTimes()) result.Add(new Entry
                    {
                        CreateDateTime = DateTime.Now,
                        TargetDateTime = time,
                        DoctorDestination = change.Staff,
                        Registrator = change.Staff, //заглушка
                    });

                result.AddRange(entries);
                return result.OrderBy(e => e.TargetDateTime).GroupBy(e => e.TargetDateTime).Select(e => e.Last());
            }
        }

    }
}
