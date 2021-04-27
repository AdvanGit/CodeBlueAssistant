using Hospital.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.EntityFramework
{
    public class ContentCreate
    {
        private readonly HospitalDbContextFactory _contextFactory;
        public ContentCreate(HospitalDbContextFactory contextFactory)
        {

            _contextFactory = contextFactory;
            using (HospitalDbContext db = _contextFactory.CreateDbContext())
            {
                List<DepartmentTitle> departmentTitles = new List<DepartmentTitle>
                {
                    new DepartmentTitle{Title="Общая терапия", Code="Therapy"},
                    new DepartmentTitle{Title = "Стоматология", Code="Stomatology"},
                    new DepartmentTitle{Title = "Станция переливания крови", Code="Transfusiology"},
                    new DepartmentTitle{Title = "Интенсивная терапия", Code="ICU"},
                };
                List<Adress> adresses = new List<Adress>
                {
                    new Adress { City="Чайковский", Street="Декабристов", Number="5", Room=15},
                    new Adress { City="Чайковский", Street="Сосновая", Number="17", Room=55},
                    new Adress { City="Пермь", Street="Ленина", Number = "35", Room=13 },
                    new Adress { City="Москва", Street="Проспект Кожевникова", Number="12"}
                };
                List<Department> departments = new List<Department>
                {
                    new Department { Title=departmentTitles.ElementAt(0), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Ambulatory },
                    new Department { Title=departmentTitles.ElementAt(1), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Stationary},
                    new Department { Title=departmentTitles.ElementAt(2), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Laboratory},
                    new Department { Title=departmentTitles.ElementAt(3), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Ambulatory},
                    new Department { Title=departmentTitles.ElementAt(3), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Stationary},
                };
                List<Belay> belays = new List<Belay>
                {
                    new Belay {Title="Росгосстрах-медицина"},
                    new Belay {Title="СОГАЗ-Мед"},
                    new Belay {Title="ВТБ МС"},
                    new Belay {Title="МАКС-М"},
                    new Belay {Title="АльфаСтрахование-ОМС"}
                };
                List<Staff> staffs = new List<Staff>
                {
                    new Staff { FirstName = "Авдыкова", MidName = "Людмила", LastName = "Захаровна", Department= departments.ElementAt(new Random().Next(departments.Count)),  Gender=Gender.female, Password="123", PhoneNumber=89991231190, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Staff { FirstName = "Ситников", MidName = "Анатолий", LastName = "Радимович", Department= departments.ElementAt(new Random().Next(departments.Count)),  Gender=Gender.male, Password="123", PhoneNumber=89223348043, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Staff { FirstName = "Павлов", MidName = "Михаил", LastName = "Николаевич", Department= departments.ElementAt(new Random().Next(departments.Count)),  Gender=Gender.male, Password="123", PhoneNumber=89223348043, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Staff { FirstName = "Жуков", MidName = "Терентий", LastName = "Георгиевич", Department= departments.ElementAt(new Random().Next(departments.Count)),  Gender=Gender.male, Password="123", PhoneNumber=89223348043, Adress=adresses.ElementAt(new Random().Next(adresses.Count))}
                };
                List<Patient> patients = new List<Patient>
                {
                    new Patient { FirstName = "Очень", MidName = "Больной", LastName = "Человек", Gender = Gender.female, HasChild = true, Belay = belays.ElementAt(0), BelayCode = 12345678, Adress=adresses.ElementAt(new Random().Next(adresses.Count))},
                    new Patient { FirstName = "Очень", MidName = "Твердая", LastName = "Воля", Gender = Gender.female, Belay = belays.ElementAt(1), BelayCode=88888888, Adress=adresses.ElementAt(new Random().Next(adresses.Count))}

                };
                List<Change> changes = new List<Change>
                {
                    new Change(new DateTime(2020,11,26,8,0,0),new DateTime(2020,11,26,13,0,0), TimeSpan.FromMinutes(30)) { Staff = staffs.ElementAt(0) },
                    new Change(new DateTime(2020,11,27,13,0,0),new DateTime(2020,11,27,18,0,0), TimeSpan.FromMinutes(30)){ Staff = staffs.ElementAt(0) },
                    new Change(new DateTime(2020,11,28,8,0,0),new DateTime(2020,11,28,13,0,0), TimeSpan.FromMinutes(30)) { Staff = staffs.ElementAt(0) },
                    new Change(new DateTime(2020,11,28,13,0,0),new DateTime(2020,11,28,18,0,0), TimeSpan.FromMinutes(30)) { Staff = staffs.ElementAt(0) },
                };

                List<MedCard> medCards = new List<MedCard>
                {
                    new MedCard { TherapyDoctor=staffs.ElementAt(new Random().Next(staffs.Count) )}
                };

                List<Entry> entries = new List<Entry>
                {
                    new Entry {MedCard=medCards.FirstOrDefault(), Registrator=staffs.ElementAt(new Random().Next(staffs.Count)), DoctorDestination=staffs.ElementAt(0), EntryStatus=EntryStatus.Открыта, Patient=patients.ElementAt(new Random().Next(patients.Count)), TargetDateTime=new DateTime(2020,11,26,10,0,0)},
                    new Entry {Registrator=staffs.ElementAt(new Random().Next(staffs.Count)), DoctorDestination=staffs.ElementAt(0), EntryStatus=EntryStatus.Открыта, Patient=patients.ElementAt(new Random().Next(patients.Count)), TargetDateTime=new DateTime(2020,11,26,11,0,0)},
                    new Entry {Registrator=staffs.ElementAt(new Random().Next(staffs.Count)), DoctorDestination=staffs.ElementAt(0), EntryStatus=EntryStatus.Открыта, Patient=patients.ElementAt(new Random().Next(patients.Count)), TargetDateTime=new DateTime(2020,11,26,11,30,0)}
                };

                List<TestType> testTypes = new List<TestType>
                {
                    new TestType { Title="Первичное обследование", TestMethod=TestMethod.Физикальная},
                    new TestType { Title="Клинический анализ крови", TestMethod=TestMethod.Лабараторная},
                    new TestType { Title="Биохимический анализ крови", TestMethod=TestMethod.Лабараторная},
                    new TestType { Title="Общий анализ мочи", TestMethod=TestMethod.Лабараторная},
                    new TestType { Title="Рентгенография", TestMethod=TestMethod.Инструментальная},
                    new TestType { Title="Функциональная диагностика", TestMethod=TestMethod.Инструментальная},
                    new TestType { Title="КТ", TestMethod=TestMethod.Инструментальная},
                    new TestType { Title="МРТ", TestMethod=TestMethod.Инструментальная},
                    new TestType { Title="УЗИ", TestMethod=TestMethod.Инструментальная}
                };
                List<Test> tests = new List<Test>
                {
                    new Test { Title="Аудиометрия", ShortTitle="Аудиометр", Measure="Дб", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Аускультация", ShortTitle="Аускульт", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Пальпация", ShortTitle="Пальпация", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Перкуссия", ShortTitle="Перкуссия", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Давление", ShortTitle="Давление", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Пульс", ShortTitle="Пульс", TestType=testTypes.ElementAt(0)},

                    new Test {Title="Гемоглобин", ShortTitle="Гемогл", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Гематокрит", ShortTitle="Гематокр", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Эритроциты", ShortTitle="Эритроц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Объем Эритроцита", ShortTitle="Об.Эритроц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Скорость оседания Эритроцита", ShortTitle="Ск.Осед.Эрит", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Лейкоциты", ShortTitle="Лейкоц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Лейкоцтарная Формула", ShortTitle="Лейк.Форм", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Тромбоциты", ShortTitle="Тромбоц", TestType=testTypes.ElementAt(1)},

                    new Test {Title="Адренналин", ShortTitle="Адреннал", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Аммиак", ShortTitle="Аммиак", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Остаточный Азот", ShortTitle="Азот.Остат", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Альбумин", ShortTitle="Альбумин", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Ацетон", ShortTitle="Ацетон", TestType=testTypes.ElementAt(2)},
                    new Test {Title="цАМФ", ShortTitle="цАМФ", TestType=testTypes.ElementAt(2)},
                    new Test {Title="АльфаГлобулин", ShortTitle="Альф.Глоб", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Билирубин", ShortTitle="блРубин", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Витамин А", ShortTitle="вит.А", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Витамин В1", ShortTitle="вит.В1", TestType=testTypes.ElementAt(2)},

                    new Test {Title="Плотность Утренней Порции", ShortTitle="ПлотУтрПорц", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Максимальная осмотическая концентрация", ShortTitle="МаксОсмКонц", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Цвет", ShortTitle="Цвет", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Прозрачность", ShortTitle="Прозрачн", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Реакция", ShortTitle="Реакция", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Белок", ShortTitle="Белок", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Глюкоза", ShortTitle="Глюкоза", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Ацетон", ShortTitle="Ацетон", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Кетоновые тела", ShortTitle="Кетон.Тела", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Осадок:Лейкоциты", ShortTitle="Осад:Лейк", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Осадок:Эритроциты", ShortTitle="Осад:Эритр", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Билирубин", ShortTitle="Билирубин", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Аммиак", ShortTitle="Аммиак", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Слизь", ShortTitle="Слизь", TestType=testTypes.ElementAt(3)},

                    new Test {Title="Ангиография", ShortTitle="Ангиограф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Ирригоскопия", ShortTitle="Ирригоскопия", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Коронаграфия", ShortTitle="Коронограф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Маммография", ShortTitle="Маммогр", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген грудн клетки", ShortTitle="РентГрудКл", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген желудка", ShortTitle="РентЖелуд", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген легких", ShortTitle="РентЛегк", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген пазух носа", ShortTitle="РентНос", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Сальпингография", ShortTitle="СальпГраф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Урография", ShortTitle="Урограф", TestType=testTypes.ElementAt(4)},

                    new Test {Title="Велоэргометрия", ShortTitle="ВЭМ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Измерение плече-лодыжечного индекса", ShortTitle="ПлечЛодИнд", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Кардиореспираторный мониторинг", ShortTitle="КРМ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Кардиотокография плода", ShortTitle="КТГ Плод", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Ортостатическая проба", ShortTitle="ОртстатПроб", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Пульсоксиметрия", ShortTitle="ПульсОксМетр", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Реоэнцефалография", ShortTitle="РЭГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Суточный мониторинг артериальног давления", ShortTitle="СМАД", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Эхокардиография", ShortTitle="ЭХОКГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Холтеровское мониторирование ЭКГ", ShortTitle="ХолЭКГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Электронейромиография", ShortTitle="ЭНМГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Эхоэнцефалография", ShortTitle="ЭХО-ЭГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Электроенцефалография", ShortTitle="ЭЭГ", TestType=testTypes.ElementAt(5)},

                    new Test {Title="КТ Гортани", ShortTitle="КТГортань", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудного отдела", ShortTitle="КТГрудОтд", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудной Клетки с Контрастом", ShortTitle="КТГрудКлКонт", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудной Полости с Контрастом", ShortTitle="КТГрудПолКон", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Забрюшинного Пространства", ShortTitle="КТЗабрюшПрос", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Брюшной Полости", ShortTitle="КТБрюшПол", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Коленного сустава", ShortTitle="КТКолСуст", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Костей и суставов", ShortTitle="КТКостИСуст", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Легких", ShortTitle="КТЛегких", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Матки", ShortTitle="КТМатки", TestType=testTypes.ElementAt(6)},

                    new Test {Title="Дифузионная МРТ всего тела", ShortTitle="МРТТелаДиф", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ брюшной полости", ShortTitle="МРТБрюшПол", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ всего тела", ShortTitle="МРТТела", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головного мозга", ShortTitle="МРТГолМозг", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головного мозга с контрастом", ShortTitle="МРТГолМозгКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головы", ShortTitle="МРТГол", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головы с контрастом", ShortTitle="МРТГолКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Крестцового отдела позвоночника", ShortTitle="МРТПозвКрестц", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Молочных желез", ShortTitle="МРТМолЖел", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Малого таза", ShortTitle="МРТМалТаз", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Позвоночника", ShortTitle="МРТПозв", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Позвоночника с контрастом", ShortTitle="МРТПозвКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Сосудов головного мозга", ShortTitle="МРТГолМозгСос", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Сосудов шеи", ShortTitle="МРТШеяСос", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Спинного мозга", ShortTitle="МРТСпинМозг", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Спинного мозга с контрастом", ShortTitle="МРТСпинМозгКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Шейного отдела позвоночника", ShortTitle="МРТПозвШейн", TestType=testTypes.ElementAt(7)},

                    new Test {Title="3D-4D Узи", ShortTitle="3D4DУзи", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Доплерография Сердца", ShortTitle="ДопГрафСердц", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Доплерометрия", ShortTitle="ДоплМетр", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование", ShortTitle="УЗДС", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование брахиоцефальных артерий", ShortTitle="УЗДСБрахАрт", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Стресс Эхокардиография", ShortTitle="СтрессЭхоКГ", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Транскраниальное дуплексное сканирование", ShortTitle="ТКДС", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплекское сканирование почек", ShortTitle="УЗДСПочек", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование сосудов головы и шеи", ShortTitle="УЗДССосГоловШеи", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ брюшной полости", ShortTitle="УЗИБрюшПол", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Головного мозга", ShortTitle="УЗИГолМозг", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Желудка", ShortTitle="УЗИЖелуд", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Желчного Пузыря", ShortTitle="УЗИЖелчПуз", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Кишечника", ShortTitle="УЗИКишеч", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Коленного сустава", ShortTitle="УЗИКолСуст", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Лимфоузлов", ShortTitle="УЗИЛимфоуз", TestType=testTypes.ElementAt(8)},

                };
                List<TestData> testDatas = new List<TestData>
                {
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), MedCard=medCards.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), MedCard=medCards.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), MedCard=medCards.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), MedCard=medCards.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"}
                };

                List<DrugClass> drugClasses = new List<DrugClass>
                {
                    new DrugClass {Title="Нарушение функции ЦНС"},
                    new DrugClass {Title="Нестероидные противоспалительные средства"},
                    new DrugClass {Title="Иммунотропные средства"},
                    new DrugClass {Title="Противоаллергические средства"},
                    new DrugClass {Title="Заболевания органов дыхания"},
                    new DrugClass {Title="Коррекция сосудистого тонуса"},
                    new DrugClass {Title="Антиангинальные средства"},
                    new DrugClass {Title="Кардиотонические средства"},
                    new DrugClass {Title="Антиаритмические средства"},
                    new DrugClass {Title="Усиление выделительной функции почек"},
                    new DrugClass {Title="Заболевания органов пищеварения"},
                    new DrugClass {Title="Коррекция метаболических нарушений"},
                    new DrugClass {Title="Акушерско-гинекологическая практика"},
                    new DrugClass {Title="Урологическая практика"},
                    new DrugClass {Title="Коррекция нарушений гомеостаза"},
                    new DrugClass {Title="Коррекция нарушений гемостаза"},
                    new DrugClass {Title="Коррекция нарушений кроветворения"},
                    new DrugClass {Title="Противомикробные, противовирусные, противопаразитарные средства"},
                    new DrugClass {Title="Лечение онкологических заболеваний"}
                };
                List<DrugSubClass> drugSubClasses = new List<DrugSubClass>
                {
                    //нарушения цнс
                    new DrugSubClass {Title="Нейролептики", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Транквилизаторы", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Антидипрессанты", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Снотворные", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Седативные", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Нормотимические", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Психостимуляторы", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Мозговой метаболизм", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Аналептики", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Адаптогены и общетонизирующие", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Противосудорожные", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Противопаркинснические", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Общая анастезия", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Местная анастезия", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Анальгезирующие", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Миорелаксанты", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Нервномышечная передача", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Лечение лекарственной зависимости", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Альцгеймер", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Рассеяный склероз", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Мигрень", DrugClass=drugClasses.ElementAt(0)},
                    new DrugSubClass {Title="Усранение головокружения", DrugClass=drugClasses.ElementAt(0)},

                };
                List<DrugGroup> drugGroups = new List<DrugGroup>
                {
                    //нейролептики
                    new DrugGroup {Title="Фенотизианы и трициклины", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные тиоксантена", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные бутироферона", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные дифенилбутилпиперидина", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Бензамиды", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные бензизоксазола", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные индола", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Производные бензодиазепина", DrugSubClass=drugSubClasses.ElementAt(0)},
                    new DrugGroup {Title="Алкалоиды раувольфии", DrugSubClass=drugSubClasses.ElementAt(0)},
                };
                List<DrugSubGroup> drugSubGroups = new List<DrugSubGroup>
                {
                    //фенофт и трицикл
                    new DrugSubGroup {Title="Алифатические производные", DrugGroup=drugGroups.ElementAt(0)},
                    new DrugSubGroup {Title="Пиперазиновые производные", DrugGroup=drugGroups.ElementAt(0)},
                    new DrugSubGroup {Title="Пиперидиновые производные", DrugGroup=drugGroups.ElementAt(0)},
                    //тиоксе
                    new DrugSubGroup {Title="Производные тиоксантена", DrugGroup=drugGroups.ElementAt(1)},


                };
                List<Drug> drugs = new List<Drug>
                {
                    //алифатич
                    new Drug { Title="Аминазин", Substance="Хлорпромазин", DrugSubGroup=drugSubGroups.ElementAt(0)},
                    new Drug { Title="Тизерцин", Substance="Левомепромазин", DrugSubGroup=drugSubGroups.ElementAt(0)},
                    new Drug { Substance="Алимемазин", DrugSubGroup=drugSubGroups.ElementAt(0)},
                    new Drug { Substance="Промазин", DrugSubGroup=drugSubGroups.ElementAt(0)},
                    //пиперазин
                    new Drug { Substance="Трифлуоперазин", Title="Апо-трифлуоперазин, Стелазин, Трифтазин, Тразин", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Перфеназин", Title="Этаперазин", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Флуфеназин", Title="Апо-флуфеназин, Модитен, Миренил", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Флуфеназин Деканоат", Title="Модитен-Апо", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Тиопроперазин", Title="Мажептил", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Тиэтилперазин", Title="Торекан", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    new Drug { Substance="Оланзапин", Title="Олеанз, Зипрекса", DrugSubGroup=drugSubGroups.ElementAt(1)},
                    //пиперидин
                    new Drug { Substance="Перициазин", Title="Неулептил", DrugSubGroup=drugSubGroups.ElementAt(2)},
                    new Drug { Substance="Тиоридазин", Title="Сонапакс, Тисон, Тиодазин, Тиорил", DrugSubGroup=drugSubGroups.ElementAt(2)},
                    new Drug { Substance="Тиопроперазин", Title="Мажептил", DrugSubGroup=drugSubGroups.ElementAt(2)},

                };
                List<PharmacoTherapyData> pharmacoTherapyDatas = new List<PharmacoTherapyData>
                {
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, MedCard=medCards.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, MedCard=medCards.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, MedCard=medCards.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, MedCard=medCards.ElementAt(0)}
                };

                List<PhysTherMethodGroup> physTherMethodGroups = new List<PhysTherMethodGroup>
                {
                   new PhysTherMethodGroup { Caption="Анальгетические методы"},
                   new PhysTherMethodGroup { Caption="Лечение воспаления", Title="Методы лечения восполения" },
                   new PhysTherMethodGroup { Caption="ЦНС", Title="Методы воздействия на ЦНС" },
                   new PhysTherMethodGroup { Caption="Переферическая НС", Title="Методы воздействия на Переферическую НС" },
                   new PhysTherMethodGroup { Caption="Мышечная система", Title="Методы воздействия на мышечную систему" },
                   new PhysTherMethodGroup { Caption="Сердце и Сосуды", Title="Методы воздействия преимущественно на Сердце и Сосуды" },
                   new PhysTherMethodGroup { Caption="Система Крови", Title="Методы воздействия преимущественно на Систему Крови" },
                   new PhysTherMethodGroup { Caption="Респираторный тракт", Title="Методы воздействия преимущественно на Респираторный тракт" },
                   new PhysTherMethodGroup { Caption="Желуд-кишечн тракт", Title="Методы воздействия на Желудочно-кишечный тракт" },
                   new PhysTherMethodGroup { Caption="Кожа и Соед. ткань", Title="Методы воздействия на Кожу и Соединительную ткань"},
                   new PhysTherMethodGroup { Caption="Мочепол. система", Title="Методы воздействия на Мочеполовую систему" },
                   new PhysTherMethodGroup { Caption="Эндокрин. система", Title="Методы воздействия на Эндокринную систему" },
                   new PhysTherMethodGroup { Caption="Корр. обмена веществ", Title="Методы коррекции обмена веществ" },
                   new PhysTherMethodGroup { Caption="Мод. Иммун и несп. Резист.", Title="Методы модуляции иммунитета и неспецифической резистентности" },
                   new PhysTherMethodGroup { Caption="Вирусы, Бактерии и Грибы", Title="Методы воздействия на вирусы, бактерии, грибы" },
                   new PhysTherMethodGroup { Caption="Повреждения, Раны, Ожоги", Title="Методы лечения повреждений, ран и ожогов"},
                   new PhysTherMethodGroup { Caption="Злокач. образования", Title="Методы лечения злокачественных образований" }
                };
                List<PhysTherMethod> physTherMethods = new List<PhysTherMethod>
                {
                    new PhysTherMethod {Caption="Центр. воздействие", Title="Методы центрального воздействия", PhysTherMethodGroup=physTherMethodGroups.ElementAt(0)},
                    new PhysTherMethod{Caption="Периф. воздействие", Title="Методы периферического воздействия", PhysTherMethodGroup=physTherMethodGroups.ElementAt(0)},
                    new PhysTherMethod{Caption="Альтер-экссуд. фаза", Title="Альтернативно-экссудативная фаза", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Caption="Пролиферативная фаза", Title="Пролиферативная фаза", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Caption="Репарат. регенерация", Title="Репаративная регенерация", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Caption="Седативные", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Caption="Психостимульрующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Caption="Тонизирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Caption="Анестезирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Caption="Нейростимулирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Caption="Трофостимульрующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Caption="Раздр.Св.Нервн.Оконч.", Title="Раздражающие свободные нервные окончания", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Caption="Миостимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(4)},
                    new PhysTherMethod{Caption="Миорелаксирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(4)},
                    new PhysTherMethod{Caption="Кардиотонические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Caption="Гипотензивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Caption="Сосуд.Расш.Спазмолит.", Title="Сосудорасширяющие и спазмолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Caption="Сосудосуживующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Caption="Лимфодренирующие", Title="Лимфодренирующие (противоотечные)",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Caption="Гиперкоагулирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Caption="Гипокоагулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Caption="Гемостимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Caption="Гемодеструктивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Caption="Бронхролитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Caption="Мукокинетические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Caption="Усил.АльвКап.Тран", Title="Усиливающие альвеолокаппилярный транспорт",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Caption="Стимул.Секрет.Желуд", Title="Стимулирующие секреторную функцию желудка",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Caption="Ослаб.Секрет.Желуд", Title="Ослабляющие секреторную функцию желудка",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Caption="Усил.Мотор.Кишечн", Title="Усиливающие моторную функцию кишечника",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Caption="Ослаб.Мотор.Кишечн", Title="Ослабляющие моторную функцию кишечника",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Caption="Желчегонные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Caption="МеланинСтим.ФотоСенс.", Title="Меланинстимулирующие и фотосенсибилизирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Обволакивающие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Вяжущие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Противозудные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Диафоретические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Кератолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Дефиброзирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Модул.Обм.СоедТкан", Title="Модулирующие обмен соединительной ткани",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Caption="Мочегонные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Caption="Корр.Эрект.Ф-цию", Title="Корригирующие эректильную функцию",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Caption="Стим.Репрод.Ф-цию", Title="Стимулирующие репродуктивную функцию",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Caption="Стим.Гипот.Гипоф", Title="Стимулирующие гипоталамус и гипофиз",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Caption="Стим.ЩитовЖел", Title="Стимулирующие щитовидную железу",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Caption="Стим.Надпочечники", Title="Стимулирующие надпочечники",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Caption="Стим.ПоджЖел", Title="Стимулирующие поджелудочную железу",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Caption="Энзимстимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Caption="Пластические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Caption="Ионокоррегирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Caption="Витаминостимулир",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Caption="Имунностимулрующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Caption="Имунносупрессивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Caption="Гипосенсибилизир",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Caption="Противовирусные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(14)},
                    new PhysTherMethod{Caption="БактерЦид.МикоЦид", Title="Бактерицидные и микоцидные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(14)},
                    new PhysTherMethod{Caption="Заживл.Ран.Поврежд", Title="Стимулирующие заживление ран и повреждений",PhysTherMethodGroup=physTherMethodGroups.ElementAt(15)},
                    new PhysTherMethod{Caption="Противоожоговые",PhysTherMethodGroup=physTherMethodGroups.ElementAt(15)},
                    new PhysTherMethod{Caption="Онкодеструктивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(16)},
                    new PhysTherMethod{Caption="Цитолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(16)}
                };
                List<PhysTherFactGroup> physTherFactGroups = new List<PhysTherFactGroup>
                {
                    new PhysTherFactGroup { Title="Постоянный электрический ток Низкого напряжения", Caption="ПТНН"},
                    new PhysTherFactGroup { Title="Импульсный ток Низкого напряжения", Caption="ИТНН"},
                    new PhysTherFactGroup { Title="Электрический ток Высокого напряжения", Caption="ЭТВН"},
                    new PhysTherFactGroup { Title="Электрические, магнитные, электромангнитные поля", Caption="ЭлМагПоля"},
                    new PhysTherFactGroup { Title="Электромагнитные колебания оптического диапазона", Caption="ЭлКолебанОпт"},
                    new PhysTherFactGroup { Title="Механические колебания среды", Caption="МеханКолебанСреды"},
                    new PhysTherFactGroup { Title="Измененная или особая воздушная среда", Caption="ВоздушнСреда"},
                    new PhysTherFactGroup { Title="Пресная вода, природные минеральные воды и их аналоги", Caption="Воды"},
                    new PhysTherFactGroup { Title="Теплолечение, Криолечение", Caption="ТеплоКриоЛечение"}
                };
                List<PhysioTherapyFactor> physioTherapyFactors = new List<PhysioTherapyFactor>
                {
                    new PhysioTherapyFactor{ Caption="Гальванизация", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
                    new PhysioTherapyFactor{ Title="Лекарственный Электрофорез", Caption="Электрофорез", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
                    new PhysioTherapyFactor{Caption="Электросон", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{Caption="Диадинамотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Амплипульстерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Интерференцтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Флюктуризация", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Электродиагностика", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Электростимуляция", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Caption="Ультратонотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Title="Местная дарсонвализация", Caption="Дарсонвализация", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Caption="Индуктотермия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Title="Ультравысокочастотная терапия", Caption="УльтраВысокЧастТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Title="Микроволновая терапия", Caption="МикроволновТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Caption="Франклинизация", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
                    new PhysioTherapyFactor{ Caption="Магнитотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
                    new PhysioTherapyFactor{ Title="Терапия инфракрасным излученим", Caption="ИнфракраснИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ Title="Терапия видимым излучением", Caption="ВидимоеИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ Title="Терапия ультрафиолетовым излучением", Caption="УльтрафиолетИзлуч", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ Title="Лазерная терапия", Caption="ЛазернаяТер", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ Title="Лечебный массаж", Caption="МассажЛечебн", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ Title="Ультразвуковая терапия", Caption="УльтразвукТер", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ Title="Лекарственный фонофорез", Caption="ЛекарствФоноф", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ Caption="Аэрозольтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Электроаэрозольтерапия", Caption="ЭлектроАэрозТер", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Caption="Аэроионтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Caption="Галотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Гипербарическая оксигенерация", Caption="ГипербарОксиген", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Caption="Климатотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Caption="Пресная вода", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
                    new PhysioTherapyFactor{ Title="Минеральные соли", Caption="МинСоли", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
                    new PhysioTherapyFactor{ Caption="Теплолечение",PhysTherFactGroup=physTherFactGroups.ElementAt(8)},
                    new PhysioTherapyFactor{ Caption="Криолечение", PhysTherFactGroup=physTherFactGroups.ElementAt(8)}
                };
                List<PhysioTherapyData> physioTherapyDatas = new List<PhysioTherapyData>
                {
                    new PhysioTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        TherapyDoctor = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Ожидание,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        TherapyDoctor = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Ожидание,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        TherapyDoctor = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Неявка,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        TherapyDoctor = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Готов,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                };

                List<SurgencyGroup> surgencyGroups = new List<SurgencyGroup>
                {
                    new SurgencyGroup { Title="Эктомия"},
                    new SurgencyGroup { Title="Резекция"},
                    new SurgencyGroup { Title="Стомия"},
                    new SurgencyGroup { Title="Ушивание"},
                    new SurgencyGroup { Title="Дилатация"},
                    new SurgencyGroup { Title="Экстракция"},
                    new SurgencyGroup { Title="Ампутация"},
                    new SurgencyGroup { Title="Реплантация"},
                    new SurgencyGroup { Title="Трансплантация"},
                    new SurgencyGroup { Title="Протезирование"},
                    new SurgencyGroup { Title="Шунтирование"},
                    new SurgencyGroup { Title="Пункция"},
                    new SurgencyGroup { Title="Вскрытие"}
                };
                List<SurgencyOperation> surgencyOperations = new List<SurgencyOperation>
                {
                    new SurgencyOperation{ Caption="Вазэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Caption="Полипэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Caption="Эктомия Зуба", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Caption="Гастроэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Caption="Гепатэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Caption="Гистерэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},

                    new SurgencyOperation{ Caption="ПДР", Title="Панкреатодуоденальная резекция", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="ПРЖ", Title="Продольная резекция желудка", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезКореньЗуба", Title="Резекция корня зуба", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезПоджелудЖел", Title="Резекция поджелудочной железы", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезПрямКишка", Title="Резекция прямой кишки", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезЯичники", Title="Резекция яичников", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезПечени",Title="Резекция печени", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Caption="РезОбодКишка", Title="Резекция ободочной кишки", SurgencyGroup=surgencyGroups.ElementAt(1)},

                    new SurgencyOperation{Caption="Гастростомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Caption="Трахеостомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Caption="Илео-стомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Caption="ХолПанкрСтомия",Title="Холангиопанкреатостомия",SurgencyGroup=surgencyGroups.ElementAt(2)},

                    new SurgencyOperation{Caption="Лапаротомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                    new SurgencyOperation{Caption="Торакотомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                    new SurgencyOperation{Caption="Коникотомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                };
                List<SurgencyTherapyData> surgencyTherapyDatas = new List<SurgencyTherapyData>
                {
                    new SurgencyTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Диагностическая,
                        SurgencyPriority = SurgencyPriority.Плановая,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyStatus = SurgencyStatus.Ожидание
                    },
                    new SurgencyTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Лечебная,
                        SurgencyPriority = SurgencyPriority.Срочная,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyStatus = SurgencyStatus.Ожидание
                    },
                    new SurgencyTherapyData
                    {
                        MedCard = medCards.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Паллативная,
                        SurgencyPriority = SurgencyPriority.Плановая,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyStatus = SurgencyStatus.Готово
                    }
                };

                db.DepartmentTitles.AddRange(departmentTitles);
                db.Departments.AddRange(departments);
                db.Belays.AddRange(belays);
                db.Patients.AddRange(patients);
                db.Staffs.AddRange(staffs);
                db.Changes.AddRange(changes);
                db.MedCards.AddRange(medCards);
                db.Entries.AddRange(entries);

                db.TestTypes.AddRange(testTypes);
                db.Tests.AddRange(tests);
                db.TestDatas.AddRange(testDatas);

                db.DrugClasses.AddRange(drugClasses);
                db.DrugSubClasses.AddRange(drugSubClasses);
                db.DrugGroups.AddRange(drugGroups);
                db.DrugSubGroups.AddRange(drugSubGroups);
                db.Drugs.AddRange(drugs);
                db.PharmacoTherapyDatas.AddRange(pharmacoTherapyDatas);

                db.PhysTherMethodGroups.AddRange(physTherMethodGroups);
                db.PhysTherMethods.AddRange(physTherMethods);
                db.PhysTherFactGroups.AddRange(physTherFactGroups);
                db.PhysioTherapyFactors.AddRange(physioTherapyFactors);
                db.PhysioTherapyDatas.AddRange(physioTherapyDatas);

                db.SurgencyGroups.AddRange(surgencyGroups);
                db.SurgencyOperations.AddRange(surgencyOperations);
                db.SurgencyTherapyDatas.AddRange(surgencyTherapyDatas);

                db.SaveChanges();
            }
        }
    }
}
