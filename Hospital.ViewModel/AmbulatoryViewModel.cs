using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AmbulatoryViewModel : MainViewModel
    {

        private AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        private Test _selectedTest;
        public Test SelectedTest { get => _selectedTest; set { _selectedTest = value; OnPropertyChanged(nameof(SelectedTest)); } }

        //public ObservableCollection<TestData> TestData { get;} = new ObservableCollection<TestData>();

        public ObservableCollection<TestData> PhysicalDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> ToolDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> LabDiagData { get; } = new ObservableCollection<TestData>();

        public ObservableCollection<Test> PhysicalTestList { get; } = new ObservableCollection<Test>();

        public AmbulatoryViewModel()
        {
            GetData(3);
            GetTestList(TestMethod.Физикальная);
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
                    //TestData.Clear();
                    PhysicalDiagData.Clear();
                    LabDiagData.Clear();
                    ToolDiagData.Clear();

                    foreach (TestData test in res)
                    {
                        //TestData.Add(test);
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

        private async void GetTestList(TestMethod testMethod)
        {
            {
                List<Test> result = await ambulatoryDataService.GetTestList(testMethod);
                foreach (Test item in result) PhysicalTestList.Add(item);
            }
        }

        public TestData CreatePhysDiag(Test test, string value = null, string option = null)
        {
            return new TestData 
            { 
                Test = test,
                Value = value,
                Option = option,
                DateCreate = DateTime.Now,
                DateResult = DateTime.Now,
                MedCard = CurrentEntry.MedCard,
                StaffResult = CurrentEntry.DoctorDestination
            };
        }
    }
}
