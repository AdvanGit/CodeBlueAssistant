using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public enum SurgeryStatus { Ожидание, Готово, Неявка }
    public enum SurgeryClass { Лечебная, Радикальная, Паллативная, Симптоматическая, Диагностическая }
    public enum SurgeryPriority { Плановая, Экстренная, Срочная }
    public enum SurgeryType { Малоинвазивная, Оперативная }

    public class SurgeryTherapyData : TherapyBase, ITherapyData
    {
        private MedCard _medCard;
        private SurgeryClass _surgeryClass;
        private SurgeryPriority _surgeryPriority;
        private SurgeryOperation _surgeryOperation;
        private SurgeryStatus _surgeryStatus;
        private DateTime _createDateTime;
        private DateTime _targetDateTime;
        private string _option;

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }
        public SurgeryClass SurgeryClass { get => _surgeryClass; set { _surgeryClass = value; OnPropertyChanged(nameof(Model.SurgeryClass)); } }
        public SurgeryPriority SurgeryPriority { get => _surgeryPriority; set { _surgeryPriority = value; OnPropertyChanged(nameof(SurgeryPriority)); } }
        public SurgeryOperation SurgeryOperation { get => _surgeryOperation; set { _surgeryOperation = value; OnPropertyChanged(nameof(SurgeryOperation)); } }
        public SurgeryStatus SurgeryStatus { get => _surgeryStatus; set { _surgeryStatus = value; OnPropertyChanged(nameof(SurgeryStatus)); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged("Option"); } }

        [NotMapped]
        public string Title => SurgeryOperation.Title;
        [NotMapped]
        public string Group => SurgeryOperation.SurgeryGroup.Title;
        [NotMapped]
        public string Value { get => "не доступно"; set { } }
        [NotMapped]
        public DateTime Entry => TargetDateTime;
        [NotMapped]
        public string Type => "Хирургия";
    }

    public class SurgeryOperation : DomainObject
    {
        private string _caption;
        private string _title;
        private Department _department;
        private SurgeryGroup _surgeryGroup;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
        public SurgeryGroup SurgeryGroup { get => _surgeryGroup; set { _surgeryGroup = value; OnPropertyChanged(nameof(SurgeryGroup)); } }
    }

    public class SurgeryGroup : DomainObject
    {
        private string _title;
        private SurgeryType _surgeryType;

        public ObservableCollection<SurgeryOperation> SurgeryOperations { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public SurgeryType SurgeryType { get => _surgeryType; set { _surgeryType = value; OnPropertyChanged(nameof(SurgeryType)); } }
    }
}
