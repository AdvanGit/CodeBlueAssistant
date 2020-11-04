using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum PhysTherStatus { Ожидание, Готов, Неявка }

    public class PhysioTherapyData : ModelBase
    {
        private Visit _visit;
        private PhysioTherapyFactor _physioTherapyFactor;
        private PhysTherMethod _physTherMethod;
        private string _value;
        private string _option;
        private Staff _staff;
        private DateTime _targetDateTime;
        private DateTime _createDateTime;
        private PhysTherStatus _physTherStatus;

        public int Id { get; set; }
        public Visit Visit { get => _visit; set { _visit = value; OnPropertyChanged("Visit"); } }
        public PhysioTherapyFactor PhysioTherapyFactor { get => _physioTherapyFactor; set { _physioTherapyFactor = value; OnPropertyChanged("PhysioTherapyFactor"); } }
        public PhysTherMethod PhysTherMethod { get => _physTherMethod; set { _physTherMethod = value; OnPropertyChanged("PhysTherMethod"); }    }
        public string Value { get => _value; set { _value = value; OnPropertyChanged("Value"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged("Option"); } }
        public Staff Staff { get => _staff; set { _staff = value; OnPropertyChanged("Staff"); } }
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }
        public PhysTherStatus PhysTherStatus { get => _physTherStatus; set { _physTherStatus = value; OnPropertyChanged("PhysTherStatus"); } }
    }

    public class PhysioTherapyFactor : ModelBase
    {
        private string _title;
        private string _fullTitle;
        private string _tool;
        private PhysTherFactGroup _physTherFactGroup;

        public int Id { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string FullTitle { get => _fullTitle; set { _fullTitle = value; OnPropertyChanged("FullTitle"); } }
        public string Tool { get => _tool; set { _tool = value; OnPropertyChanged("Tool"); } }
        public PhysTherFactGroup PhysTherFactGroup { get => _physTherFactGroup; set { _physTherFactGroup = value; OnPropertyChanged("PhysTherFactGroup"); } }
    }

    public class PhysTherFactGroup : ModelBase
    {
        private string _title;
        private string _fullTitle;

        public ObservableCollection<PhysioTherapyFactor> PhysioTherapyFactors { get; set; }
        public int Id { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string FullTitle { get => _fullTitle; set { _fullTitle = value; OnPropertyChanged("FullTitle"); } }
    }

    public class PhysTherMethod : ModelBase
    {
        private string _title;
        private string _fullTitle;
        private PhysTherMethodGroup _physTherMethodGroup;

        public int Id { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string FullTitle { get => _fullTitle; set { _fullTitle = value; OnPropertyChanged("FullTitle"); } }

        public PhysTherMethodGroup PhysTherMethodGroup { get => _physTherMethodGroup; set { _physTherMethodGroup = value; OnPropertyChanged("PhysTherMethodGroup"); } }
    }

    public class PhysTherMethodGroup : ModelBase
    {
        private string _title;
        private string _fullTitle;

        public ObservableCollection<PhysTherMethod> PhysTherMethods { get; set; }
        public int Id { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string FullTitle { get => _fullTitle; set { _fullTitle = value; OnPropertyChanged("FullTitle"); } }
    }

}
