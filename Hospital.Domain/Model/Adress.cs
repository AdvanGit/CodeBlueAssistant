
namespace Hospital.Domain.Model
{
    public class Adress : ModelBase
    {
        private string _city, _street;
        private int _number, _subNumber, _room;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        public int SubNumber
        {
            get => _subNumber;
            set
            {
                _subNumber = value;
                OnPropertyChanged("SubNumber");
            }
        }
        public int Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged("Room");
            }
        }
    }
}
