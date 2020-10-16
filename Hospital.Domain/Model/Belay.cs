
namespace Hospital.Domain.Model
{
    public class Belay : ModelBase
    {
        private string _title;
        private string _info;

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
