using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum ChangeTitle : byte { Первая, Вторая, Вечерняя, Ночная}
    public enum DepartmentType : byte {Ambulatory = 0, Stationary = 1, Laboratory = 2, Reception = 3}

    public class Department : DomainObject
    {
        private DepartmentTitle _title;
        private Staff _manager;
        private DepartmentType _type;

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
        public ObservableCollection<Diagnosis> Diagnoses { get; set; }
    }

    public class DepartmentTitle : DomainObject
    {
        private string _title;
        private string _code;
        private string _shortTitle;

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
    }
}
