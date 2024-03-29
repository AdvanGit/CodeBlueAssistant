﻿using Hospital.Domain.Filters;
using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class EntryDataService : GenericRepository<Entry>
    {
        private readonly IDbContextFactory<HospitalDbContext> _contextFactory;

        public EntryDataService(IDbContextFactory<HospitalDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Entry>> FindDoctor(string message, EntrySearchFilter filter)
        {
            List<Entry> result = new List<Entry>();
            string _string = Regex.Replace(message.Trim(), @"\s+", " ");
            string[] words = _string.Split(' ');
            if (_string.Length > 2 || words[0] == "*")
            {
                if (words[0] == "*") words[0] = "";
                using (HospitalDbContext db = _contextFactory.CreateDbContext())
                {
                    //Поиск смен по фильтру и строке
                    List<Change> allChanges = await db.Changes
                        .AsQueryable()
                        .OrderBy(c => c.DateTimeStart)
                        .Where(c => c.Staff.Department.Type == filter.DepartmentType)
                        .Where(c => (filter.IsDate ? (c.DateTimeStart.Date == filter.DateTime.Date) : (c.DateTimeStart.Date < (DateTime.Now.AddDays(30)))))
                        .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                        //Далее фильтрация происходит на клиенте, EF не дает добро на обработку сложных запросов сервером(в асинхронном режиме)
                        //StringComparison только на клиенте, если рефакторить на сервер, то через ToLower
                        .AsAsyncEnumerable()
                        .Where(c => (
                                ((filter.IsName) ? ((words.Any(word => c.Staff.FirstName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                                                    (words.Any(word => c.Staff.MidName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                                                    (words.Any(word => c.Staff.LastName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0)) : 0) +
                                ((filter.IsQualification) ? (words.Any(word => (c.Staff.Qualification != null) && (c.Staff.Qualification.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) : 0) +
                                ((filter.IsDepartment) ? (words.Any(word => (c.Staff.Department.Title.Title != null) && (c.Staff.Department.Title.Title.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) : 0) +
                                ((filter.IsAdress) ? (words.Any(word => (c.Staff.Department.Adress.Street != null) && (c.Staff.Department.Adress.Street.Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0) : 0)
                                >= words.Count()))
                        //.Take(50) //подобрать оптимальное значение
                        .ToListAsync();


                    for (int i = 0; i < allChanges.Count; i++)
                    {
                        Change change = allChanges[i];

                        //генерация виртуальных записей на текущую смену
                        List<Entry> emptyEntries = new List<Entry>();
                        foreach (DateTime time in change.GetTimes()) emptyEntries
                                .Add(new Entry
                                {
                                    EntryStatus = Enum.Parse<EntryStatus>("0"),
                                    CreateDateTime = DateTime.Now,
                                    TargetDateTime = time,
                                    DoctorDestination = change.Staff
                                });

                        //поиск уже существующих записей на текущую смену !Уходит много времени
                        List<Entry> existEntries = await db.Entries
                            .AsQueryable()
                            .Where(e => e.DoctorDestination.Id == change.Staff.Id)
                            .Where(e => e.TargetDateTime.Date == change.DateTimeStart.Date)
                            .Include(e => e.DoctorDestination).ThenInclude(d => d.Department).ThenInclude(d => d.Title)
                            .ToListAsync();

                        //объединение записей
                        emptyEntries.AddRange(existEntries);

                        //группировка с заменой совпадений
                        var _result = emptyEntries
                            //.OrderBy(e => e.TargetDateTime)
                            .GroupBy(e => e.TargetDateTime)
                            .Select(e => e.Last())
                            .Where(e => (filter.IsFree) ? e.EntryStatus == EntryStatus.Открыта : true)
                            .GroupBy(r => r.DoctorDestination.Id)
                            .Select(r => r.First());

                        //если свободные записи найдены, то все последующие смены этого доктора удаляются из очереди
                        if (filter.IsNearest && _result.Count() != 0)
                        {
                            allChanges.RemoveAll(c => c.Staff.Id == change.Staff.Id && c.DateTimeStart >= change.DateTimeStart);
                            i--;
                        }

                        result.AddRange(_result);
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<Entry>> FindDoctor(int departmentTitleId, EntrySearchFilter filter)
        {
            List<Entry> result = new List<Entry>();
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<Change> allChanges = await db.Changes
                    .AsQueryable()
                    .OrderBy(c => c.DateTimeStart)
                    .Where(c => c.Staff.Department.Title.Id == departmentTitleId && c.Staff.Department.Type == filter.DepartmentType)
                    .Where(c => filter.IsDate ? (c.DateTimeStart.Date == filter.DateTime.Date) : (c.DateTimeStart.Date < DateTime.Now.AddDays(30)))
                    .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    //.Take(50) //подобрать оптимальное значение
                    .ToListAsync();

                for (int i = 0; i < allChanges.Count; i++)
                {
                    Change change = allChanges[i];

                    //генерация виртуальных записей на текущую смену
                    List<Entry> emptyEntries = new List<Entry>();
                    foreach (DateTime time in change.GetTimes()) emptyEntries
                            .Add(new Entry
                            {
                                EntryStatus = Enum.Parse<EntryStatus>("0"),
                                CreateDateTime = DateTime.Now,
                                TargetDateTime = time,
                                DoctorDestination = change.Staff
                            });

                    //поиск уже существующих записей на текущую смену !Уходит много времени
                    List<Entry> existEntries = await db.Entries
                        .AsQueryable()
                        .Where(e => e.DoctorDestination.Id == change.Staff.Id)
                        .Where(e => e.TargetDateTime.Date == change.DateTimeStart.Date)
                        .Include(e => e.DoctorDestination).ThenInclude(d => d.Department).ThenInclude(d => d.Title)
                        .ToListAsync();

                    //объединение записей
                    emptyEntries.AddRange(existEntries);

                    //группировка с заменой совпадений
                    var _result = emptyEntries
                        //.OrderBy(e => e.TargetDateTime)
                        .GroupBy(e => e.TargetDateTime)
                        .Select(e => e.Last())
                        .Where(e => filter.IsFree ? e.EntryStatus == EntryStatus.Открыта : true)
                        .GroupBy(r => r.DoctorDestination.Id)
                        .Select(r => r.First());

                    //если свободные записи найдены, то все последующие смены этого доктора удаляются из очереди
                    if (filter.IsNearest && _result.Count() != 0)
                    {
                        allChanges.RemoveAll(c => c.Staff.Id == change.Staff.Id && c.DateTimeStart >= change.DateTimeStart);
                        i--;
                    }

                    result.AddRange(_result);
                }
            }
            return result;
        }

        public async Task<IEnumerable<Patient>> FindPatient(string message)
        {
            List<Patient> result = new List<Patient>();
            string _string = Regex.Replace(message.Trim(), @"\s+", " ");
            string[] words = _string.Split(' ');
            if (_string.Length > 2 || words[0] == "*")
            {
                if (words[0] == "*") words[0] = "";
                using (HospitalDbContext db = _contextFactory.CreateDbContext())
                {
                    result = await db.Patients.Include(p => p.Belay)
                        .AsAsyncEnumerable()
                        .Where(p =>
                           ((
                               (words.Any(word => p.FirstName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                               (words.Any(word => p.MidName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                               (words.Any(word => p.LastName.Contains(word, StringComparison.CurrentCultureIgnoreCase)) ? 1 : 0) +
                               (words.Any(word => (p.PhoneNumber != 0) && (p.PhoneNumber.ToString().Contains(word, StringComparison.CurrentCultureIgnoreCase))) ? 1 : 0)
                               >= words.Count())
                          ))
                        .Take(100)
                        .ToListAsync();
                }
            }
            return result;
        }

        public async Task<IEnumerable<Entry>> GetEntries(int staffId, DateTime date, bool isFreeOnly = false)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Entry> entries = await db.Entries
                    .AsQueryable()
                    .Where(e => e.DoctorDestination.Id == staffId)
                    .Where(e => e.TargetDateTime.Date == date.Date)
                    .Include(e => e.Patient)
                    .Include(e => e.DoctorDestination).ThenInclude(d => d.Department).ThenInclude(d => d.Title)
                    .Include(e => e.Registrator)
                    .Include(e => e.MedCard)
                    .ToListAsync();

                IList<Change> changes = await db.Changes
                    .AsQueryable()
                    .Where(c => c.Staff.Id == staffId)
                    .Where(c => c.DateTimeStart.Date == date.Date)
                    .Include(c => c.Staff).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .ToListAsync();

                List<Entry> result = new List<Entry>();

                foreach (Change change in changes)
                    foreach (DateTime time in change.GetTimes())
                        result.Add(new Entry
                        {
                            EntryStatus = Enum.Parse<EntryStatus>("0"),
                            CreateDateTime = DateTime.Now,
                            TargetDateTime = time,
                            DoctorDestination = change.Staff,
                            Registrator = change.Staff, //заглушка
                        });

                result.AddRange(entries);

                return result.OrderBy(e => e.TargetDateTime).GroupBy(e => e.TargetDateTime).Select(e => e.Last());
            }
        }

        public async Task<IEnumerable<Entry>> GetEntries(int patientId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                //Решить, давать ли возможность отменять записи, созданные операторами? Если нет, то добавить
                //условие, при котором предоставляются только собственноручно созданные записи
                var entries = await db.Entries
                    .AsQueryable()
                    .Where(e => e.Patient.Id == patientId)
                    .Include(e => e.DoctorDestination).ThenInclude(d => d.Department).ThenInclude(d => d.Title)
                    .OrderBy(e => e.TargetDateTime)
                    .ToListAsync();
                return entries;
            }
        }

        public async Task<Entry> UpdateEntry(Entry entry)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                db.UpdateWithoutTracking(entry);
                await db.SaveChangesAsync();
                return entry;
            }
        }
    }
}