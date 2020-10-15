using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hospital.Domain.Model
{
    public enum ChangeTitle : byte { Первая, Вторая, Вечерняя, Ночная}
    public enum DepartmentType : byte {Ambulatory, Stationary, Laboratory}

    public class Department : INotifyPropertyChanged
    {
        private DepartmentTitle _title;
        private Staff _manager;
        private DepartmentType _type;

        public int Id { get; set; }
        public DepartmentTitle Title
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
        public DepartmentType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
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

        public string _Adress { get; set; }
        public Adress Adress
        {
            get { return _Adress == null ? null : JsonConvert.DeserializeObject<Adress>(_Adress); }
            set { _Adress = JsonConvert.SerializeObject(value); }
        }

        public ObservableCollection<Change> Changes { get; set; }
        public ObservableCollection<Staff> Staffs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class Change : INotifyPropertyChanged
    {
        private Department _department;
        private ChangeTitle _changeTitle;
        private DateTime _timeStart;
        private TimeSpan _timeSpan;

        public int Id { get; set; }
        public Department Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }
        public ChangeTitle ChangeTitle
        {
            get => _changeTitle;
            set
            {
                _changeTitle = value;
                OnPropertyChanged("ChangeTitle");
            }
        }
        public DateTime TimeStart
        {
            get => _timeStart;
            set
            {
                _timeStart = value;
                OnPropertyChanged("TimeStart");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class DepartmentTitle : INotifyPropertyChanged
    {
        private string _title;
        private string _code;
        private string _shortTitle;

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
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public string ShortTitle
        {
            get => _shortTitle;
            set
            {
                _shortTitle = value;
                OnPropertyChanged("ShortTitle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
