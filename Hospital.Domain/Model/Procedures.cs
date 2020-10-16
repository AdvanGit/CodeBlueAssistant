using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum ProcType { research, healing }
    public enum ProcStatus { Waiting , Complete}

    public class Proc : ModelBase
    {
        private string _title;
        private int _code;
        private ProcType _procType;
        private Department _department;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public ProcType ProcType
        {
            get => _procType;
            set
            {
                _procType = value;
                OnPropertyChanged("ProcType");
            }
        }
        public Department Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public ObservableCollection<ProcOption> Options { get; set; }
    }

    public class ProcOption : ModelBase
    {
        private Proc _proc;
        private string _title;
        private string _description;
        private int _code;
        private string _measure;
        private TimeSpan _timeSpan;
        private int _price;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public int Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public Proc Proc
        {
            get => _proc;
            set
            {
                _proc = value;
                OnPropertyChanged("Proc");
            }
        }
        public string Measure
        {
            get => _measure;
            set
            {
                _measure = value;
                OnPropertyChanged("Measure");
            }
        }
        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set
            {
                _timeSpan = value;
                OnPropertyChanged("TimeSpan");
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
    }

    public class ProcAsset : ModelBase
    {
        private Presence _presence;
        private ProcOption _procOption;
        private DateTime _createDate;
        private Staff _staffResult;
        private DateTime _resultDate;
        private string _resultValue;
        private ProcStatus _procStatus;
        private bool _isSymptom;
        
        public int Id { get; set; }
        public Presence Presence { get => _presence; set { _presence = value; OnPropertyChanged("Precense"); } }
        public ProcOption ProcOption { get => _procOption; set { _procOption = value; OnPropertyChanged("ProcOption"); } }
        public DateTime CreateDate { get => _createDate; set { _createDate = value; OnPropertyChanged("CreateDate"); } }
        public Staff StaffResult { get => _staffResult; set { _staffResult = value; OnPropertyChanged("StaffResult"); } }
        public DateTime ResultDate { get => _resultDate; set { _resultDate = value; OnPropertyChanged("ResultDate"); } }
        public string ResultValue { get => _resultValue; set { _resultValue = value; OnPropertyChanged("ResultValue"); } }
        public ProcStatus ProcStatus { get => _procStatus; set { _procStatus = value; OnPropertyChanged("ProcStatus"); } }
        public bool IsSymptom { get => _isSymptom; set { _isSymptom = value; OnPropertyChanged("IsSymptom"); } }
    }
}
