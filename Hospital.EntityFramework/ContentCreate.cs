using Hospital.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.EntityFramework
{
    public class ContentCreate
    {
        public ContentCreate()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                List<Adress> adresses = new List<Adress>
                {
                    new Adress { City="Чайковский", Street="Декабристов", Number=5, SubNumber=3, Room=15},
                    new Adress { City="Чайковский", Street="Сосновая", Number=17, Room=55},
                    new Adress { City="Пермь", Street="Ленина", Number=48, Room=1},
                    new Adress { City="Москва", Street="Проспект Кожевникова", Number=7}
                };
                List<Belay> belays = new List<Belay>
                {
                    new Belay {Title="Росгосстрах-медицина"},
                    new Belay {Title="СОГАЗ-Мед"},
                    new Belay {Title="ВТБ МС"},
                    new Belay {Title="МАКС-М"},
                    new Belay {Title="АльфаСтрахование-ОМС"}
                };
                List<Patient> patients = new List<Patient>
                {
                    new Patient { FirstName = "Очень", MidName = "Больной", LastName = "Человек", Gender = Gender.female, HasChild = true, Belay = belays.ElementAt(0), BelayCode = 12345678, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Patient { FirstName = "Очень", MidName = "Твердая", LastName = "Воля", Gender = Gender.female, Belay = belays.ElementAt(1), BelayCode=88888888, Adress=adresses.ElementAt(new Random().Next(adresses.Count))}

                };
                List<Staff> staffs = new List<Staff>
                {
                    new Staff { FirstName = "Ресепшен", MidName = "Вашу", LastName = "Мать",  Gender=Gender.female, Password="123", PhoneNumeber=89991231190, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Staff { FirstName = "Доктор", MidName = "Соколов", LastName = "Премудрый", Password="123", PhoneNumeber=89223348043, Adress=adresses.ElementAt(new Random().Next(adresses.Count))}
                };

                db.Belays.AddRange(belays);
                db.Patients.AddRange(patients);
                db.Staffs.AddRange(staffs);
                db.SaveChanges();
            }

        }
    }
}
