using System;
using Hospital.EntityFramework;
using System.Collections.Generic;
using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hospital.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            //var change1 = new Change(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0),
            //                        new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 13, 0, 0),
            //                        TimeSpan.FromMinutes(20));
            //var change2 = new Change(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 13, 0, 0),
            //                        new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 18, 0, 0),
            //                        TimeSpan.FromMinutes(20));

            //foreach (DateTime time in change1.DateTimes)
            //{
            //    System.Console.WriteLine("t: {0:t}", time);
            //}
            //System.Console.WriteLine();

            //foreach (DateTime time in change2.DateTimes)
            //{
            //    System.Console.WriteLine("t: {0:t}", time);
            //}
            //System.Console.WriteLine();

            //using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
            //{
            //    List<Entry> entries = db.Entries.ToList();
            //    List<Entry> emptyEntries = new List<Entry>();

            //    foreach(DateTime time in change2.DateTimes)
            //    {
            //        emptyEntries.Add(new Entry { CreateDateTime = DateTime.Now, TargetDateTime = time });
            //    }

            //    emptyEntries.AddRange(entries);
            //    var result = emptyEntries.OrderBy(e => e.TargetDateTime).GroupBy(e => e.TargetDateTime).Select(e => e.Last());
            //    foreach (Entry entry in result) System.Console.WriteLine(entry.TargetDateTime + " " + entry.CreateDateTime);
            //}
            //System.Console.ReadKey();
        }
    }
}
