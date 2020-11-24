using Hospital.EntityFramework;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HospitalDbContext db = new HospitalDbContextFactory().CreateDbContext())
        }
    }
}
