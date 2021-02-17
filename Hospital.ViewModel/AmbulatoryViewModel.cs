using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class TestTemplate<T> : ObservableCollection<T>
    {
        public TestTemplate(string title)
        {
            _title = title;
        }

        private string _title;
        public string Title { get => _title; set => _title = value; }
    }

    public class AmbulatoryViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        private TestTemplate<Test> _selectedTemplate;
        public TestTemplate<Test> SelectedTemplate { get => _selectedTemplate; set { _selectedTemplate = value; OnPropertyChanged(nameof(SelectedTemplate)); } }

        private Test _selectedTest;
        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                OnPropertyChanged(nameof(SelectedTest));
                if (value != null) TestOption = value.DefaultOption;
                else TestOption = null;
            }
        }
        private string _testOption;
        public string TestOption { get => _testOption; set { _testOption = value; OnPropertyChanged(nameof(TestOption)); } }

        public ObservableCollection<TestData> PhysicalDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> ToolDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> LabDiagData { get; } = new ObservableCollection<TestData>();

        public ObservableCollection<Test> PhysicalTestList { get; } = new ObservableCollection<Test>();

        public ObservableCollection<TestTemplate<Test>> PhysicalTestTemplate { get; } = new ObservableCollection<TestTemplate<Test>>();

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
                    PhysicalDiagData.Clear();
                    LabDiagData.Clear();
                    ToolDiagData.Clear();

                    foreach (TestData test in res)
                    {
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
            List<Test> result = await ambulatoryDataService.GetTestList(testMethod);
            foreach (Test item in result) PhysicalTestList.Add(item);
            CreateTestDiagTemplate();
        }

        public TestData CreatePhysDiag(Test test, string value = null, string option = null)
        {
            return new TestData
            {
                Test = test,
                Value = value,
                Option = option,
                Status = TestStatus.Редакция,
                DateCreate = DateTime.Now,
                DateResult = DateTime.Now,
                MedCard = CurrentEntry.MedCard,
                StaffResult = CurrentEntry.DoctorDestination
            };
        }

        private void CreateTestDiagTemplate()
        {
            var t = new TestTemplate<Test>("Первичный осмотр");
            foreach (Test test in PhysicalTestList) t.Add(test);
            PhysicalTestTemplate.Add(t);
        }

        public void AddTemplate()
        {
            foreach (Test test in SelectedTemplate) PhysicalDiagData.Add(new TestData
            {
                Test = test,
                DateCreate = DateTime.Now,
                DateResult = DateTime.Now,
                Status = TestStatus.Редакция,
                Option = test.DefaultOption,
                MedCard = CurrentEntry.MedCard,
                StaffResult = CurrentEntry.DoctorDestination
            });
        }

        public void DeleteRows(object testDatas)
        {
            var items = ((System.Collections.IList)testDatas).Cast<TestData>().ToList();

            foreach (TestData test in items)
            {
                if (test.Status == TestStatus.Редакция)
                    PhysicalDiagData.Remove(test);
            }
        }
    }
}
