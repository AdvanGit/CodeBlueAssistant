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

        public async Task<Entry> GetEntryById(int entryId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                Entry result = await db.Entries
                    .Include(e => e.DoctorDestination).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.Patient)
                    .Include(e => e.Registrator).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
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

        public async Task<List<Test>> GetTestList(TestMethod testMethod, TestType testType = null)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<Test> result = await db.Tests
                    .Include(t => t.TestType)
                    .AsQueryable()
                    .Where(t => t.TestType.TestMethod == testMethod)
                    .Where(t=> (testType != null) ?  t.TestType == testType : true)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<List<TestType>> GetTestTypeList(TestMethod testMethod)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<TestType> result = await db.TestTypes
                    .AsQueryable()
                    .Where(t => t.TestMethod == testMethod)
                    .ToListAsync();
                return result;
            }
        }

    }
}
