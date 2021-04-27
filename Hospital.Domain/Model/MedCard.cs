using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public class MedCard : TherapyBase
    {
        private bool _isDifferential;
        private bool _isPreliminary;
        private bool _isCured;

        private string _option;
        private string _conclusion;
        private string _recomendation;

        private Staff _patient;

        public ObservableCollection<TestData> TestDatas { get; set; }
        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; set; }
        public ObservableCollection<PhysioTherapyData> PhysioTherapyDatas { get; set; }
        public ObservableCollection<SurgencyTherapyData> SurgencyTherapyDatas { get; set; }

        public bool IsDifferential { get => _isDifferential; set { _isDifferential = value; OnPropertyChanged(nameof(IsDifferential)); } }
        public bool IsPreliminary { get => _isPreliminary; set { _isPreliminary = value; OnPropertyChanged(nameof(IsPreliminary)); } }
        public bool IsCured { get => _isCured; set { _isCured = value; OnPropertyChanged(nameof(IsCured)); } }

        public string Conclusion { get => _conclusion; set { _conclusion = value; OnPropertyChanged("Conclusion"); } }
        public string Recomendation { get => _recomendation; set { _recomendation = value; OnPropertyChanged("Recomendation"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged(nameof(Option)); } }

        public Staff Patient { get => _patient; set { _patient = value; OnPropertyChanged(nameof(Patient)); } }
    }
}
