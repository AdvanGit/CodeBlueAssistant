using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    //public class TestTemplate<T> : ObservableCollection<T>
    //{
    //    public TestTemplate(string title)
    //    {
    //        _title = title;
    //    }

    //    private string _title;
    //    public string Title { get => _title; set => _title = value; }
    //}

    public class AmbulatoryViewModel : MainViewModel
    {
        public AmbulatoryViewModel()
        {
            GetEntry(3);

            //GetTestList(TestMethod.Физикальная);
            //GetTestTypeList(TestMethod.Лабараторная);
            //GetTestTypeList(TestMethod.Инструментальная);
        }

        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());

        private Entry _currentEntry;
        public Entry CurrentEntry { get => _currentEntry; set { _currentEntry = value; OnPropertyChanged(nameof(CurrentEntry)); } }

        private DiagnosticViewModel _diagnosticViewModel;
        public DiagnosticViewModel DiagnosticViewModel { get => _diagnosticViewModel; set { _diagnosticViewModel = value; OnPropertyChanged(nameof(DiagnosticViewModel)); } }

        //private TestTemplate<Test> _selectedTemplate;
        //public TestTemplate<Test> SelectedTemplate { get => _selectedTemplate; set { _selectedTemplate = value; OnPropertyChanged(nameof(SelectedTemplate)); } }

        //private Test _selectedTest;
        //public Test SelectedTest
        //{
        //    get => _selectedTest;
        //    set
        //    {
        //        _selectedTest = value;
        //        OnPropertyChanged(nameof(SelectedTest));
        //        if (value != null) TestOption = value.DefaultOption;
        //        else TestOption = null;
        //    }
        //}

        //private TestType _currentLabTestType;
        //public TestType CurrentLabTestType { get => _currentLabTestType; set { _currentLabTestType = value; OnPropertyChanged(nameof(CurrentLabTestType)); GetTestList(TestMethod.Лабараторная, CurrentLabTestType); } }
        //private TestType _currentToolTestType;
        //public TestType CurrentToolTestType { get => _currentToolTestType; set { _currentToolTestType = value; OnPropertyChanged(nameof(CurrentToolTestType)); GetTestList(TestMethod.Инструментальная, CurrentToolTestType); } }
        //private Test _selectedLabTest;
        //public Test SelectedLabTest { get => _selectedLabTest; set { _selectedLabTest = value; OnPropertyChanged(nameof(SelectedLabTest)); } }
        //private Test _selectedToolTest;
        //public Test SelectedToolTest { get => _selectedToolTest; set { _selectedToolTest = value; OnPropertyChanged(nameof(SelectedToolTest)); } }

        //private string _testOption;
        //public string TestOption { get => _testOption; set { _testOption = value; OnPropertyChanged(nameof(TestOption)); } }


        //public ObservableCollection<TestData> PhysicalDiagData { get; } = new ObservableCollection<TestData>();
        //public ObservableCollection<TestData> ToolDiagData { get; } = new ObservableCollection<TestData>();
        //public ObservableCollection<TestData> LabDiagData { get; } = new ObservableCollection<TestData>();
        //public ObservableCollection<TestTemplate<Test>> PhysicalTestTemplate { get; } = new ObservableCollection<TestTemplate<Test>>();

        //public ObservableCollection<TestType> LabTestTypes { get; } = new ObservableCollection<TestType>();
        //public ObservableCollection<TestType> ToolTestTypes { get; } = new ObservableCollection<TestType>();

        //public ObservableCollection<Test> PhysicalTestList { get; } = new ObservableCollection<Test>();
        //public ObservableCollection<Test> LabTestList { get; } = new ObservableCollection<Test>();
        //public ObservableCollection<Test> ToolTestList { get; } = new ObservableCollection<Test>();


        //public TestData CreatePhysDiag(Test test, string value = null, string option = null)
        //{
        //    return new TestData
        //    {
        //        Test = test,
        //        Value = value,
        //        Option = option,
        //        Status = TestStatus.Редакция,
        //        DateCreate = DateTime.Now,
        //        DateResult = DateTime.Now,
        //        MedCard = CurrentEntry.MedCard,
        //        StaffResult = CurrentEntry.DoctorDestination
        //    };
        //}

        private async void GetEntry(int entryId)
        {
            Task<Entry> taskEntry = ambulatoryDataService.GetEntryById(entryId);
            CurrentEntry = await taskEntry;
            if (taskEntry.IsCompleted)
            {
                DiagnosticViewModel = new DiagnosticViewModel(CurrentEntry);
                ///Заполнение данными из мед карты
                //if (CurrentEntry.MedCardId != null)
                //{
                //    var res = await ambulatoryDataService.GetTestData(CurrentEntry.MedCard.Id);
                //    PhysicalDiagData.Clear();
                //    LabDiagData.Clear();
                //    ToolDiagData.Clear();

                //    foreach (TestData test in res)
                //    {
                //        switch (test.Test.TestType.TestMethod)
                //        {
                //            case TestMethod.Физикальная:
                //                {
                //                    PhysicalDiagData.Add(test);
                //                    break;
                //                }
                //            case TestMethod.Лабараторная:
                //                {
                //                    LabDiagData.Add(test);
                //                    break;
                //                }
                //            case TestMethod.Инструментальная:
                //                {
                //                    ToolDiagData.Add(test);
                //                    break;
                //                }
                //            default:
                //                break;
                //        }
                //    }
                //}
            }
        }

        //private async void GetTestList(TestMethod testMethod, TestType testType = null)
        //{
        //    List<Test> result = await ambulatoryDataService.GetTestList(testMethod, testType);
        //    switch (testMethod)
        //    {
        //        case TestMethod.Физикальная:
        //            {
        //                PhysicalTestList.Clear();
        //                foreach (Test item in result) PhysicalTestList.Add(item);
        //                CreateTestDiagTemplate(); // need refact.
        //                break;
        //            }
        //        case TestMethod.Лабараторная:
        //            {
        //                LabTestList.Clear();
        //                foreach (Test item in result) LabTestList.Add(item);
        //                break;
        //            }
        //        case TestMethod.Инструментальная:
        //            {
        //                ToolTestList.Clear();
        //                foreach (Test item in result) ToolTestList.Add(item);
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //}
        //private async void GetTestTypeList(TestMethod testMethod)
        //{
        //    List<TestType> result = await ambulatoryDataService.GetTestTypeList(testMethod);
        //    if (testMethod == TestMethod.Лабараторная) foreach (TestType item in result) LabTestTypes.Add(item);
        //    if (testMethod == TestMethod.Инструментальная) foreach (TestType item in result) ToolTestTypes.Add(item);
        //}

        //private void CreateTestDiagTemplate()
        //{
        //    var t = new TestTemplate<Test>("Первичный осмотр");
        //    foreach (Test test in PhysicalTestList) t.Add(test);
        //    PhysicalTestTemplate.Add(t);
        //}

        //public void AddTemplate()
        //{
        //    foreach (Test test in SelectedTemplate) PhysicalDiagData.Add(new TestData
        //    {
        //        Test = test,
        //        DateCreate = DateTime.Now,
        //        DateResult = DateTime.Now,
        //        Status = TestStatus.Редакция,
        //        Option = test.DefaultOption,
        //        MedCard = CurrentEntry.MedCard,
        //        StaffResult = CurrentEntry.DoctorDestination
        //    });
        //}
        //public void DeleteRows(object testDatas)
        //{
        //    var items = ((System.Collections.IList)testDatas).Cast<TestData>().ToList();
        //    if (items.Count != 0)
        //    switch (items[0].Test.TestType.TestMethod)
        //    {
        //        case TestMethod.Физикальная:
        //            {
        //                foreach (TestData test in items)
        //                {
        //                    if (test.Status == Enum.Parse<TestStatus>("3"))
        //                        PhysicalDiagData.Remove(test);
        //                }
        //                break;
        //            }
        //        case TestMethod.Лабараторная:
        //            {
        //                foreach (TestData test in items)
        //                {
        //                    if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
        //                        LabDiagData.Remove(test);
        //                }
        //                break;
        //            }
        //        case TestMethod.Инструментальная:
        //            {
        //                foreach (TestData test in items)
        //                {
        //                    if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
        //                        ToolDiagData.Remove(test);
        //                }
        //                break;
        //            }
        //        default: break;
        //    }
        //}
    }
}
