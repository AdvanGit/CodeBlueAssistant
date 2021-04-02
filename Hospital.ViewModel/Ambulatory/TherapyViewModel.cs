using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hospital.ViewModel.Ambulatory
{
    public class TherapyViewModel : MainViewModel
    {
        private readonly AmbulatoryDataService ambulatoryDataService = new AmbulatoryDataService(new HospitalDbContextFactory());
        private readonly GenericDataServices<Diagnosis> dataServicesDiagnosis = new GenericDataServices<Diagnosis>(new HospitalDbContextFactory());
        private readonly GenericDataServices<DrugSubClass> dataServicesDrugSubClass = new GenericDataServices<DrugSubClass>(new HospitalDbContextFactory());
        private readonly GenericDataServices<DrugGroup> dataServicesDrugGroup = new GenericDataServices<DrugGroup>(new HospitalDbContextFactory());

        private string _diagnosisSearchValue, _drugSearchValue;

        private PharmacoTherapyData _pharmacoData = new PharmacoTherapyData();
        private Entry _currentEntry;

        private DrugClass _currentDrugClass;
        private DrugSubClass _currentDrugSubClass;
        private DrugGroup _currentDrugGroup;

        private async void Initialize(Entry entry)
        {
            if (entry != null && entry.MedCardId != null)
            {
                var res = await ambulatoryDataService.GetTestData(entry.MedCard.Id, true);
                foreach (TestData data in res) TestDatas.Add(data);
                var drugsClasses = await new GenericDataServices<DrugClass>(new HospitalDbContextFactory()).GetAll();
                foreach (DrugClass drugClass in drugsClasses) DrugClasses.Add(drugClass);
                var pharmacoDatas = await ambulatoryDataService.GetPharmacoTherapyDatas(entry.MedCard.Id);
                foreach (PharmacoTherapyData pharmacoTherapyData in pharmacoDatas) PharmacoTherapyDatas.Add(pharmacoTherapyData);

                PharmacoData.MedCard = entry.MedCard;
                PharmacoData.TherapyDoctor = entry.DoctorDestination;
                Diagnoses.Add(entry.MedCard.Diagnosis);
            }
        }

        private async void SearchDiagnoses(string value)
        {
            if (value.Length > 2)
            {
                if (CurrentDiagnosis == null)
                {
                    Diagnoses.Clear();
                    var result = await dataServicesDiagnosis.GetWhere(d => d.Title.Contains(value));
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
                    DrugSearchList.Clear();
                    var result = await ambulatoryDataService.GetDrugs(value);
                    foreach (Drug drug in result)
                    {
                        DrugSearchList.Add(drug);
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


        public TherapyViewModel(Entry entry)
        {
            _currentEntry = entry;
            Initialize(entry);
        }

        public ObservableCollection<TestData> TestDatas { get; } = new ObservableCollection<TestData>();
        public ObservableCollection<Diagnosis> Diagnoses { get; } = new ObservableCollection<Diagnosis>();

        public ObservableCollection<DrugClass> DrugClasses { get; } = new ObservableCollection<DrugClass>();
        public ObservableCollection<DrugSubClass> DrugSubClasses { get; } = new ObservableCollection<DrugSubClass>();
        public ObservableCollection<DrugGroup> DrugGroups { get; } = new ObservableCollection<DrugGroup>();
        public ObservableCollection<DrugSubGroup> DrugSubGroups { get; } = new ObservableCollection<DrugSubGroup>();
        public ObservableCollection<Drug> Drugs { get; } = new ObservableCollection<Drug>();
        public ObservableCollection<Drug> DrugSearchList { get; } = new ObservableCollection<Drug>();

        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; } = new ObservableCollection<PharmacoTherapyData>();
        public PharmacoTherapyData PharmacoData { get => _pharmacoData; set { _pharmacoData = value; OnPropertyChanged(nameof(PharmacoData)); } }
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

        public string DiagnosisSearchValue
        {
            get => _diagnosisSearchValue;
            set
            {
                _diagnosisSearchValue = value;
                OnPropertyChanged(nameof(DiagnosisSearchValue));
                SearchDiagnoses(value);
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
        }
        public void RemovePharmacoTherapyData(object testDatas)
        {
            var items = ((System.Collections.IList)testDatas).Cast<PharmacoTherapyData>().ToList();
            int removecounter = 0;
            //if (items.Count != 0)
            foreach (PharmacoTherapyData data in items)
            {
                if (data.Id == 0)
                {
                    PharmacoTherapyDatas.Remove(data);
                    removecounter++;
                }
            }
            NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(5), "Удалено " + removecounter.ToString() + " элементов", true));
        }
    }
}
