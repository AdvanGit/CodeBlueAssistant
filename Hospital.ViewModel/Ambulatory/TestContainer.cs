using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class TestContainer : INotifyPropertyChanged
    {
        private readonly ITestDataService testDataService;

        public TestContainer(HospitalDbContextFactory contextFactory)
        {
            testDataService = new TestDataService(contextFactory);
        }

        private Entry entry;

        private bool _isLoading;

        private TestData _currentData;
        private TestType _currentType;
        private Test _currentTest;
        private TestTemplate _currentTemplate;

        private string _dataOption;
        private string _dataValue;

        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }

        public TestData CurrentData { get => _currentData; set { _currentData = value; OnPropertyChanged(nameof(CurrentData)); } }
        public TestType CurrentType
        {
            get => _currentType;
            set
            {
                _currentType = value;
                OnPropertyChanged(nameof(CurrentType));
                GetTestList(value);
            }
        }
        public Test CurrentTest { get => _currentTest; set { _currentTest = value; OnPropertyChanged(nameof(CurrentTest)); } }
        public TestTemplate CurrentTemplate { get => _currentTemplate; set { _currentTemplate = value; OnPropertyChanged(nameof(CurrentTemplate)); } }

        public string DataOption { get => _dataOption; set { _dataOption = value; OnPropertyChanged(nameof(DataOption)); } }
        public string DataValue { get => _dataValue; set { _dataValue = value; OnPropertyChanged(nameof(DataValue)); } }

        public ObservableCollection<TestType> TypeList { get; } = new ObservableCollection<TestType>();
        public ObservableCollection<Test> TestList { get; } = new ObservableCollection<Test>();
        public ObservableCollection<TestData> Datas { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<TestTemplate> Templates { get; } = new ObservableCollection<TestTemplate>();

        protected internal async Task Initialize(TestMethod testMethod, Entry entry)
        {

            if (entry.MedCard != null) await GetData(entry.MedCard.Id, testMethod);
            await GetTypeList(testMethod);
            this.entry = entry;
        }

        private async Task GetData(int medCardId, TestMethod testMethod)
        {
            IsLoading = true;
            try
            {
                var datas = await testDataService.GetTestData(medCardId, testMethod);
                Datas.Clear();
                foreach (TestData data in datas) Datas.Add(data);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoading = false;
        }
        private async Task GetTypeList(TestMethod method)
        {
            IsLoading = true;
            try
            {
                var datas = await testDataService.GetTestTypeList(method);
                TypeList.Clear();
                foreach (TestType type in datas) TypeList.Add(type);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoading = false;
        }
        private async void GetTestList(TestType type)
        {
            IsLoading = true;
            try
            {
                var testItems = await testDataService.GetTestList(type);
                TestList.Clear();
                foreach (Test test in testItems) TestList.Add(test);

                var tempItems = await testDataService.GetTemplateList(type);
                Templates.Clear();
                foreach (TestTemplate template in tempItems) Templates.Add(template);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoading = false;
        }

        public TestData GenerateData()
        {
            return new TestData
            {
                Test = CurrentTest,
                Value = DataValue,
                Option = DataOption,
                MedCard = entry.MedCard,
                Status = ProcedureStatus.Редакция,
                StaffResult = entry.DoctorDestination,
                DateCreate = DateTime.Now,
                DateResult = DateTime.Now //ждем внедения модуля с процедурами, а пока заглушка
            };
        }
        public void RemoveRangeData(object obj)
        {
            int removecounter = 0;
            var datas = (obj as IList).Cast<TestData>().ToList();
            if (datas.Count != 0)
            {
                foreach (TestData data in datas)
                    if (data.Status == Enum.Parse<ProcedureStatus>("3"))
                    {
                        Datas.Remove(data);
                        removecounter++;
                    }
                NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(4), "Удалено " + removecounter.ToString() + " элементов", true));
            }
        }
        public async Task AddTemplate()
        {
            IsLoading = true;
            try
            {
                var datas = await testDataService.GetTestList(CurrentTemplate.Objects);
                foreach (Test test in datas)
                {
                    Datas.Add(new TestData()
                    {
                        Test = test,
                        Value = "",
                        Option = test.DefaultOption,
                        MedCard = entry.MedCard,
                        Status = ProcedureStatus.Редакция,
                        StaffResult = entry.DoctorDestination,
                        DateCreate = DateTime.Now,
                        DateResult = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
