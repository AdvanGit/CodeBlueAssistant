
namespace Hospital.Domain.Model
{
    public class Test : ModelBase
    {
        private string _title;
        private string _normal;
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
        public string Normal
        {
            get => _normal;
            set
            {
                _normal = value;
                OnPropertyChanged("Normal");
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
    }

    public class TestData : ModelBase
    {
        private Test _test;
        private string _detail;
        private string _value;
        private Presence _presence;
        private bool _isSymptom;

        public int Id { get; set; }
        public Test Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged("Test");
            }
        }
        public string Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                OnPropertyChanged("Detail");
            }
        }
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public Presence Presence { get => _presence; set { _presence = value; OnPropertyChanged("Presence"); } }

        public bool IsSymptom
        {
            get => _isSymptom;
            set
            {
                _isSymptom = value;
                OnPropertyChanged("IsSymptom");
            }
        }

    }
}
