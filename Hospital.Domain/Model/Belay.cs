
namespace Hospital.Domain.Model
{
    public class Belay : DomainObject
    {
        private string _title;
        private string _info;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }
    }
}
