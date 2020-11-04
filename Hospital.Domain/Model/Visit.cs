using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public class Visit : ModelBase
    {
        private Entry _entyIn;
        private Entry _entryOut;
        private Diagnosis _diagnosis;
        private string _conclusion;
        private string _recomendation;

        public ObservableCollection<TestData> TestDatas { get; set; }
        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; set; }
        public ObservableCollection<PhysioTherapyData> PhysioTherapyDatas { get; set; }
        public ObservableCollection<SurgencyTherapyData> SurgencyTherapyDatas { get; set; }

        public int Id { get; set; }
        public Entry EntryIn
        {
            get => _entyIn;
            set
            {
                _entyIn = value;
                OnPropertyChanged("EntryIn");
            }
        }
        public int EntryInId { get; set; }

        public Diagnosis Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }
        }
        //public int DiagnosisId { get; set; }
        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                OnPropertyChanged("Conclusion");
            }
        }
        public string Recomendation
        {
            get => _recomendation;
            set
            {
                _recomendation = value;
                OnPropertyChanged("Recomendation");
            }
        }

        public Entry EntryOut
        {
            get => _entryOut;
            set
            {
                _entryOut = value;
                OnPropertyChanged("EntryOut");
            }
        }
        public int? EntryOutId { get; set; }

    }
}
