using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Ambulatory
{
    /// <summary>
    /// Всю структуру класса нужно переделать с созданием контейнера наследника ITherapyData, аналогично DiagnosticViewModel
    /// </summary>
    public class TherapyViewModel : MainViewModel
    {
        private readonly ITherapyDataService _therapyDataService;
        private readonly IDbContextFactory<HospitalDbContext> _contextFactory;

        public TherapyViewModel(ITherapyDataService therapyDataService, IDbContextFactory<HospitalDbContext> contextFactory)
        {
            _therapyDataService = therapyDataService;
            _contextFactory = contextFactory;
        }

        private Entry _currentEntry;
        private MedCard _medCard;

        private string _diagnosisSearchTitleValue, _diagnosisSearchCodeValue, _drugSearchValue;

        private bool _isLoadingPharma;
        private bool _isLoadingSurgery;
        private bool _isLoadingPhysio;
        private bool _isLoadingDiagnosis;

        private PharmacoTherapyData _pharmacoData = new PharmacoTherapyData();
        private PhysioTherapyData _physioData = new PhysioTherapyData();
        private SurgeryTherapyData _surgeryData = new SurgeryTherapyData();

        private DrugClass _currentDrugClass;
        private DrugSubClass _currentDrugSubClass;
        private DrugGroup _currentDrugGroup;

        //private PhysioTherapyFactor _currentPhysioFactor; //на данный момент не используется
        private PhysTherFactGroup _currentPhysioFactGroup;

        private DiagnosisClass _currentDiagnosisClass;
        private DiagnosisGroup _currentDiagnosisGroup;

        private SurgeryType _currentSurgeryType;
        private SurgeryGroup _currentSurgeryGroup;

        internal protected async Task Initialize(Entry entry)
        {
            _currentEntry = entry;
            MedCard = entry.MedCard;
            try
            {
                IsLoadingDiagnosis = true;
                var diagnosisClasses = await new GenericRepository<DiagnosisClass>(_contextFactory).GetAll();
                DiagnosisClasses.Clear();
                foreach (DiagnosisClass diagnosisClass in diagnosisClasses) DiagnosisClasses.Add(diagnosisClass);
                IsLoadingDiagnosis = false;

                IsLoadingPharma = true;
                var drugsClasses = await new GenericRepository<DrugClass>(_contextFactory).GetAll();
                DrugClasses.Clear();
                foreach (DrugClass drugClass in drugsClasses) DrugClasses.Add(drugClass);
                var pharmacoDatas = await _therapyDataService.GetPharmacoTherapyDatas(entry.MedCard.Id);
                PharmacoTherapyDatas.Clear();
                foreach (PharmacoTherapyData pharmacoTherapyData in pharmacoDatas) PharmacoTherapyDatas.Add(pharmacoTherapyData);
                IsLoadingPharma = false;

                IsLoadingPhysio = true;
                var physioDatas = await _therapyDataService.GetPhysioTherapyDatas(entry.MedCard.Id);
                PhysioTherapyDatas.Clear();
                foreach (PhysioTherapyData physioTherapyData in physioDatas) PhysioTherapyDatas.Add(physioTherapyData);
                var physioGroups = await new GenericRepository<PhysTherFactGroup>(_contextFactory).GetAll();
                PhysTherFactGroups.Clear();
                foreach (PhysTherFactGroup physTherFactGroup in physioGroups) PhysTherFactGroups.Add(physTherFactGroup);
                IsLoadingPhysio = false;

                IsLoadingSurgery = true;
                var surgeryDatas = await _therapyDataService.GetSurgeryTherapyDatas(entry.MedCard.Id);
                SurgeryTherapyDatas.Clear();
                foreach (SurgeryTherapyData surgeryData in surgeryDatas) SurgeryTherapyDatas.Add(surgeryData);
                CurrentSurgeryType = SurgeryType.Оперативная;
                IsLoadingSurgery = false;
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }

            PharmacoData.MedCard = entry.MedCard;
            PharmacoData.TherapyDoctor = entry.DoctorDestination;
            PhysioData.MedCard = entry.MedCard;
            PhysioData.TherapyDoctor = entry.DoctorDestination;
            PhysioData.ProcedureStatus = ProcedureStatus.Редакция;

            SurgeryData.MedCard = entry.MedCard;
            SurgeryData.TherapyDoctor = entry.DoctorDestination;
            SurgeryData.ProcedureStatus = ProcedureStatus.Редакция;

            Diagnoses.Add(entry.MedCard?.Diagnosis);
            CurrentDiagnosis = entry.MedCard?.Diagnosis;

            RaiseDataPropertyChanged();
        }

        private async Task SearchDiagnoses(string value, bool isCode = false) //эксперементальная функция горячего поиска, нужно прерывание и задержка
        {
            if (value.Length > (isCode ? 1 : 2))
            {
                if (CurrentDiagnosis == null)
                {
                    IsLoadingDiagnosis = true;
                    try
                    {
                        var result = await _therapyDataService.GetDiagnoses(value, isCode);
                        Diagnoses.Clear();
                        foreach (Diagnosis diagnosis in result)
                        {
                            Diagnoses.Add(diagnosis);
                            //if (diagnosis.Title == value) CurrentDiagnosis = diagnosis;
                        }
                    }
                    catch (Exception ex)
                    {
                        NotificationManager.AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(3), ex.Message, true));
                    }
                    IsLoadingDiagnosis = false;
                }
            }
            else
            {
                CurrentDiagnosisGroup = null;
                CurrentDiagnosis = null;
            }
        }
        private async Task SearchDrugs(string value) //эксперементальная функция горячего поиска, нужно прерывание и задержка
        {
            if (value.Length > 2)
            {
                if (PharmacoData.Drug == null)
                {
                    IsLoadingPharma = true;
                    try
                    {
                        var result = await _therapyDataService.GetDrugs(value);
                        Drugs.Clear(); //drugsearchlist
                        foreach (Drug drug in result) Drugs.Add(drug);
                    }
                    catch (Exception ex)
                    {
                        NotificationManager.AddException(ex, 4);
                    }
                    IsLoadingPharma = false;
                }
            }
            else PharmacoData.Drug = null;
        }

        private async Task GetDrugSubClass(DrugClass drugClass)
        {
            IsLoadingPharma = true;
            try
            {
                DrugSubClasses.Clear();
                var result = await _therapyDataService.GetDrugSubClasses(drugClass);
                foreach (DrugSubClass drugSubClass in result) DrugSubClasses.Add(drugSubClass);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingPharma = false;
        }
        private async Task GetDrugGroup(DrugSubClass drugSubClass)
        {
            IsLoadingPharma = true;
            try
            {
                DrugGroups.Clear();
                var result = await _therapyDataService.GetDrugGroup(drugSubClass);
                foreach (DrugGroup drugGroup in result) DrugGroups.Add(drugGroup);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingPharma = false;
        }
        private async Task GetDrugs(DrugGroup drugGroup)
        {
            IsLoadingPharma = true;
            try
            {
                Drugs.Clear();
                var result = await _therapyDataService.GetDrugs(drugGroup);
                foreach (Drug drug in result) Drugs.Add(drug);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingPharma = false;
        }

        private async Task GetDiagnosisGroups(DiagnosisClass diagnosisClass)
        {
            IsLoadingDiagnosis = true;
            try
            {
                DiagnosisGroups.Clear();
                var res = await _therapyDataService.GetDiagnosisGroups(diagnosisClass);
                foreach (DiagnosisGroup group in res) DiagnosisGroups.Add(group);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingDiagnosis = false;
        }
        private async Task GetDiagnoses(DiagnosisGroup diagnosisGroup)
        {
            IsLoadingDiagnosis = true;
            try
            {
                Diagnoses.Clear();
                var res = await _therapyDataService.GetDiagnoses(diagnosisGroup);
                foreach (Diagnosis diagnosis in res) Diagnoses.Add(diagnosis);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingDiagnosis = false;
        }

        private async Task GetPhysioFactors(PhysTherFactGroup physTherFactGroup)
        {
            IsLoadingPhysio = true;
            try
            {
                PhysioTherapyFactors.Clear();
                var res = await _therapyDataService.GetPhysioFactors(physTherFactGroup);
                foreach (PhysioTherapyFactor factor in res) PhysioTherapyFactors.Add(factor);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingPhysio = false;
        } 

        private async Task GetSurgeryGroups(SurgeryType type)
        {
            IsLoadingSurgery = true;
            try
            {
                var res = await _therapyDataService.GetSurgeryGroups(type);
                SurgeryGroups.Clear();
                foreach (SurgeryGroup group in res) SurgeryGroups.Add(group);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingSurgery = false;
        }
        private async Task GetSurgeryOperations(SurgeryGroup group)
        {
            IsLoadingSurgery = true;
            try
            {
                SurgeryOperations.Clear();
                var res = await _therapyDataService.GetSurgeryOperations(group);
                foreach (SurgeryOperation operation in res) SurgeryOperations.Add(operation);
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 4);
            }
            IsLoadingSurgery = false;
        }

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

        public ObservableCollection<SurgeryTherapyData> SurgeryTherapyDatas { get; } = new ObservableCollection<SurgeryTherapyData>();
        public ObservableCollection<SurgeryGroup> SurgeryGroups { get; } = new ObservableCollection<SurgeryGroup>();
        public ObservableCollection<SurgeryOperation> SurgeryOperations { get; } = new ObservableCollection<SurgeryOperation>();

        public PharmacoTherapyData PharmacoData { get => _pharmacoData; set { _pharmacoData = value; OnPropertyChanged(nameof(PharmacoData)); } }
        public DrugClass CurrentDrugClass
        {
            get => _currentDrugClass;
            set
            {
                _currentDrugClass = value;
                OnPropertyChanged(nameof(CurrentDrugClass));
                GetDrugSubClass(value).ConfigureAwait(true);
            }
        }
        public DrugSubClass CurrentDrugSubClass
        {
            get => _currentDrugSubClass;
            set
            {
                _currentDrugSubClass = value;
                OnPropertyChanged(nameof(CurrentDrugSubClass));
                GetDrugGroup(value).ConfigureAwait(true);
            }
        }
        public DrugGroup CurrentDrugGroup
        {
            get => _currentDrugGroup;
            set
            {
                _currentDrugGroup = value;
                OnPropertyChanged(nameof(CurrentDrugGroup));
                GetDrugs(value).ConfigureAwait(true);
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
                SearchDrugs(value).ConfigureAwait(true);
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
                GetPhysioFactors(value).ConfigureAwait(true);
            }
        }
        public PhysioTherapyFactor CurrentPhysioFactor { get => PhysioData.PhysioTherapyFactor; set { PhysioData.PhysioTherapyFactor = value; OnPropertyChanged(nameof(CurrentPhysioFactor)); } }

        public DiagnosisClass CurrentDiagnosisClass
        {
            get => _currentDiagnosisClass;
            set
            {
                _currentDiagnosisClass = value;
                OnPropertyChanged(nameof(CurrentDiagnosisClass));
                GetDiagnosisGroups(value).ConfigureAwait(true);
            }
        }
        public DiagnosisGroup CurrentDiagnosisGroup
        {
            get => _currentDiagnosisGroup;
            set
            {
                _currentDiagnosisGroup = value;
                OnPropertyChanged(nameof(CurrentDiagnosisGroup));
                GetDiagnoses(value).ConfigureAwait(true);
            }
        }
        public Diagnosis CurrentDiagnosis
        {
            get => MedCard?.Diagnosis;
            set
            {
                if (value != null)
                {
                    MedCard.Diagnosis = value;

                    MedCard.DiagnosisDoctor = _currentEntry.DoctorDestination;
                    MedCard.DiagnosisDate = DateTime.Now;
                }
                else
                {
                    MedCard.Diagnosis = null;
                    MedCard.DiagnosisDoctor = null;
                }
                OnPropertyChanged(nameof(CurrentDiagnosis));
            }
        }
        public string DiagnosisSearchTitleValue
        {
            get => _diagnosisSearchTitleValue;
            set
            {
                _diagnosisSearchTitleValue = value;
                OnPropertyChanged(nameof(DiagnosisSearchTitleValue));
                SearchDiagnoses(value).ConfigureAwait(true);
            }
        }
        public string DiagnosisSearchCodeValue
        {
            get => _diagnosisSearchCodeValue;
            set
            {
                _diagnosisSearchCodeValue = value;
                OnPropertyChanged(nameof(DiagnosisSearchCodeValue));
                SearchDiagnoses(value, true).ConfigureAwait(true);
            }
        }

        public SurgeryTherapyData SurgeryData { get => _surgeryData; set { _surgeryData = value; OnPropertyChanged(nameof(SurgeryData)); } }
        public SurgeryType CurrentSurgeryType
        {
            get => _currentSurgeryType;
            set
            {
                _currentSurgeryType = value;
                OnPropertyChanged(nameof(CurrentSurgeryType));
                GetSurgeryGroups(value).ConfigureAwait(true);
            }
        }
        public SurgeryGroup CurrentSurgeryGroup
        {
            get => _currentSurgeryGroup;
            set
            {
                _currentSurgeryGroup = value;
                OnPropertyChanged(nameof(CurrentSurgeryGroup));
                GetSurgeryOperations(value).ConfigureAwait(true);
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
                foreach (ITherapyData data in SurgeryTherapyDatas.Where(s => s.Id == 0)) _addedDatas.Add(data);
                return _addedDatas;
            }
        }

        public bool IsLoadingPharma { get => _isLoadingPharma; set { _isLoadingPharma = value; OnPropertyChanged(nameof(IsLoadingPharma)); } }
        public bool IsLoadingSurgery { get => _isLoadingSurgery; set { _isLoadingSurgery = value; OnPropertyChanged(nameof(IsLoadingSurgery)); } }
        public bool IsLoadingPhysio { get => _isLoadingPhysio; set { _isLoadingPhysio = value; OnPropertyChanged(nameof(IsLoadingPhysio)); } }
        public bool IsLoadingDiagnosis { get => _isLoadingDiagnosis; set { _isLoadingDiagnosis = value; OnPropertyChanged(nameof(IsLoadingDiagnosis)); } }
        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }

        public int AwaitCount { get => PhysioTherapyDatas.Where(p => p.ProcedureStatus == ProcedureStatus.Ожидание).Count() + SurgeryTherapyDatas.Where(s => s.ProcedureStatus == ProcedureStatus.Ожидание).Count(); }

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

        public void AddSurgeryTherapyData()
        {
            var data = (SurgeryTherapyData)SurgeryData.Clone();
            data.Diagnosis = _currentEntry.MedCard.Diagnosis;
            data.DiagnosisDoctor = _currentEntry.MedCard.DiagnosisDoctor;
            data.DiagnosisDate = _currentEntry.MedCard.DiagnosisDate;
            data.CreateDateTime = DateTime.Now;
            SurgeryTherapyDatas.Add(data);
            //CurrentSurgeryGroup = null;
            SurgeryData.Option = null;
            SurgeryData.SurgeryOperation = null;
            OnPropertyChanged(nameof(AddedDatas));
        }
        public void RemoveSurgeryTherapyData(object surgeryDatas)
        {
            var items = ((IList)surgeryDatas).Cast<SurgeryTherapyData>().ToList();
            int removecounter = 0;
            foreach (SurgeryTherapyData data in items)
            {
                if (data.Id == 0)
                {
                    SurgeryTherapyDatas.Remove(data);
                    removecounter++;
                }
            }
            OnPropertyChanged(nameof(AddedDatas));
            NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(3), "Удалено " + removecounter.ToString() + " элементов", true));
        }

        public void RaiseDataPropertyChanged()
        {
            OnPropertyChanged(nameof(AwaitCount));
        }
    }
}
