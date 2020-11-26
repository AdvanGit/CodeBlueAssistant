using System;
using System.Collections.Generic;
using Hospital.Domain.Model;


namespace Hospital.Console
{
    class Program
    {

        static void Main(string[] args)
        {

            var schedule = new Schedule();
            var dateTimes = new List<DateTime>();
            dateTimes = schedule.GetTimesByDate(DateTime.Now + TimeSpan.FromDays(1));

            foreach (DateTime time in dateTimes)
            {
            System.Console.WriteLine("t: {0:t}", time);
            }

            System.Console.ReadKey();
        }
    }
}
