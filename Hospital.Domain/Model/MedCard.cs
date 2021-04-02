using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public class MedCard : DomainObject
    {
        private Diagnosis _diagnosis;
        private Staff _diagnosisDoctor;
        private DateTime _diagnosisDate;
        private bool _isDifferential;
        private bool _isPreliminary;

        private Staff _therapyDoctor;
        private Staff _patient;
        private string _option;
        private string _conclusion;
        private string _recomendation;

        public ObservableCollection<TestData> TestDatas { get; set; }
        public ObservableCollection<PharmacoTherapyData> PharmacoTherapyDatas { get; set; }
        public ObservableCollection<PhysioTherapyData> PhysioTherapyDatas { get; set; }
        public ObservableCollection<SurgencyTherapyData> SurgencyTherapyDatas { get; set; }

        public string Conclusion { get => _conclusion; set { _conclusion = value; OnPropertyChanged("Conclusion"); }}
        public string Recomendation { get => _recomendation; set { _recomendation = value; OnPropertyChanged("Recomendation"); }}
        public string Option { get => _option; set { _option = value; OnPropertyChanged(nameof(Option)); } }

        public Diagnosis Diagnosis { get => _diagnosis; set { _diagnosis = value; OnPropertyChanged(nameof(Diagnosis)); }}
        public Staff DiagnosisDoctor { get => _diagnosisDoctor; set { _diagnosisDoctor = value; OnPropertyChanged(nameof(DiagnosisDoctor)); } }
        public DateTime DiagnosisDate { get => _diagnosisDate; set { _diagnosisDate = value; OnPropertyChanged(nameof(DiagnosisDate)); } }
        public Staff TherapyDoctor { get => _therapyDoctor; set { _therapyDoctor = value; OnPropertyChanged(nameof(TherapyDoctor));} }
        public Staff Patient { get => _patient; set { _patient = value; OnPropertyChanged(nameof(Patient)); } }
        public bool IsDifferential { get => _isDifferential; set { _isDifferential = value; OnPropertyChanged(nameof(IsDifferential)); } }
        public bool IsPreliminary { get => _isPreliminary; set { _isPreliminary = value; OnPropertyChanged(nameof(IsPreliminary)); } }


    }
}
