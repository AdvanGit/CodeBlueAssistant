using System;

namespace Hospital.Domain.Model
{
    public class Schedule : ModelBase
    {
        private Staff _doctor;
        private DateTime _targetTime;
        private Entry _entry;

        public int Id { get; set; }
        public Staff Doctor
        {
            get => _doctor;
            set
            {
                _doctor = value;
                OnPropertyChanged("Staff");
            }
        }
        public DateTime TargetTime
        {
            get => _targetTime;
            set
            {
                _targetTime = value;
                OnPropertyChanged("TargetTime");
            }
        }
        public Entry Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged("Entry");
            }
        }
    }
}
