using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class TestContainer : INotifyPropertyChanged
    {
        private readonly ITestDataService testDataService = new AmbulatoryDataService(new HospitalDbContextFactory());
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
        public TestTemplate CurrentTemplate { get => _currentTemplate; set { _currentTemplate = value; OnPropertyChanged(nameof(CurrentTemplate)); }}

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

        public TestData GenerateData()
        {
            return new TestData
            {
                Test = CurrentTest,
                Value = DataValue,
                Option = DataOption,
                MedCard = entry.MedCard,
                Status = TestStatus.Редакция,
                StaffResult = entry.DoctorDestination,
                DateCreate = DateTime.Now,
                DateResult = DateTime.Now //ждем внедения модуля с процедурами, а пока заглушка
            };
        }

        private async Task GetData(int medCardId, TestMethod testMethod)
        {
            Datas.Clear();
            IsLoading = true;
            foreach (TestData data in await testDataService.GetTestData(medCardId, testMethod))
                Datas.Add(data);
            IsLoading = false;
        }
        private async Task GetTypeList(TestMethod method)
        {
            TypeList.Clear();
            IsLoading = true;
            foreach (TestType type in await testDataService.GetTestTypeList(method))
                TypeList.Add(type);
            IsLoading = false;
        }
        private async void GetTestList(TestType type)
        {
            TestList.Clear();
            Templates.Clear();
            IsLoading = true;
            foreach (Test test in await testDataService.GetTestList(type)) TestList.Add(test);
            foreach (TestTemplate template in await testDataService.GetTemplateList(type)) Templates.Add(template);
            IsLoading = false;
        }

        public void RemoveRangeData(IList<TestData> datas)
        {
            int removecounter = 0;
            if (datas.Count != 0)
            {
                foreach (TestData data in datas)
                    if (data.Status == Enum.Parse<TestStatus>("3"))
                    {
                        Datas.Remove(data);
                        removecounter++;
                    }
                NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(4), "Удалено " + removecounter.ToString() + " элементов", true));
            }
        }
        public async void AddTemplate()
        {
            foreach (Test test in await testDataService.GetTestList(CurrentTemplate.Objects))
            {
                Datas.Add(new TestData()
                {
                    Test = test,
                    Value = "",
                    Option = test.DefaultOption,
                    MedCard = entry.MedCard,
                    Status = TestStatus.Редакция,
                    StaffResult = entry.DoctorDestination,
                    DateCreate = DateTime.Now,
                    DateResult = DateTime.Now
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
