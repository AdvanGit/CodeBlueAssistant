using System;

namespace Hospital.Domain.Model
{
    public enum Treatment { Этиотропная, Патогенетическая, Симптоматическая, Заместительная, Профилактическая }

    public interface ITherapyData
    {
        public string Title { get; }
        public string Option { get; set; }
        public string Value { get; set; }
        public string Group { get; }
        public string Type { get; }
        public DateTime Entry { get; }
    }

    public class TherapyBase : DomainObject
    {
        private Staff _therapyDoctor;
        private Diagnosis _diagnosis;
        private DateTime _diagnosisDate;
        private Staff _diagnosisDoctor;

        public Staff TherapyDoctor { get => _therapyDoctor; set { _therapyDoctor = value; OnPropertyChanged(nameof(TherapyDoctor)); } }
        public DateTime DiagnosisDate { get => _diagnosisDate; set { _diagnosisDate = value; OnPropertyChanged(nameof(DiagnosisDate)); } }
        public Staff DiagnosisDoctor { get => _diagnosisDoctor; set { _diagnosisDoctor = value; OnPropertyChanged(nameof(DiagnosisDoctor)); } }
        public Diagnosis Diagnosis { get => _diagnosis; set { _diagnosis = value; OnPropertyChanged(nameof(Diagnosis)); } }
    }
}
