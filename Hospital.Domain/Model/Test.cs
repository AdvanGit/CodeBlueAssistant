using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum TestMethod { Физикальная, Лабараторная, Инструментальная }
    public enum TestStatus { Ожидание, Готов, Неявка }

    public class Test : DomainObject
    {
        private string _title;
        private string _shortTitle;
        private string _measure;
        private TestType _testType;

        public ObservableCollection<TestNormalValue> NormalValues { get; set; }

        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string ShortTitle { get => _shortTitle; set { _shortTitle = value; OnPropertyChanged("ShortTitle"); } }

        public string Measure { get => _measure; set { _measure = value; OnPropertyChanged("Measure"); } }
        public TestType TestType { get => _testType; set { _testType = value; OnPropertyChanged("TestType"); } }
    }

    public class TestType : DomainObject
    {
        private string _title;
        private TestMethod _testMethod;
        private Department _department;

        public ObservableCollection<Test> Tests { get; set; }

        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public TestMethod TestMethod { get => _testMethod; set { _testMethod = value; OnPropertyChanged("TestMethod"); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
    }

    public class TestNormalValue : DomainObject
    {
        private Test _test;
        private Gender _gender;
        private int _ageIn;
        private int _ageOut;
        private string _value;

        public Test Test { get => _test; set { _test = value; OnPropertyChanged("Test"); } }
        public Gender Gender { get => _gender; set { _gender = value; OnPropertyChanged("Gender"); } }
        public int AgeIn { get => _ageIn; set { _ageIn = value; OnPropertyChanged("AgeIn"); } }
        public int AgeOut { get => _ageOut; set { _ageOut = value; OnPropertyChanged("AgeOut"); } }
        public string Value { get => _value; set { _value = value; OnPropertyChanged("Value"); } }
    }

    public class TestData : DomainObject
    {
        private Visit _visit;
        private Test _test;
        private string _option;
        private string _value;

        private DateTime _dateCreate;
        private DateTime _dateResult;

        private Staff _staffResult;
        private TestStatus _status;
        private bool _isSymptom;

        public Visit Visit { get => _visit; set { _visit = value; OnPropertyChanged("Visit"); } }
        public Test Test { get => _test; set { _test = value; OnPropertyChanged("Test"); } }
        public string Option { get => _option; set { _option = value; OnPropertyChanged("Option"); } }
        public string Value { get => _value; set { _value = value; OnPropertyChanged("Value"); } }
        public DateTime DateCreate { get => _dateCreate; set { _dateCreate = value; OnPropertyChanged("DateCreate"); } }
        public DateTime DateResult { get => _dateResult; set { _dateResult = value; OnPropertyChanged("DateResult"); } }
        public Staff StaffResult { get => _staffResult; set { _staffResult = value; OnPropertyChanged("StaffResult"); } }
        public TestStatus Status { get => _status; set { _status = value; OnPropertyChanged("Status"); } }
        public bool IsSymptom { get => _isSymptom; set { _isSymptom = value; OnPropertyChanged("IsSymptom"); } }
    }
}
