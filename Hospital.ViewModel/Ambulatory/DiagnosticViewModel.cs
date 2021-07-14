using Hospital.Domain.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class DiagnosticViewModel : MainViewModel
    {
        public TestContainer PhysicalContainer { get; } = new TestContainer(contextFactory);
        public TestContainer ToolContainer { get; } = new TestContainer(contextFactory);
        public TestContainer LabContainer { get; } = new TestContainer(contextFactory);

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
        private ObservableCollection<TestData> _dataIsSymptom = new ObservableCollection<TestData>();
        public ObservableCollection<TestData> DataIsSymptom
        {
            get
            {
                _dataIsSymptom.Clear();
                foreach (TestData data in PhysicalContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptom.Add(data);
                foreach (TestData data in ToolContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptom.Add(data);
                foreach (TestData data in LabContainer.Datas.Where(p => p.IsSymptom)) _dataIsSymptom.Add(data);
                return _dataIsSymptom;
            }
        }

        public int DataAwaitCount => LabContainer.Datas.Where(l => l.Status == Enum.Parse<ProcedureStatus>("0")).Count()
            + ToolContainer.Datas.Where(t => t.Status == Enum.Parse<ProcedureStatus>("0")).Count();
        public int DataIsSymptomCount => PhysicalContainer.Datas.Where(p => p.IsSymptom).Count()
            + LabContainer.Datas.Where(l => l.IsSymptom).Count()
            + ToolContainer.Datas.Where(t => t.IsSymptom).Count();
        public int DataCount => PhysicalContainer.Datas.Count() + LabContainer.Datas.Count() + ToolContainer.Datas.Count();

        protected internal async Task Initialize(Entry entry)
        {
            await Task.WhenAll(LabContainer.Initialize(TestMethod.Лабараторная, entry),
                                ToolContainer.Initialize(TestMethod.Инструментальная, entry),
                                PhysicalContainer.Initialize(TestMethod.Физикальная, entry));
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
