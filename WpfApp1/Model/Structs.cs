using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace WpfApp1.Model
{ 
    public struct Diagnosis : INotifyPropertyChanged
    {
        private string _description;
        private string _code;

        public string Code
        {
            get
            {  
                return _code;
            }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));}
    }

    public struct Checkup : INotifyPropertyChanged
    {
        private string _title;
        private string _normal;
        private string _normalFemale;
        private Department _department;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Name");
            }
        }
        public string Normal
        {
            get
            {
                return _normal;
            }
            set
            {
                _normal = value;
                OnPropertyChanged("Normal");
            }
        }
        public string NormalFemale
        {
            get
            {
                if (_normalFemale == null)
                {
                    _normalFemale = _normal;
                    return _normalFemale; 
                }
                else return _normalFemale;
            }
            set
            {
                _normalFemale = value;
                OnPropertyChanged("NormalFemale");
            }
        }
        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public struct Proc : INotifyPropertyChanged
    {
        private string _title;
        //private int _procId;
        private Department _department;

        public ObservableCollection<ProcOption> Options { get; set; }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public struct ProcOption : INotifyPropertyChanged
    {
        private string _code;
        private string _description;
        private string _measure;

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Measure
        {
            get
            {
                return _measure;
            }
            set
            {
                _measure = value;
                OnPropertyChanged("Measure");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public struct ProcOptionData : INotifyPropertyChanged
    {
        private ProcOption _procOption;
        private string _value;

        public ProcOption ProcOption
        {
            get
            {
                return _procOption;
            }
            set
            {
                _procOption = value;
                OnPropertyChanged("ProcOption");
            }
        }
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }

    }

    public class ProcQueue : INotifyPropertyChanged
    {
        public ProcQueue(Visit visit)
        {
            _visit = visit;
        }

        private enum status
        {
            Complete, Waiting
        }
        private Proc _procedure;
        private ProcOptionData _procOptionData;
        private Staff _staffOperation;
        private DateTime _createDateTime;
        private DateTime _resultDateTime;
        private Visit _visit;

        public Proc Procedure
        {
            get
            {
                return _procedure;
            }
            set
            {
                _procedure = value;
                OnPropertyChanged("Procedure");
            }
        }
        public ProcOptionData ProcOptionData
        {
            get
            {
                return _procOptionData;
            }
            set
            {
                _procOptionData = value;
                OnPropertyChanged("ProcOptionData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }

    }

    public class Department : INotifyPropertyChanged
    {
        private string _title;
        private Adress _adress;
        private Staff _manager;

        //private TimeSpan _time;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public Adress Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }
        public Staff Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}