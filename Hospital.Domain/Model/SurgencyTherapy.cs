using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum SurgencyStatus { Ожидание, Готово, Неявка }
    public enum SurgencyClass { Лечебная, Радикальная, Паллативная, Симптоматическая, Диагностическая }
    public enum SurgencyPriority { Экстренная, Срочная, Плановая }

    public class SurgencyTherapyData : DomainObject
    {
        private MedCard _medCard;
        private SurgencyClass _surgencyClass;
        private SurgencyPriority _surgencyPriority;
        private SurgencyOperation _surgencyOperation;
        private SurgencyEndoscop _surgencyEndoscop;
        private SurgencyStatus _surgencyStatus;
        private DateTime _createDateTime;
        private DateTime _targetDateTime;
        private string _option;

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged("MedCard"); } }
        public SurgencyClass SurgencyClass { get => _surgencyClass; set { _surgencyClass = value; OnPropertyChanged("SurgencyClass"); } }
        public SurgencyPriority SurgencyPriority { get => _surgencyPriority; set { _surgencyPriority = value; OnPropertyChanged("SurgencyPriority"); } }
        public SurgencyOperation SurgencyOperation { get => _surgencyOperation; set { _surgencyOperation = value; OnPropertyChanged("SurgencyOperation"); } }
        public SurgencyEndoscop SurgencyEndoscop { get => _surgencyEndoscop; set { _surgencyEndoscop = value; OnPropertyChanged("SurgencyEndoscop"); } }
        public SurgencyStatus SurgencyStatus { get => _surgencyStatus; set { _surgencyStatus = value; OnPropertyChanged("SurgencyStatus"); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged("Option"); } }
    }

    public class SurgencyEndoscop : DomainObject
    {
        private string _title;
        private string _tool;

        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Tool { get => _tool; set { _tool = value; OnPropertyChanged("Tool"); } }
    }

    public class SurgencyOperation : DomainObject
    {
        private string _title;
        private string _fullTitle;
        private Department _department;
        private SurgencyGroup _surgencyGroup;

        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string FullTitle { get => _fullTitle; set { _fullTitle = value; OnPropertyChanged("FullTitle"); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
        public SurgencyGroup SurgencyGroup { get => _surgencyGroup; set { _surgencyGroup = value; OnPropertyChanged("SurgencyGroup"); } }
    }

    public class SurgencyGroup : DomainObject
    {
        private string _title;

        public ObservableCollection<SurgencyOperation> SurgencyOperations { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
    }
}
