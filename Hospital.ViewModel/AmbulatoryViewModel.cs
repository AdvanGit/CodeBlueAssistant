using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AmbulatoryViewModel : MainViewModel
    {

        private AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        public ObservableCollection<TestData> TestData { get; set; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> PhysicalDiagData { get; set; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> ToolDiagData { get; set; } = new ObservableCollection<TestData>();

        public ObservableCollection<TestData> PhysicalDiagList { get ; set; } = new ObservableCollection<TestData>();

        public ObservableCollection<TestData> LabDiagData { get; set; } = new ObservableCollection<TestData>();

        public AmbulatoryViewModel()
        {
            GetData(3);
        }

        private async void GetData(int entryId)
        {
            Task<Entry> taskEntry = ambulatoryDataService.GetEntriesById(entryId);
            CurrentEntry = await taskEntry;
            if (taskEntry.IsCompleted)
            {
                if (CurrentEntry.MedCardId != null)
                {
                    var res = await ambulatoryDataService.GetTestData(CurrentEntry.MedCard.Id);
                    TestData.Clear();
                    PhysicalDiagData.Clear();
                    LabDiagData.Clear();
                    ToolDiagData.Clear();

                    foreach (TestData test in res)
                    {
                        TestData.Add(test);
                        switch (test.Test.TestType.TestMethod)
                        {
                            case TestMethod.Физикальная:
                                {
                                    PhysicalDiagData.Add(test);
                                    break;
                                }
                            case TestMethod.Лабараторная:
                                {
                                    LabDiagData.Add(test);
                                    break;
                                }
                            case TestMethod.Инструментальная:
                                {
                                    ToolDiagData.Add(test);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private async void GetPhysicalDiagList()
        {

        }
    }
}
