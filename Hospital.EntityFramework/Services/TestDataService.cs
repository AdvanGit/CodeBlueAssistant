using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly HospitalDbContextFactory _contextFactory;

        public TestDataService(HospitalDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TestData>> GetTestData(int medCardId, TestMethod method)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<TestData> result = await db.TestDatas
                    .AsQueryable()
                    .Where(t => t.MedCard.Id == medCardId)
                    .Where(t => t.Test.TestType.TestMethod == method)
                    .Include(t => t.Test).ThenInclude(t => t.TestType)
                    .Include(t => t.StaffResult)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Test>> GetTestList(TestType testType)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Test> result = await db.Tests
                    .AsQueryable()
                    .Where(t => t.TestType == testType)
                    .Include(t => t.TestType)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Test>> GetTestList(IEnumerable<int> ids)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<Test> result = await db.Tests
                    .AsQueryable()
                    .Where(t => ids.Contains(t.Id))
                    .Include(t => t.TestType)
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
        public async Task<IEnumerable<TestTemplate>> GetTemplateList(TestType testType)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<TestTemplate> result = await db.TestTemplates
                    .AsQueryable()
                    .Where(t => t.Category == testType)
                    .ToListAsync();
                return result;
            }
        }

    }
}
