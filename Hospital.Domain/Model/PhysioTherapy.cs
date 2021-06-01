using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public class PhysioTherapyData : TherapyBase, ITherapyData
    {
        private MedCard _medCard;
        private PhysioTherapyFactor _physioTherapyFactor;
        private PhysTherMethod _physTherMethod; //not use
        private string _localization;
        private string _params;
        private DateTime _targetDateTime;
        private DateTime _createDateTime;
        private ProcedureStatus _procedureStatus;
        private Staff _operationDoctor;
        private Treatment _treatment;
        private TimeSpan _duration;
        private TimeSpan _remainingTime;

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }
        public PhysioTherapyFactor PhysioTherapyFactor { get => _physioTherapyFactor; set { _physioTherapyFactor = value; OnPropertyChanged("PhysioTherapyFactor"); } }
        public PhysTherMethod PhysTherMethod { get => _physTherMethod; set { _physTherMethod = value; OnPropertyChanged("PhysTherMethod"); } }
        public string Localization { get => _localization; set { _localization = value; OnPropertyChanged(nameof(Localization)); OnPropertyChanged(nameof(Option)); } }
        public string Params { get => _params; set { _params = value; OnPropertyChanged(nameof(Params)); OnPropertyChanged(nameof(Value)); } }
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }
        public ProcedureStatus ProcedureStatus { get => _procedureStatus; set { _procedureStatus = value; OnPropertyChanged("PhysTherStatus"); } }
        public Staff OperationDoctor { get => _operationDoctor; set { _operationDoctor = value; OnPropertyChanged(nameof(OperationDoctor)); } }
        public Treatment Treatment { get => _treatment; set { _treatment = value; OnPropertyChanged(nameof(Treatment)); } }
        public TimeSpan Duration { get => _duration; set { _duration = value; OnPropertyChanged(nameof(Duration)); } }
        public TimeSpan RemainingTime { get => _remainingTime; set { _remainingTime = value; OnPropertyChanged(nameof(RemainingTime)); OnPropertyChanged(nameof(Value)); } }

        [NotMapped]
        public string Title => PhysioTherapyFactor.Caption;
        [NotMapped]
        public string Group => PhysioTherapyFactor.PhysTherFactGroup.Caption;
        [NotMapped]
        public DateTime Entry => TargetDateTime;
        [NotMapped]
        public string Value { get => RemainingTime.ToString(); set { TimeSpan.TryParse(value, out _remainingTime); OnPropertyChanged(nameof(RemainingTime)); } }
        [NotMapped]
        public string Option { get => Localization; set { Localization = value; OnPropertyChanged(nameof(Option)); } }
        [NotMapped]
        public string Type => "Физиотерапия";
    }

    public class PhysioTherapyFactor : DomainObject
    {
        private string _caption;
        private string _title;
        private string _tool;
        private PhysTherFactGroup _physTherFactGroup;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public string Tool { get => _tool; set { _tool = value; OnPropertyChanged("Tool"); } }
        public PhysTherFactGroup PhysTherFactGroup { get => _physTherFactGroup; set { _physTherFactGroup = value; OnPropertyChanged("PhysTherFactGroup"); } }
    }

    public class PhysTherFactGroup : DomainObject
    {
        private string _caption;
        private string _title;

        public ObservableCollection<PhysioTherapyFactor> PhysioTherapyFactors { get; set; }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
    }

    public class PhysTherMethod : DomainObject
    {
        private string _caption;
        private string _title;
        private PhysTherMethodGroup _physTherMethodGroup;

        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }

        public PhysTherMethodGroup PhysTherMethodGroup { get => _physTherMethodGroup; set { _physTherMethodGroup = value; OnPropertyChanged("PhysTherMethodGroup"); } }
    }

    public class PhysTherMethodGroup : DomainObject
    {
        private string _caption;
        private string _title;

        public ObservableCollection<PhysTherMethod> PhysTherMethods { get; set; }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged(nameof(Caption)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
    }

}
