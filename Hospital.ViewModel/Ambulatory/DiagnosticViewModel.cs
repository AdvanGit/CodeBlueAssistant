using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class DiagnosticViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());
        private readonly GenericDataServices<TestTemplate> genericTemplateServices = new GenericDataServices<TestTemplate>(new HospitalDbContextFactory());

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
        private TestType _currentToolTestType;
        public TestType CurrentLabTestType
        {
            get => _currentLabTestType;
            set
            {
                _currentLabTestType = value;
                OnPropertyChanged(nameof(CurrentLabTestType));
                GetTestList(TestMethod.Лабараторная, CurrentLabTestType);
                GetTemplateList(value);
            }
        }
        public TestType CurrentToolTestType
        {
            get => _currentToolTestType;
            set
            {
                _currentToolTestType = value;
                OnPropertyChanged(nameof(CurrentToolTestType));
                GetTestList(TestMethod.Инструментальная, CurrentToolTestType);
                GetTemplateList(value);
            }
        }

        private TestTemplate _currentPhysicalTemplate;
        private TestTemplate _currentLabTemplate;
        private TestTemplate _currentToolTemplate;
        public TestTemplate CurrentPhysicalTemplate { get => _currentPhysicalTemplate; set { _currentPhysicalTemplate = value; OnPropertyChanged(nameof(CurrentPhysicalTemplate)); } }
        public TestTemplate CurrentLabTemplate { get => _currentLabTemplate; set { _currentLabTemplate = value; OnPropertyChanged(nameof(CurrentLabTemplate)); } }
        public TestTemplate CurrentToolTemplate { get => _currentToolTemplate; set { _currentToolTemplate = value; OnPropertyChanged(nameof(CurrentToolTemplate)); } }

        private Test _selectedPhysicalTest;
        private Test _selectedLabTest;
        private Test _selectedToolTest;
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
        public Test SelectedLabTest { get => _selectedLabTest; set { _selectedLabTest = value; OnPropertyChanged(nameof(SelectedLabTest)); } }
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

        private string _physicalDataOption;
        private string _toolDataOption;
        public string PhysicalDataOption { get => _physicalDataOption; set { _physicalDataOption = value; OnPropertyChanged(nameof(PhysicalDataOption)); } }
        public string ToolDataOption { get => _toolDataOption; set { _toolDataOption = value; OnPropertyChanged(nameof(ToolDataOption)); } }

        public ObservableCollection<TestTemplate> PhysicalTemplates { get; } = new ObservableCollection<TestTemplate>();
        public ObservableCollection<TestTemplate> LabTemplates { get; } = new ObservableCollection<TestTemplate>();
        public ObservableCollection<TestTemplate> ToolTemplates { get; } = new ObservableCollection<TestTemplate>();

        public ObservableCollection<TestType> LabTestTypes { get; } = new ObservableCollection<TestType>();
        public ObservableCollection<TestType> ToolTestTypes { get; } = new ObservableCollection<TestType>();

        public ObservableCollection<Test> PhysicalTestList { get; } = new ObservableCollection<Test>();
        public ObservableCollection<Test> LabTestList { get; } = new ObservableCollection<Test>();
        public ObservableCollection<Test> ToolTestList { get; } = new ObservableCollection<Test>();

        private int _diagnosticLocator;
        public int DiagnosticLocator
        {
            get => _diagnosticLocator;
            set
            {
                _diagnosticLocator = value;
                OnPropertyChanged(nameof(DiagnosticLocator));
                switch (value)
                {
                    case 0:
                        PhysicalTemplates.Clear();
                        GetTemplateList(new TestType { Id = 9, TestMethod = TestMethod.Физикальная }); //id's may be not equal
                        break;
                    case 2:
                        PhysicalTemplates.Clear();
                        GetTemplateList(new TestType { Id = 9, TestMethod = TestMethod.Физикальная }); //id's may be not equal
                        break;
                    default:
                        break;
                }
            }
        }

        private ObservableCollection<TestData> _addedDatas = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> AddedDatas 
        {
            get 
            {
                _addedDatas.Clear();
                foreach (TestData data in PhysicalDiagData.Where(p=>p.Id == 0)) _addedDatas.Add(data);
                foreach (TestData data in ToolDiagData.Where(p => p.Id == 0)) _addedDatas.Add(data);
                foreach (TestData data in LabDiagData.Where(p => p.Id == 0)) _addedDatas.Add(data);
                return _addedDatas;
            }
        }

        public ObservableCollection<TestData> PhysicalDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> LabDiagData { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> ToolDiagData { get; } = new ObservableCollection<TestData>();

        private async void Initialize(Entry entry)
        {
            if (entry != null && entry.MedCard != null)
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
            IEnumerable<TestType> result = await ambulatoryDataService.GetTestTypeList(testMethod);
            if (testMethod == TestMethod.Лабараторная) foreach (TestType item in result) LabTestTypes.Add(item);
            else if (testMethod == TestMethod.Инструментальная) foreach (TestType item in result) ToolTestTypes.Add(item);
        }
        private async void GetTestList(TestMethod testMethod, TestType testType = null)
        {
            IEnumerable<Test> result = await ambulatoryDataService.GetTestList(testMethod, testType);
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
        private async void GetTemplateList(TestType testType)
        {
            var arr = await genericTemplateServices.GetWithInclude(t => t.Category.Id == testType.Id, t => t.Category);
            switch (testType.TestMethod)
            {
                case TestMethod.Физикальная:
                    PhysicalTemplates.Clear();
                    foreach (TestTemplate template in arr) PhysicalTemplates.Add(template);
                    break;
                case TestMethod.Лабараторная:
                    LabTemplates.Clear();
                    foreach (TestTemplate template in arr) LabTemplates.Add(template);
                    break;
                case TestMethod.Инструментальная:
                    ToolTemplates.Clear();
                    foreach (TestTemplate template in arr) ToolTemplates.Add(template);
                    break;
                default:
                    break;
            }
        }

        public async void AddTemplate(TestMethod testMethod)
        {
            switch (testMethod)
            {
                case TestMethod.Физикальная:
                    if (CurrentPhysicalTemplate != null && CurrentPhysicalTemplate.Objects.Count != 0)
                    {
                        var result = await ambulatoryDataService.GetTestList(CurrentPhysicalTemplate.Objects);
                        foreach (Test test in result)
                        {
                            var data = CreateData(test, null, test.DefaultOption);
                            data.DateResult = DateTime.Now;
                            data.StaffResult = currentEntry.DoctorDestination;
                            PhysicalDiagData.Add(data);
                        }
                    }
                    break;
                case TestMethod.Лабараторная:
                    if (CurrentLabTemplate != null && CurrentLabTemplate.Objects.Count != 0)
                    {
                        var result = await ambulatoryDataService.GetTestList(CurrentLabTemplate.Objects);
                        foreach (Test test in result) LabDiagData.Add(CreateData(test, null, test.DefaultOption));
                    }
                    break;
                case TestMethod.Инструментальная:
                    if (CurrentToolTemplate != null && CurrentToolTemplate.Objects.Count != 0)
                    {
                        var result = await ambulatoryDataService.GetTestList(CurrentToolTemplate.Objects);
                        foreach (Test test in result) ToolDiagData.Add(CreateData(test, null, test.DefaultOption));
                    }
                    break;
                default:
                    break;
            }
            OnPropertyChanged(nameof(AddedDatas));
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
            OnPropertyChanged(nameof(AddedDatas));
            NotificationManager.AddItem(new NotificationItem(NotificationType.Success, TimeSpan.FromSeconds(3), "Success"));
        }
        public void RemoveData(object testDatas)
        {
            var items = ((System.Collections.IList)testDatas).Cast<TestData>().ToList();
            int removecounter = 0;
            if (items.Count != 0)
                switch (items[0].Test.TestType.TestMethod)
                {
                    case TestMethod.Физикальная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == Enum.Parse<TestStatus>("3"))
                            { 
                                PhysicalDiagData.Remove(test);
                                removecounter++;
                            }
                        }
                        break;
                    case TestMethod.Лабараторная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
                            {
                                LabDiagData.Remove(test);
                                removecounter++;
                            }
                        }
                        break;
                    case TestMethod.Инструментальная:
                        foreach (TestData test in items)
                        {
                            if (test.Status == TestStatus.Резерв || test.Status == TestStatus.Редакция)
                            {
                                ToolDiagData.Remove(test);
                                removecounter++;
                            }
                        }
                        break;
                    default: break;
                }
            OnPropertyChanged(nameof(AddedDatas));
            NotificationManager.AddItem(new NotificationItem (NotificationType.Information, TimeSpan.FromSeconds(5), "Удалено " + removecounter.ToString() + " элементов 5 сек", true));
        }

        public async Task UpdateData()
        {
            foreach (TestData data in PhysicalDiagData.Where(p=>p.Id == 0)) data.Status = TestStatus.Готов;
            foreach (TestData data in LabDiagData.Where(l => l.Id == 0)) data.Status = TestStatus.Ожидание;
            foreach (TestData data in ToolDiagData.Where(t => t.Id == 0)) data.Status = TestStatus.Ожидание;
            await ambulatoryDataService.UpdateData(AddedDatas);
        }
    }
}
