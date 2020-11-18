using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public class MedCard : DomainObject
    {
        private Diagnosis _diagnosis;
        private Staff _doctor;
        private Staff _patient;
        private string _option;
        private string _conclusion;
        private string _recomendation;

        public ObservableCollection<TestData> TestDatas { get; set; }
        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; set; }
        public ObservableCollection<PhysioTherapyData> PhysioTherapyDatas { get; set; }
        public ObservableCollection<SurgencyTherapyData> SurgencyTherapyDatas { get; set; }

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
        public string Option { get => _option; set { _option = value; OnPropertyChanged(nameof(Option)); } }

        public Diagnosis Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }
        }
        public Staff Doctor { get => _doctor; set { _doctor = value; OnPropertyChanged(nameof(Doctor));} }
        public Staff Patient { get => _patient; set { _patient = value; OnPropertyChanged(nameof(Patient)); } }

    }
}
