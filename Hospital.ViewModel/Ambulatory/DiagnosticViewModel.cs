using Hospital.Domain.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class DiagnosticViewModel : MainViewModel
    {
        public TestContainer PhysicalContainer { get; } = new TestContainer();
        public TestContainer ToolContainer { get; } = new TestContainer();
        public TestContainer LabContainer { get; } = new TestContainer();

        private ObservableCollection<TestData> _addedDatas = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> AddedDatas
        {
            get
            {
                _addedDatas.Clear();
                foreach (TestData data in PhysicalContainer.Datas.Where(p => p.Id == 0)) _addedDatas.Add(data);
                foreach (TestData data in ToolContainer.Datas.Where(p => p.Id == 0)) _addedDatas.Add(data);
                foreach (TestData data in LabContainer.Datas.Where(p => p.Id == 0)) _addedDatas.Add(data);
                return _addedDatas;
            }
        }
        private ObservableCollection<TestData> _dataIsSymptome = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> DataIsSymptom
        {
            get
            {
                _dataIsSymptome.Clear();
                foreach (TestData data in PhysicalContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptome.Add(data);
                foreach (TestData data in ToolContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptome.Add(data);
                foreach (TestData data in LabContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptome.Add(data);
                return _dataIsSymptome;
            }
        }

        public int DataAwaitCount => LabContainer.Datas.Where(l => l.Status == Enum.Parse<ProcedureStatus>("0")).Count()
            + ToolContainer.Datas.Where(t => t.Status == Enum.Parse<ProcedureStatus>("0")).Count();
        public int DataIsSymptomCount => PhysicalContainer.Datas.Where(p => p.IsSymptom).Count()
            + LabContainer.Datas.Where(l => l.IsSymptom).Count()
            + ToolContainer.Datas.Where(t => t.IsSymptom).Count();
        public int DataCount => PhysicalContainer.Datas.Count() + LabContainer.Datas.Count() + ToolContainer.Datas.Count();

        protected internal async Task Initialize(Entry entry) //container task to void
        {
            await LabContainer.Initialize(TestMethod.Лабараторная, entry);
            await ToolContainer.Initialize(TestMethod.Инструментальная, entry);
            await PhysicalContainer.Initialize(TestMethod.Физикальная, entry);
            PhysicalContainer.CurrentType = PhysicalContainer.TypeList.FirstOrDefault();
            RaiseDataPropetryChange();
        }

        public void RaiseDataPropetryChange()
        {
            OnPropertyChanged(nameof(AddedDatas));
            OnPropertyChanged(nameof(DataAwaitCount));
            OnPropertyChanged(nameof(DataCount));
            RaiseIsSymptomPropetryChange();
        }
        public void RaiseIsSymptomPropetryChange()
        {
            OnPropertyChanged(nameof(DataIsSymptom));
            OnPropertyChanged(nameof(DataIsSymptomCount));
        }
    }
}
