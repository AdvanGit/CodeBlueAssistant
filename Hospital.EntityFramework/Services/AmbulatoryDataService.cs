using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
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
                    .Include(e => e.DoctorDestination).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.Patient)
                    .Include(e => e.Registrator).ThenInclude(s => s.Department).ThenInclude(d => d.Title)
                    .Include(e => e.MedCard).ThenInclude(m => m.Diagnosis)
                    .Include(e => e.MedCard).ThenInclude(m => m.DiagnosisDoctor)
                    .AsQueryable()
                    .Where(e => e.Id == entryId)
                    .FirstOrDefaultAsync();
                return result;
            }
        }

        public async Task<IEnumerable<TestData>> GetTestData(int medCardId, bool onlySymptom = false)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<TestData> result = await db.TestDatas
                    .Include(t => t.Test).ThenInclude(t => t.TestType)
                    .Include(t => t.StaffResult)
                    .AsQueryable()
                    .Where(t => t.MedCard.Id == medCardId)
                    .Where(t => (onlySymptom == true) ? (t.IsSymptom == true) : true)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Test>> GetTestList(TestMethod testMethod, TestType testType = null)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Test> result = await db.Tests
                    .Include(t => t.TestType)
                    .AsQueryable()
                    .Where(t => t.TestType.TestMethod == testMethod)
                    .Where(t => (testType != null) ? t.TestType == testType : true)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Test>> GetTestList(ICollection<int> ids)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<Test> result = await db.Tests
                    .Include(t => t.TestType)
                    .AsQueryable()
                    .Where(t => ids.Contains(t.Id))
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<TestType>> GetTestTypeList(TestMethod testMethod)
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

        public async Task<IEnumerable<PharmacoTherapyData>> GetPharmacoTherapyDatas(int medCardId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<PharmacoTherapyData> result = await db.PharmacoTherapyDatas
                    .AsQueryable()
                    .Where(p => p.MedCard.Id == medCardId)
                    .Include(p => p.Drug).ThenInclude(d => d.DrugSubGroup).ThenInclude(d => d.DrugGroup).ThenInclude(d => d.DrugSubClass).ThenInclude(d => d.DrugClass)
                    .Include(p => p.TherapyDoctor)
                    .Include(p=> p.Diagnosis)
                    .Include(p=>p.DiagnosisDoctor)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Drug>> GetDrugs(DrugGroup drugGroup)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Drug> result = await db.Drugs
                    .Include(d => d.DrugSubGroup).ThenInclude(d => d.DrugGroup).ThenInclude(d => d.DrugSubClass).ThenInclude(d => d.DrugClass)
                    .AsQueryable()
                    .Where(d => d.DrugSubGroup.DrugGroup == drugGroup)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Drug>> GetDrugs(string substance)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Drug> result = await db.Drugs
                    .Include(d => d.DrugSubGroup).ThenInclude(d => d.DrugGroup).ThenInclude(d => d.DrugSubClass).ThenInclude(d => d.DrugClass)
                    .AsQueryable()
                    .Where(d => d.Substance.Contains(substance))
                    .ToListAsync();
                return result;
            }
        }

        public async Task<bool> SaveRangeTestData(IEnumerable<TestData> datas)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                db.TestDatas.AddRange(datas);
                await db.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateTestData(IEnumerable<TestData> datas)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                db.TestDatas.UpdateRange(datas);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
