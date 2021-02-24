using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hospital.ViewModel.Ambulatory
{
    public class DiagnosticViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());
        private readonly Entry currentEntry;

        public DiagnosticViewModel(Entry entry)
        {
            currentEntry = entry;
            Initialize(entry);

            GetTestList(TestMethod.Физикальная);
            GetTestTypeList(TestMethod.Лабараторная);
            GetTestTypeList(TestMethod.Инструментальная);
        }

        private TestType _currentLabTestType;
        public TestType CurrentLabTestType
        {
            get => _currentLabTestType;
            set
            {
                _currentLabTestType = value;
                OnPropertyChanged(nameof(CurrentLabTestType));
                GetTestList(TestMethod.Лабараторная, CurrentLabTestType);
            }
        }
        private TestType _currentToolTestType;
        public TestType CurrentToolTestType
        {
            get => _currentToolTestType;
            set
            {
                _currentToolTestType = value;
                OnPropertyChanged(nameof(CurrentToolTestType));
                GetTestList(TestMethod.Инструментальная, CurrentToolTestType);
            }
        }

        private Test _selectedPhysicalTest;
        public Test SelectedPhysicalTest
        {
            get => _selectedPhysicalTest;
            set
            {
                _selectedPhysicalTest = value;
                OnPropertyChanged(nameof(SelectedPhysicalTest));
                if (value != null) PhysicalDataOption = value.DefaultOption;
                else PhysicalDataOption = null;
            }
        }
        private Test _selectedToolTest;
        public Test SelectedToolTest
        {
            get => _selectedToolTest;
            set
            {
                _selectedToolTest = value;
                OnPropertyChanged(nameof(SelectedToolTest));
                if (value != null) ToolDataOption = value.DefaultOption;
                else ToolDataOption = null;
            }
        }
        private Test _selectedLabTest;
        public Test SelectedLabTest { get => _selectedLabTest; set { _selectedLabTest = value; OnPropertyChanged(nameof(SelectedLabTest)); } }

        private string _physicalDataOption;
        public string PhysicalDataOption { get => _physicalDataOption; set { _physicalDataOption = value; OnPropertyChanged(nameof(PhysicalDataOption)); } }
        private string _toolDataOption;
        public string ToolDataOption { get => _toolDataOption; set { _toolDataOption = value; OnPropertyChanged(nameof(ToolDataOption)); } }


        public ObservableCollection<TestData> PhysicalDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> ToolDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> LabDiagData { get; } = new ObservableCollection<TestData>();

        public ObservableCollection<TestType> LabTestTypes { get; } = new ObservableCollection<TestType>();
        public ObservableCollection<TestType> ToolTestTypes { get; } = new ObservableCollection<TestType>();

        public ObservableCollection<Test> PhysicalTestList { get; } = new ObservableCollection<Test>();
        public ObservableCollection<Test> LabTestList { get; } = new ObservableCollection<Test>();
        public ObservableCollection<Test> ToolTestList { get; } = new ObservableCollection<Test>();

        private async void Initialize(Entry entry)
        {
            if (entry != null && entry.MedCardId != null)
            {
                var res = await ambulatoryDataService.GetTestData(entry.MedCard.Id);
                PhysicalDiagData.Clear();
                LabDiagData.Clear();
                ToolDiagData.Clear();

                foreach (TestData data in res)
                {
                    switch (data.Test.TestType.TestMethod)
                    {
                        case TestMethod.Физикальная:
                            PhysicalDiagData.Add(data);
                            break;
                        case TestMethod.Лабараторная:
                            LabDiagData.Add(data);
                            break;
                        case TestMethod.Инструментальная:
                            ToolDiagData.Add(data);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private async void GetTestTypeList(TestMethod testMethod)
        {
            List<TestType> result = await ambulatoryDataService.GetTestTypeList(testMethod);
            if (testMethod == TestMethod.Лабараторная) foreach (TestType item in result) LabTestTypes.Add(item);
            else if (testMethod == TestMethod.Инструментальная) foreach (TestType item in result) ToolTestTypes.Add(item);
        }
        private async void GetTestList(TestMethod testMethod, TestType testType = null)
        {
            List<Test> result = await ambulatoryDataService.GetTestList(testMethod, testType);
            switch (testMethod)
            {
                case TestMethod.Физикальная:
                    {
                        PhysicalTestList.Clear();
                        foreach (Test item in result) PhysicalTestList.Add(item);
                        break;
                    }
                case TestMethod.Лабараторная:
                    {
                        LabTestList.Clear();
                        foreach (Test item in result) LabTestList.Add(item);
                        break;
                    }
                case TestMethod.Инструментальная:
                    {
                        ToolTestList.Clear();
                        foreach (Test item in result) ToolTestList.Add(item);
                        break;
                    }
                default:
                    break;
            }
        }

        public TestData CreateData(Test test, string value = null, string option = null)
        {
            return new TestData
            {
                Test = test,
                Value = value,
                Option = option,
                Status = TestStatus.Редакция,
                DateCreate = DateTime.Now,
                MedCard = currentEntry.MedCard,
            };
        }

        public void AddData(object obj, TestMethod testMethod)
        {
            switch (testMethod)
            {
                case TestMethod.Физикальная:
                    TestData data = CreateData(SelectedPhysicalTest, obj.ToString(), PhysicalDataOption);
                    data.DateResult = DateTime.Now;
                    data.StaffResult = currentEntry.DoctorDestination;
                    PhysicalDiagData.Add(data);
                    break;
                case TestMethod.Лабараторная:
                    LabDiagData.Add(CreateData(SelectedLabTest, null, SelectedLabTest.DefaultOption));
                    break;
                case TestMethod.Инструментальная:
                    ToolDiagData.Add(CreateData(SelectedToolTest, null, SelectedToolTest.DefaultOption));
                    break;
                default:
                    break;
            }
        }
        public void RemoveData(object testDatas)
        {
            var items = ((System.Collections.IList)testDatas).Cast<TestData>().ToList();
            if (items.Count != 0)
                switch (items[0].Test.TestType.TestMethod)
                {
                    case TestMethod.Физикальная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == Enum.Parse<TestStatus>("3"))
                                PhysicalDiagData.Remove(test);
                        }
                        break;
                    case TestMethod.Лабараторная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
                                LabDiagData.Remove(test);
                        }
                        break;
                    case TestMethod.Инструментальная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
                                ToolDiagData.Remove(test);
                        }
                        break;
                    default: break;
                }
        }


    }
}
