using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.EntityFramework.Services
{
    public class TherapyDataService : ITherapyDataService
    {
        private readonly IDbContextFactory<HospitalDbContext> _contextFactory;

        public TherapyDataService(IDbContextFactory<HospitalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnoses(string searchValue, bool isCode = false)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Diagnosis> result = await db.Diagnoses
                    .AsQueryable()
                    .Where(d => isCode ? d.Code.ToLower().Contains(searchValue.ToLower()) : d.Title.ToLower().Contains(searchValue.ToLower()))
                    .Include(d => d.DiagnosisGroup).ThenInclude(d => d.DiagnosisClass)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Diagnosis>> GetDiagnoses(DiagnosisGroup diagnosisGroup)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Diagnosis> result = await db.Diagnoses
                    .AsQueryable()
                    .Where(d => d.DiagnosisGroup == diagnosisGroup)
                    .Include(d => d.DiagnosisGroup).ThenInclude(d => d.DiagnosisClass)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<DiagnosisGroup>> GetDiagnosisGroups(DiagnosisClass diagnosisClass)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<DiagnosisGroup> result = await db.DiagnosisGroups
                    .AsQueryable()
                    .Where(d => d.DiagnosisClass == diagnosisClass)
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
                    .Include(p => p.Diagnosis)
                    .Include(p => p.DiagnosisDoctor)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<DrugSubClass>> GetDrugSubClasses(DrugClass drugClass)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<DrugSubClass> result = await db.DrugSubClasses
                    .AsQueryable()
                    .Where(d => d.DrugClass == drugClass)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<DrugGroup>> GetDrugGroup(DrugSubClass drugSubClass)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<DrugGroup> result = await db.DrugGroups
                    .AsQueryable()
                    .Where(d => d.DrugSubClass == drugSubClass)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Drug>> GetDrugs(DrugGroup drugGroup)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Drug> result = await db.Drugs
                    .AsQueryable()
                    .Where(d => d.DrugSubGroup.DrugGroup == drugGroup)
                    .Include(d => d.DrugSubGroup).ThenInclude(d => d.DrugGroup).ThenInclude(d => d.DrugSubClass).ThenInclude(d => d.DrugClass)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<Drug>> GetDrugs(string substance)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<Drug> result = await db.Drugs
                    .AsQueryable()
                    .Where(d => d.Substance.ToLower().Contains(substance.ToLower()))
                    .Include(d => d.DrugSubGroup).ThenInclude(d => d.DrugGroup).ThenInclude(d => d.DrugSubClass).ThenInclude(d => d.DrugClass)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<PhysioTherapyData>> GetPhysioTherapyDatas(int medCardId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<PhysioTherapyData> result = await db.PhysioTherapyDatas
                    .AsQueryable().AsNoTracking()
                    .Where(p => p.MedCard.Id == medCardId)
                    .Include(p => p.PhysioTherapyFactor).ThenInclude(p => p.PhysTherFactGroup)
                    .Include(p => p.OperationDoctor)
                    .Include(p => p.TherapyDoctor)
                    .Include(p => p.DiagnosisDoctor)
                    .Include(p => p.Diagnosis)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<PhysioTherapyFactor>> GetPhysioFactors(PhysTherFactGroup physTherFactGroup)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<PhysioTherapyFactor> result = await db.PhysioTherapyFactors
                    .AsQueryable().AsNoTracking()
                    .Where(p => p.PhysTherFactGroup == physTherFactGroup)
                    .Include(p => p.PhysTherFactGroup)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<IEnumerable<SurgeryTherapyData>> GetSurgeryTherapyDatas(int medCardId)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<SurgeryTherapyData> result = await db.SurgeryTherapyDatas
                    .AsQueryable().AsNoTracking()
                    .Where(s => s.MedCard.Id == medCardId)
                    .Include(s => s.SurgeryOperation).ThenInclude(s => s.SurgeryGroup)
                    .Include(p => p.TherapyDoctor)
                    .Include(p => p.DiagnosisDoctor)
                    .Include(p => p.Diagnosis)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<SurgeryGroup>> GetSurgeryGroups(SurgeryType surgeryType)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<SurgeryGroup> result = await db.SurgeryGroups
                    .AsQueryable().AsNoTracking()
                    .Where(s => s.SurgeryType == surgeryType)
                    .ToListAsync();
                return result;
            }
        }
        public async Task<IEnumerable<SurgeryOperation>> GetSurgeryOperations(SurgeryGroup surgeryGroup)
        {
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                IList<SurgeryOperation> result = await db.SurgeryOperations
                    .AsQueryable().AsNoTracking()
                    .Where(s => s.SurgeryGroup == surgeryGroup)
                    .Include(s => s.SurgeryGroup)
                    .ToListAsync();
                return result;
            }
        }

    }
}
