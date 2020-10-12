using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Context;
using WpfApp1.Model;

namespace WpfApp1.Data
{
    public class DataCreate
    {
        public DataCreate()
        {
            using (PersonContext db = new PersonContext())
            {
                List<Patient> patients = new List<Patient>
                {
                    new Patient { FirstName = "Очень", MidName = "Больной", LastName = "Человек", Gender = Gender.female, HasChild = true },
                    new Patient { FirstName = "Очень", MidName = "Твердая", LastName = "Воля", Gender = Gender.female,  }

                };
                List<Staff> staffs = new List<Staff>
                {
                    new Staff { FirstName = "Ресепшен", MidName = "Вашу", LastName = "Мать",  Gender=Gender.female, Password="123", PhoneNumeber=89341 },
                    new Staff { FirstName = "Доктор", MidName = "Соколов", LastName = "Премудрый", }
                };

                db.Patients.AddRange(patients);
                db.Staffs.AddRange(staffs);
                db.SaveChanges();
            }
        }
    }
}

