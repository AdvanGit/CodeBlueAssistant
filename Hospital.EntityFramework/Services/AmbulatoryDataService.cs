using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class AmbulatoryDataService
    {
        private readonly HospitalDbContextFactory _contextFactory;

        public AmbulatoryDataService(HospitalDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Entry> GetEntryById(int entryId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                Entry result = await db.Entries
                    .AsQueryable()
                    .Include(e => e.Patient)
                    .Include(e => e.DoctorDestination).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.Registrator).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.MedCard).ThenInclude(m => m.Diagnosis).ThenInclude(d => d.DiagnosisGroup).ThenInclude(d => d.DiagnosisClass)
                    .Include(e => e.MedCard).ThenInclude(m => m.DiagnosisDoctor)
                    .Include(e => e.EntryOut)
                    .FirstOrDefaultAsync(e => e.Id == entryId);
                return result;
            }
        }

        public async Task<IList<object>> UpdateData(IList<object> entities)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                db.UpdateRangeWithoutTracking(entities);
                await db.SaveChangesAsync();
                return entities;
            }
        }
    }
}