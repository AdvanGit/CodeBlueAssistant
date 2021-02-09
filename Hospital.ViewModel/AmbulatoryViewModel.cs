using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{

    public class AmbulatoryViewModel : MainViewModel
    {
        private AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        public ObservableCollection<TestData> ToolTestData { get; set; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> PhysicalTestData { get; set; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> LabTestData { get; set; } = new ObservableCollection<TestData>();

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
                    PhysicalTestData.Clear();
                    LabTestData.Clear();
                    ToolTestData.Clear();

                    foreach (TestData test in res)
                    {
                        switch (test.Test.TestType.TestMethod)
                        {
                            case TestMethod.Физикальная:
                                {
                                    PhysicalTestData.Add(test);
                                    break;
                                }
                            case TestMethod.Лабараторная:
                                {
                                    LabTestData.Add(test);
                                    break;
                                }
                            case TestMethod.Инструментальная:
                                {
                                    ToolTestData.Add(test);
                                    break;
                                }
                            default: break;
                        }

                    }
                }





            }


        }

    }
}
