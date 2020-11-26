using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Model
{
    public class Schedule
    {
        //private Staff _staff;
        private TimeSpan span = TimeSpan.FromMinutes(30);
        
        public List<DateTime> GetTimesByDate(DateTime date)
        {
            DateTime timeStart = GetTimeStart(date);
            var timeEnd = timeStart + TimeSpan.FromHours(4);
            List<DateTime> times = new List<DateTime>();
            for (var _timeStart = timeStart; _timeStart < timeEnd; _timeStart+=span)
            {
                times.Add(_timeStart);
            }

            return times;
        }

        private DateTime GetTimeStart(DateTime date)
        {
            if ((date.Day % 2) == 0)
            {
                var startDateTime = new DateTime(date.Hour,date.Month,date.Day,9,0,0);
                return startDateTime;
            }
            else
            {
                var startDateTime = new DateTime(date.Hour, date.Month, date.Day, 13, 0, 0);
                return startDateTime;
            }
        }
        
    }
}
