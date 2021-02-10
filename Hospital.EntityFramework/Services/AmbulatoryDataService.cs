using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Entry> GetEntriesById(int entryId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                Entry result = await db.Entries
                    .Include(e => e.DoctorDestination).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.Patient)
                    .Include(e => e.Registrator)
                    .Include(e => e.MedCard).ThenInclude(m => m.Diagnosis)
                    .AsQueryable()
                    .Where(e => e.Id == entryId)
                    .FirstOrDefaultAsync();
                return result;
            }
        }

        public async Task<List<TestData>> GetTestData(int medCardId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<TestData> result = await db.TestDatas
                    .Include(t=>t.Test).ThenInclude(t=>t.TestType)
                    .Include(t=>t.StaffResult)
                    .AsQueryable()
                    .Where(t => t.MedCard.Id == medCardId)
                    .ToListAsync();
                return result;
            }
        }

    }
}
