using System;
using System.Collections.Generic;

namespace Hospital.Domain.Model
{
    public class Change : DomainObject
    {
        public Change(DateTime dateTimeStart, DateTime dateTimeEnd, TimeSpan timeSpan)
        {
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
            TimeSpan = timeSpan;
        }

        private Staff _staff;
        private DateTime _dateTimeStart;
        private DateTime _dateTimeEnd;
        private TimeSpan _timeSpan;

        public Staff Staff { get => _staff; set { _staff = value; OnPropertyChanged(nameof(Staff)); } }
        public DateTime DateTimeStart { get => _dateTimeStart; set { _dateTimeStart = value; OnPropertyChanged(nameof(DateTimeStart)); } }
        public DateTime DateTimeEnd { get => _dateTimeEnd; set { _dateTimeEnd = value; OnPropertyChanged(nameof(DateTimeEnd)); } }
        public TimeSpan TimeSpan { get => _timeSpan; set { _timeSpan = value; OnPropertyChanged(nameof(TimeSpan)); } }

        public List<DateTime> GetTimes()
        {
            var dateTimes = new List<DateTime>();
            for (var _timeStart = _dateTimeStart; _timeStart < _dateTimeEnd; _timeStart += _timeSpan)
            {
                dateTimes.Add(_timeStart);
            }
            return dateTimes;
        }
    }
}
