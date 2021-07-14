using Hospital.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Domain.Services
{
    public interface ITherapyDataService
    {
        Task<IEnumerable<PharmacoTherapyData>> GetPharmacoTherapyDatas(int medCardId);
        Task<IEnumerable<PhysioTherapyData>> GetPhysioTherapyDatas(int medCardId);
        Task<IEnumerable<SurgeryTherapyData>> GetSurgeryTherapyDatas(int medCardId);

        Task<IEnumerable<Drug>> GetDrugs(string substance);
        Task<IEnumerable<Drug>> GetDrugs(DrugGroup drugGroup);
        Task<IEnumerable<DrugSubClass>> GetDrugSubClasses(DrugClass drugClass);
        Task<IEnumerable<DrugGroup>> GetDrugGroup(DrugSubClass drugSubClass);

        Task<IEnumerable<DiagnosisGroup>> GetDiagnosisGroups(DiagnosisClass diagnosisClass);
        Task<IEnumerable<Diagnosis>> GetDiagnoses(string searchValue, bool isCode = false);
        Task<IEnumerable<Diagnosis>> GetDiagnoses(DiagnosisGroup diagnosisGroup);

        Task<IEnumerable<PhysioTherapyFactor>> GetPhysioFactors(PhysTherFactGroup physTherFactGroup);

        Task<IEnumerable<SurgeryGroup>> GetSurgeryGroups(SurgeryType surgeryType);
        Task<IEnumerable<SurgeryOperation>> GetSurgeryOperations(SurgeryGroup surgeryGroup);
    }
}
