using System.Collections.Generic;

namespace Hospital.Domain.Model
{
    public class Diagnosis : DomainObject
    {
        private string _caption;
        private string _title;
        private string _code;
        private string _description;
        private Department _department;
        private DiagnosisGroup _diagnosisGroup;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Code { get => _code; set { _code = value; OnPropertyChanged("Code"); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
        public DiagnosisGroup DiagnosisGroup { get => _diagnosisGroup; set { _diagnosisGroup = value; OnPropertyChanged(nameof(DiagnosisGroup)); } }
    }

    public class DiagnosisClass : DomainObject
    {
        private string _caption;
        private string _title;
        private string _description;
        private string _code;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public string Code { get => _code; set { _code = value; OnPropertyChanged(nameof(Code)); } }

        public ICollection<DiagnosisGroup> DiagnosisGroups { get; set; }
    }

    public class DiagnosisGroup : DomainObject
    {
        private string _caption;
        private string _title;
        private string _description;
        private DiagnosisClass _diagnosisClass;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        public DiagnosisClass DiagnosisClass { get => _diagnosisClass; set { _diagnosisClass = value; OnPropertyChanged(nameof(DiagnosisClass)); } }

        public ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
