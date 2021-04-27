using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    public class TherapyViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService;
        private readonly GenericDataServices<DrugSubClass> dataServicesDrugSubClass = new GenericDataServices<DrugSubClass>(new HospitalDbContextFactory());
        private readonly GenericDataServices<DrugGroup> dataServicesDrugGroup = new GenericDataServices<DrugGroup>(new HospitalDbContextFactory());

        private Entry _currentEntry;

        private string _diagnosisSearchTitleValue, _diagnosisSearchCodeValue, _drugSearchValue;

        private PharmacoTherapyData _pharmacoData = new PharmacoTherapyData();
        private PhysioTherapyData _physioData = new PhysioTherapyData();
        private SurgencyTherapyData _surgencyData = new SurgencyTherapyData();

        private DrugClass _currentDrugClass;
        private DrugSubClass _currentDrugSubClass;
        private DrugGroup _currentDrugGroup;

        //private PhysioTherapyFactor _currentPhysioFactor;
        private PhysTherFactGroup _currentPhysioFactGroup;

        private DiagnosisClass _currentDiagnosisClass;
        private DiagnosisGroup _currentDiagnosisGroup;

        private SurgencyType _currentSurgencyType;
        private SurgencyGroup _currentSurgencyGroup;

        private async void Initialize(Entry entry)
        {
            if (entry != null && entry.MedCard != null)
            {
                var res = await ambulatoryDataService.GetTestData(entry.MedCard.Id, true);
                foreach (TestData data in res) TestDatas.Add(data);

                var diagnosisClasses = await ambulatoryDataService.GetDiagnosisClasses();
                foreach (DiagnosisClass diagnosisClass in diagnosisClasses) DiagnosisClasses.Add(diagnosisClass);
                var drugsClasses = await new GenericDataServices<DrugClass>(new HospitalDbContextFactory()).GetAll();
                foreach (DrugClass drugClass in drugsClasses) DrugClasses.Add(drugClass);
                var pharmacoDatas = await ambulatoryDataService.GetPharmacoTherapyDatas(entry.MedCard.Id);
                foreach (PharmacoTherapyData pharmacoTherapyData in pharmacoDatas) PharmacoTherapyDatas.Add(pharmacoTherapyData);

                var physioDatas = await ambulatoryDataService.GetPhysioTherapyDatas(entry.MedCard.Id);
                foreach (PhysioTherapyData physioTherapyData in physioDatas) PhysioTherapyDatas.Add(physioTherapyData);
                var physioGroups = await ambulatoryDataService.GetPhysioGroups();
                foreach (PhysTherFactGroup physTherFactGroup in physioGroups) PhysTherFactGroups.Add(physTherFactGroup);

                var surgencyDatas = await ambulatoryDataService.GetSurgencyTherapyDatas(entry.MedCard.Id);
                foreach (SurgencyTherapyData surgencyData in surgencyDatas) SurgencyTherapyDatas.Add(surgencyData);

                PharmacoData.MedCard = entry.MedCard;
                PharmacoData.TherapyDoctor = entry.DoctorDestination;
                PhysioData.MedCard = entry.MedCard;
                PhysioData.TherapyDoctor = entry.DoctorDestination;
                PhysioData.PhysTherStatus = PhysTherStatus.Ожидание;

                SurgencyData.MedCard = entry.MedCard;
                SurgencyData.TherapyDoctor = entry.DoctorDestination;
                SurgencyData.SurgencyStatus = SurgencyStatus.Ожидание;

                Diagnoses.Add(entry.MedCard.Diagnosis);
            }
        }

        private async void SearchDiagnoses(string value, bool isCode = false)
        {
            if (value.Length > (isCode ? 1 : 2))
            {
                if (CurrentDiagnosis == null)
                {
                    Diagnoses.Clear();
                    var result = await ambulatoryDataService.GetDiagnoses(value, isCode);
                    foreach (Diagnosis diagnosis in result)
                    {
                        Diagnoses.Add(diagnosis);
                        //if (diagnosis.Title == value) CurrentDiagnosis = diagnosis;
                    }
                }
            }
            else
            {
                Diagnoses.Clear();
                CurrentDiagnosis = null;
            }
        }
        private async void SearchDrugs(string value)
        {
            if (value.Length > 2)
            {
                if (PharmacoData.Drug == null)
                {
                    Drugs.Clear(); //drugsearchlist
                    var result = await ambulatoryDataService.GetDrugs(value);
                    foreach (Drug drug in result)
                    {
                        Drugs.Add(drug);
                    }
                }
            }
            else PharmacoData.Drug = null;
        }

        private async void GetDrugSubClass(DrugClass drugClass)
        {
            DrugSubClasses.Clear();
            var result = await dataServicesDrugSubClass.GetWhere(d => d.DrugClass == drugClass);
            foreach (DrugSubClass drugSubClass in result) DrugSubClasses.Add(drugSubClass);
        }
        private async void GetDrugGroup(DrugSubClass drugSubClass)
        {
            DrugGroups.Clear();
            var result = await dataServicesDrugGroup.GetWhere(d => d.DrugSubClass == drugSubClass);
            foreach (DrugGroup drugGroup in result) DrugGroups.Add(drugGroup);
        }
        private async void GetDrugs(DrugGroup drugGroup)
        {
            Drugs.Clear();
            var result = await ambulatoryDataService.GetDrugs(drugGroup);
            foreach (Drug drug in result) Drugs.Add(drug);
        }

        private async void GetDiagnosisGroups(DiagnosisClass diagnosisClass)
        {
            DiagnosisGroups.Clear();
            var res = await ambulatoryDataService.GetDiagnosisGroups(diagnosisClass);
            foreach (DiagnosisGroup group in res) DiagnosisGroups.Add(group);
        }
        private async void GetDiagnoses(DiagnosisGroup diagnosisGroup)
        {
            Diagnoses.Clear();
            var res = await ambulatoryDataService.GetDiagnoses(diagnosisGroup);
            foreach (Diagnosis diagnosis in res) Diagnoses.Add(diagnosis);
        }

        private async void GetPhysioFactors(PhysTherFactGroup physTherFactGroup)
        {
            PhysioTherapyFactors.Clear();
            var res = await ambulatoryDataService.GetPhysioFactors(physTherFactGroup);
            foreach (PhysioTherapyFactor factor in res) PhysioTherapyFactors.Add(factor);
        } //null check all above on method params

        private async void GetSurgencyGroups(SurgencyType type)
        {
            SurgencyGroups.Clear();
            var res = await ambulatoryDataService.GetSurgencyGroups(type);
            foreach (SurgencyGroup group in res) SurgencyGroups.Add(group);
        }
        private async void GetSurgencyOperations(SurgencyGroup group)
        {
            if (group != null)
            {
                SurgencyOperations.Clear();
                var res = await ambulatoryDataService.GetSurgencyOperations(group);
                foreach (SurgencyOperation operation in res) SurgencyOperations.Add(operation);
            }
        }


        public TherapyViewModel(Entry entry, AmbulatoryDataService dataService)
        {
            _currentEntry = entry;
            ambulatoryDataService = dataService;
            Initialize(entry);
        }

        public ObservableCollection<TestData> TestDatas { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<Diagnosis> Diagnoses { get; } = new ObservableCollection<Diagnosis>();
        public ObservableCollection<DiagnosisClass> DiagnosisClasses { get; } = new ObservableCollection<DiagnosisClass>();
        public ObservableCollection<DiagnosisGroup> DiagnosisGroups { get; } = new ObservableCollection<DiagnosisGroup>();

        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; } = new ObservableCollection<PharmacoTherapyData>();
        public ObservableCollection<DrugClass> DrugClasses { get; } = new ObservableCollection<DrugClass>();
        public ObservableCollection<DrugSubClass> DrugSubClasses { get; } = new ObservableCollection<DrugSubClass>();
        public ObservableCollection<DrugGroup> DrugGroups { get; } = new ObservableCollection<DrugGroup>();
        public ObservableCollection<DrugSubGroup> DrugSubGroups { get; } = new ObservableCollection<DrugSubGroup>();
        public ObservableCollection<Drug> Drugs { get; } = new ObservableCollection<Drug>();

        public ObservableCollection<PhysioTherapyData> PhysioTherapyDatas { get; } = new ObservableCollection<PhysioTherapyData>();
        public ObservableCollection<PhysTherFactGroup> PhysTherFactGroups { get; } = new ObservableCollection<PhysTherFactGroup>();
        public ObservableCollection<PhysioTherapyFactor> PhysioTherapyFactors { get; } = new ObservableCollection<PhysioTherapyFactor>();

        public ObservableCollection<SurgencyTherapyData> SurgencyTherapyDatas { get; } = new ObservableCollection<SurgencyTherapyData>();

        public ObservableCollection<SurgencyGroup> SurgencyGroups { get; } = new ObservableCollection<SurgencyGroup>();
        public ObservableCollection<SurgencyOperation> SurgencyOperations { get; } = new ObservableCollection<SurgencyOperation>();

        public PharmacoTherapyData PharmacoData { get => _pharmacoData; set { _pharmacoData = value; OnPropertyChanged(nameof(PharmacoData)); } }
        public DrugClass CurrentDrugClass
        {
            get => _currentDrugClass;
            set
            {
                _currentDrugClass = value;
                OnPropertyChanged(nameof(CurrentDrugClass));
                GetDrugSubClass(value);
            }
        }
        public DrugSubClass CurrentDrugSubClass
        {
            get => _currentDrugSubClass;
            set
            {
                _currentDrugSubClass = value;
                OnPropertyChanged(nameof(CurrentDrugSubClass));
                GetDrugGroup(value);
            }
        }
        public DrugGroup CurrentDrugGroup
        {
            get => _currentDrugGroup;
            set
            {
                _currentDrugGroup = value;
                OnPropertyChanged(nameof(CurrentDrugGroup));
                GetDrugs(value);
            }
        }
        public Drug CurrentDrug
        {
            get => PharmacoData.Drug;
            set
            {
                if (value != null)
                {
                    PharmacoData.Drug = value;
                    PharmacoData.Trademark = value.Title;
                }
                OnPropertyChanged(nameof(CurrentDrug));
            }
        }
        public string DrugSearchValue
        {
            get => _drugSearchValue;
            set
            {
                _drugSearchValue = value;
                OnPropertyChanged(nameof(DrugSearchValue));
                SearchDrugs(value);
            }
        }

        public PhysioTherapyData PhysioData { get => _physioData; set { _physioData = value; OnPropertyChanged(nameof(PhysioData)); } }
        public PhysTherFactGroup CurrentPhysioFactGroup 
        {
            get => _currentPhysioFactGroup; 
            set
            {
                _currentPhysioFactGroup = value; 
                OnPropertyChanged(nameof(CurrentPhysioFactGroup));
                GetPhysioFactors(value);
            }
        }
        public PhysioTherapyFactor CurrentPhysioFactor { get => PhysioData.PhysioTherapyFactor; set { PhysioData.PhysioTherapyFactor = value; OnPropertyChanged(nameof(CurrentPhysioFactor));} }

        public DiagnosisClass CurrentDiagnosisClass
        {
            get => _currentDiagnosisClass;
            set
            {
                _currentDiagnosisClass = value;
                OnPropertyChanged(nameof(CurrentDiagnosisClass));
                GetDiagnosisGroups(value);
            }
        }
        public DiagnosisGroup CurrentDiagnosisGroup
        {
            get => _currentDiagnosisGroup;
            set
            {
                _currentDiagnosisGroup = value;
                OnPropertyChanged(nameof(CurrentDiagnosisGroup));
                GetDiagnoses(value);
            }
        }
        public Diagnosis CurrentDiagnosis
        {
            get => _currentEntry.MedCard.Diagnosis;
            set
            {
                _currentEntry.MedCard.Diagnosis = value;
                OnPropertyChanged(nameof(CurrentDiagnosis));
                _currentEntry.MedCard.DiagnosisDoctor = _currentEntry.DoctorDestination;
                _currentEntry.MedCard.DiagnosisDate = DateTime.Now;
            }
        }
        public string DiagnosisSearchTitleValue
        {
            get => _diagnosisSearchTitleValue;
            set
            {
                _diagnosisSearchTitleValue = value;
                OnPropertyChanged(nameof(DiagnosisSearchTitleValue));
                SearchDiagnoses(value);
            }
        }
        public string DiagnosisSearchCodeValue
        {
            get => _diagnosisSearchCodeValue;
            set
            {
                _diagnosisSearchCodeValue = value;
                OnPropertyChanged(nameof(DiagnosisSearchCodeValue));
                SearchDiagnoses(value, true);
            }
        }

        public SurgencyTherapyData SurgencyData { get => _surgencyData; set { _surgencyData = value; OnPropertyChanged(nameof(SurgencyData)); } }
        public SurgencyType CurrentSurgencyType 
        {
            get => _currentSurgencyType; 
            set 
            {
                _currentSurgencyType = value; 
                OnPropertyChanged(nameof(CurrentSurgencyType));
                GetSurgencyGroups(value);
            }
        }
        public SurgencyGroup CurrentSurgencyGroup
        {
            get => _currentSurgencyGroup;
            set
            {
                _currentSurgencyGroup = value;
                OnPropertyChanged(nameof(CurrentSurgencyGroup));
                GetSurgencyOperations(value);
            }
        }

        private ObservableCollection<ITherapyData> _addedDatas = new ObservableCollection<ITherapyData>();
        public ObservableCollection<ITherapyData> AddedDatas
        {
            get
            {
                _addedDatas.Clear();
                foreach (ITherapyData data in PharmacoTherapyDatas.Where(p => p.Id == 0)) _addedDatas.Add(data);
                foreach (ITherapyData data in PhysioTherapyDatas.Where(p => p.Id == 0)) _addedDatas.Add(data);
                foreach (ITherapyData data in SurgencyTherapyDatas.Where(s => s.Id == 0)) _addedDatas.Add(data);
                return _addedDatas;
            }
        }

        public void AddPharmacoTherapyData()
        {
            var data = (PharmacoTherapyData)PharmacoData.Clone();
            data.Diagnosis = _currentEntry.MedCard.Diagnosis;
            data.DiagnosisDoctor = _currentEntry.MedCard.DiagnosisDoctor;
            data.DiagnosisDate = _currentEntry.MedCard.DiagnosisDate;
            data.DateCreate = DateTime.Now;
            PharmacoTherapyDatas.Add(data);
            PharmacoData.Drug = null;
            PharmacoData.Trademark = null;
            PharmacoData.Dose = null;
            CurrentDrug = null;
            DrugSearchValue = "";
            OnPropertyChanged(nameof(AddedDatas));
        }
        public void RemovePharmacoTherapyData(object testDatas)
        {
            var items = ((IList)testDatas).Cast<PharmacoTherapyData>().ToList();
            int removecounter = 0;
            foreach (PharmacoTherapyData data in items)
            {
                if (data.Id == 0)
                {
                    PharmacoTherapyDatas.Remove(data);
                    removecounter++;
                }
            }
            OnPropertyChanged(nameof(AddedDatas));
            NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Удалено " + removecounter.ToString() + " элементов", true));
        }

        public void AddPhysioTherapyData()
        {
            var data = (PhysioTherapyData)PhysioData.Clone();
            data.Diagnosis = _currentEntry.MedCard.Diagnosis;
            data.DiagnosisDoctor = _currentEntry.MedCard.DiagnosisDoctor;
            data.DiagnosisDate = _currentEntry.MedCard.DiagnosisDate;
            data.CreateDateTime = DateTime.Now;
            PhysioTherapyDatas.Add(data);
            OnPropertyChanged(nameof(AddedDatas));
            PhysioData.RemainingTime = TimeSpan.FromSeconds(0);
            //PhysioData.PhysioTherapyFactor = null;
            CurrentPhysioFactor = null;
            PhysioData.Localization = null;
            PhysioData.Params = null;
        }
        public void RemovePhysioTherapyData(object physioDatas) 
        {
            var items = ((IList)physioDatas).Cast<PhysioTherapyData>().ToList();
            int removecounter = 0;
            foreach (PhysioTherapyData data in items)
            {
                if (data.Id == 0)
                {
                    PhysioTherapyDatas.Remove(data);
                    removecounter++;
                }
            }
            NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Удалено " + removecounter.ToString() + " элементов", true));
            OnPropertyChanged(nameof(AddedDatas));
        }

        public void AddSurgencyTherapyData()
        {
            var data = (SurgencyTherapyData)SurgencyData.Clone();
            data.Diagnosis = _currentEntry.MedCard.Diagnosis;
            data.DiagnosisDoctor = _currentEntry.MedCard.DiagnosisDoctor;
            data.DiagnosisDate = _currentEntry.MedCard.DiagnosisDate;
            data.CreateDateTime = DateTime.Now;
            SurgencyTherapyDatas.Add(data);
            CurrentSurgencyGroup = null;
            SurgencyData.Option = null;
            SurgencyData.SurgencyOperation = null;
            OnPropertyChanged(nameof(AddedDatas));
        }
        public void RemoveSurgencyTherapyData(object surgencyDatas)
        {
            var items = ((IList)surgencyDatas).Cast<SurgencyTherapyData>().ToList();
            int removecounter = 0;
            foreach (SurgencyTherapyData data in items)
            {
                if (data.Id == 0)
                {
                    SurgencyTherapyDatas.Remove(data);
                    removecounter++;
                }
            }
            OnPropertyChanged(nameof(AddedDatas));
            NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Удалено " + removecounter.ToString() + " элементов", true));
        }

        public async Task UpdateData() => await ambulatoryDataService.UpdateData(AddedDatas);
    }
}
