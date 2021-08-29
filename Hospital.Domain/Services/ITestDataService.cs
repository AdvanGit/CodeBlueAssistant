using Hospital.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Domain.Services
{
    public interface ITestDataService
    {
        Task<IEnumerable<TestData>> GetTestData(int medCardId, TestMethod method);
        Task<IEnumerable<TestData>> GetTestData(int patientId);


        Task<IEnumerable<TestType>> GetTestTypeList(TestMethod testMethod);

        Task<IEnumerable<Test>> GetTestList(TestType testType);
        Task<IEnumerable<Test>> GetTestList(IEnumerable<int> testIds);

        Task<IEnumerable<TestTemplate>> GetTemplateList(TestType testType);
    }
}
