using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public enum SurgencyStatus { Ожидание, Готово, Неявка }
    public enum SurgencyClass { Лечебная, Радикальная, Паллативная, Симптоматическая, Диагностическая }
    public enum SurgencyPriority { Плановая, Экстренная, Срочная }
    public enum SurgencyType { Малоинвазивная, Оперативная }

    public class SurgencyTherapyData : TherapyBase, ITherapyData
    {
        private MedCard _medCard;
        private SurgencyClass _surgencyClass;
        private SurgencyPriority _surgencyPriority;
        private SurgencyOperation _surgencyOperation;
        private SurgencyStatus _surgencyStatus;
        private DateTime _createDateTime;
        private DateTime _targetDateTime;
        private string _option;

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }
        public SurgencyClass SurgencyClass { get => _surgencyClass; set { _surgencyClass = value; OnPropertyChanged("SurgencyClass"); } }
        public SurgencyPriority SurgencyPriority { get => _surgencyPriority; set { _surgencyPriority = value; OnPropertyChanged("SurgencyPriority"); } }
        public SurgencyOperation SurgencyOperation { get => _surgencyOperation; set { _surgencyOperation = value; OnPropertyChanged("SurgencyOperation"); } }
        public SurgencyStatus SurgencyStatus { get => _surgencyStatus; set { _surgencyStatus = value; OnPropertyChanged("SurgencyStatus"); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged("Option"); } }

        [NotMapped]
        public string Title => SurgencyOperation.Title;
        [NotMapped]
        public string Group => SurgencyOperation.SurgencyGroup.Title;
        [NotMapped]
        public string Value { get => "не доступно"; set { } }
        [NotMapped]
        public DateTime Entry => TargetDateTime;
        [NotMapped]
        public string Type => "Хирургия";
    }

    public class SurgencyOperation : DomainObject
    {
        private string _caption;
        private string _title;
        private Department _department;
        private SurgencyGroup _surgencyGroup;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
        public SurgencyGroup SurgencyGroup { get => _surgencyGroup; set { _surgencyGroup = value; OnPropertyChanged("SurgencyGroup"); } }
    }

    public class SurgencyGroup : DomainObject
    {
        private string _title;
        private SurgencyType _surgencyType;

        public ObservableCollection<SurgencyOperation> SurgencyOperations { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public SurgencyType SurgencyType { get => _surgencyType; set { _surgencyType = value; OnPropertyChanged(nameof(SurgencyType)); } }
    }
}
