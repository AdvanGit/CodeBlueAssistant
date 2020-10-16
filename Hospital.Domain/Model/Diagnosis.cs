
namespace Hospital.Domain.Model
{
    public class Diagnosis : ModelBase
    {
        private string _title;
        private string _code;
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
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
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
}
