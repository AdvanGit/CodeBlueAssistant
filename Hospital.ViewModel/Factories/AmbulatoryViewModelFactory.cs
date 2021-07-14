using Hospital.Domain.Services;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Ambulatory;

namespace Hospital.ViewModel.Factories
{
    public class AmbulatoryViewModelFactory
    {
        private readonly AmbulatoryDataService _ambulatoryDataService;
        private readonly EntryDataService _entryDataServices;
        private readonly ITestDataService _testDataService;
        private readonly ITherapyDataService _therapyDataService;

        public AmbulatoryViewModelFactory(
            ITestDataService testDataService,
            ITherapyDataService therapyDataService,
            AmbulatoryDataService ambulatoryDataService,
            EntryDataService entryDataServices)
        {
            _testDataService = testDataService;
            _therapyDataService = therapyDataService;
            _ambulatoryDataService = ambulatoryDataService;
            _entryDataServices = entryDataServices;
        }

        public AmbulatoryViewModel CreateViewModel(int entryId)
        {
            return new AmbulatoryViewModel(entryId, _ambulatoryDataService, _entryDataServices , _testDataService, _therapyDataService);
        }
    }
}
