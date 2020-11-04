using Hospital.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.EntityFramework
{
    public class ContentCreate
    {
        public ContentCreate()
        {
            using (HospitalDbContext db = new HospitalDbContext())
            {
                List<DepartmentTitle> departmentTitles = new List<DepartmentTitle>
                {
                    new DepartmentTitle{ Title="Общая терапия", Code="Therapy"},
                    new DepartmentTitle{Title = "Стоматология", Code="Stomatology"},
                    new DepartmentTitle{Title = "Станция переливания крови", Code="Transfusiology"},
                    new DepartmentTitle{Title = "Интенсивная терапия", Code="ICU"},
                };
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
                List<Department> departments = new List<Department>
                {
                    new Department { Title=departmentTitles.ElementAt(0), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Ambulatory, Manager=staffs.ElementAt(new Random().Next(staffs.Count)) },
                    new Department { Title=departmentTitles.ElementAt(1), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Stationary, Manager=staffs.ElementAt(new Random().Next(staffs.Count)) },
                    new Department { Title=departmentTitles.ElementAt(2), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Laboratory, Manager=staffs.ElementAt(new Random().Next(staffs.Count)) },
                    new Department { Title=departmentTitles.ElementAt(3), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Ambulatory, Manager=staffs.ElementAt(new Random().Next(staffs.Count)) },
                    new Department { Title=departmentTitles.ElementAt(3), Adress = adresses.ElementAt(new Random().Next(adresses.Count)), Type=DepartmentType.Stationary, Manager=staffs.ElementAt(new Random().Next(staffs.Count)) },
                };
                List<Change> changes = new List<Change>
                {
                    new Change { ChangeTitle=ChangeTitle.Первая, TimeStart=DateTime.Parse("8:00"), TimeSpan=TimeSpan.FromHours(6), Department=departments.ElementAt(0)},
                    new Change { ChangeTitle=ChangeTitle.Вторая, TimeStart=DateTime.Parse("13:00"), TimeSpan=TimeSpan.FromHours(6), Department=departments.ElementAt(0)},
                    new Change { ChangeTitle=ChangeTitle.Ночная, TimeStart=DateTime.Parse("19:00"), TimeSpan=TimeSpan.FromHours(12), Department=departments.ElementAt(0)}
                };

                List<Entry> entries = new List<Entry>
                {
                    new Entry {Chain=1, Destination=staffs.ElementAt(new Random().Next(staffs.Count)), EntryStatus=EntryStatus.Open, Patient=patients.ElementAt(new Random().Next(patients.Count)), TargetDateTime=DateTime.Now}
                };
                List<Visit> visits = new List<Visit>
                {
                    new Visit { EntryIn = entries.ElementAt(0) }
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
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), Visit=visits.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), Visit=visits.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), Visit=visits.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"},
                    new TestData {Test=tests.ElementAt(new Random().Next(tests.Count)), Visit=visits.ElementAt(0), DateCreate=DateTime.Now, StaffResult=staffs.ElementAt(new Random().Next(staffs.Count)), Status=TestStatus.Готов, DateResult=DateTime.Now, Value="Passed"}
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
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, Visit=visits.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, Visit=visits.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, Visit=visits.ElementAt(0)},
                    new PharmacoTherapyData{ Drug = drugs.ElementAt(new Random().Next(drugs.Count)), Dose=((new Random().Next(10)).ToString()), DateCreate=DateTime.Now, Treatment=Treatment.Симптоматическая, Visit=visits.ElementAt(0)}
                };

                List<PhysTherMethodGroup> physTherMethodGroups = new List<PhysTherMethodGroup>
                {
                   new PhysTherMethodGroup { Title="Анальгетические методы"},
                   new PhysTherMethodGroup { Title="Лечение воспаления", FullTitle="Методы лечения восполения" },
                   new PhysTherMethodGroup { Title="ЦНС", FullTitle="Методы воздействия на ЦНС" },
                   new PhysTherMethodGroup { Title="Переферическая НС", FullTitle="Методы воздействия на Переферическую НС" },
                   new PhysTherMethodGroup { Title="Мышечная система", FullTitle="Методы воздействия на мышечную систему" },
                   new PhysTherMethodGroup { Title="Сердце и Сосуды", FullTitle="Методы воздействия преимущественно на Сердце и Сосуды" },
                   new PhysTherMethodGroup { Title="Система Крови", FullTitle="Методы воздействия преимущественно на Систему Крови" },
                   new PhysTherMethodGroup { Title="Респираторный тракт", FullTitle="Методы воздействия преимущественно на Респираторный тракт" },
                   new PhysTherMethodGroup { Title="Желуд-кишечн тракт", FullTitle="Методы воздействия на Желудочно-кишечный тракт" },
                   new PhysTherMethodGroup { Title="Кожа и Соед. ткань", FullTitle="Методы воздействия на Кожу и Соединительную ткань"},
                   new PhysTherMethodGroup { Title="Мочепол. система", FullTitle="Методы воздействия на Мочеполовую систему" },
                   new PhysTherMethodGroup { Title="Эндокрин. система", FullTitle="Методы воздействия на Эндокринную систему" },
                   new PhysTherMethodGroup { Title="Корр. обмена веществ", FullTitle="Методы коррекции обмена веществ" },
                   new PhysTherMethodGroup { Title="Мод. Иммун и несп. Резист.", FullTitle="Методы модуляции иммунитета и неспецифической резистентности" },
                   new PhysTherMethodGroup { Title="Вирусы, Бактерии и Грибы", FullTitle="Методы воздействия на вирусы, бактерии, грибы" },
                   new PhysTherMethodGroup { Title="Повреждения, Раны, Ожоги", FullTitle="Методы лечения повреждений, ран и ожогов"},
                   new PhysTherMethodGroup { Title="Злокач. образования", FullTitle="Методы лечения злокачественных образований" }
                };
                List<PhysTherMethod> physTherMethods = new List<PhysTherMethod>
                {
                    new PhysTherMethod {Title="Центр. воздействие", FullTitle="Методы центрального воздействия", PhysTherMethodGroup=physTherMethodGroups.ElementAt(0)},
                    new PhysTherMethod{Title="Периф. воздействие", FullTitle="Методы периферического воздействия", PhysTherMethodGroup=physTherMethodGroups.ElementAt(0)},
                    new PhysTherMethod{Title="Альтер-экссуд. фаза", FullTitle="Альтернативно-экссудативная фаза", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Title="Пролиферативная фаза", FullTitle="Пролиферативная фаза", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Title="Репарат. регенерация", FullTitle="Репаративная регенерация", PhysTherMethodGroup=physTherMethodGroups.ElementAt(1)},
                    new PhysTherMethod{Title="Седативные", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Title="Психостимульрующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Title="Тонизирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(2)},
                    new PhysTherMethod{Title="Анестезирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Title="Нейростимулирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Title="Трофостимульрующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Title="Раздр.Св.Нервн.Оконч.", FullTitle="Раздражающие свободные нервные окончания", PhysTherMethodGroup=physTherMethodGroups.ElementAt(3)},
                    new PhysTherMethod{Title="Миостимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(4)},
                    new PhysTherMethod{Title="Миорелаксирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(4)},
                    new PhysTherMethod{Title="Кардиотонические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Title="Гипотензивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Title="Сосуд.Расш.Спазмолит.", FullTitle="Сосудорасширяющие и спазмолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Title="Сосудосуживующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Title="Лимфодренирующие", FullTitle="Лимфодренирующие (противоотечные)",PhysTherMethodGroup=physTherMethodGroups.ElementAt(5)},
                    new PhysTherMethod{Title="Гиперкоагулирующие", PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Title="Гипокоагулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Title="Гемостимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Title="Гемодеструктивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(6)},
                    new PhysTherMethod{Title="Бронхролитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Title="Мукокинетические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Title="Усил.АльвКап.Тран", FullTitle="Усиливающие альвеолокаппилярный транспорт",PhysTherMethodGroup=physTherMethodGroups.ElementAt(7)},
                    new PhysTherMethod{Title="Стимул.Секрет.Желуд", FullTitle="Стимулирующие секреторную функцию желудка",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Title="Ослаб.Секрет.Желуд", FullTitle="Ослабляющие секреторную функцию желудка",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Title="Усил.Мотор.Кишечн", FullTitle="Усиливающие моторную функцию кишечника",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Title="Ослаб.Мотор.Кишечн", FullTitle="Ослабляющие моторную функцию кишечника",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Title="Желчегонные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(8)},
                    new PhysTherMethod{Title="МеланинСтим.ФотоСенс.", FullTitle="Меланинстимулирующие и фотосенсибилизирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Обволакивающие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Вяжущие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Противозудные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Диафоретические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Кератолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Дефиброзирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Модул.Обм.СоедТкан", FullTitle="Модулирующие обмен соединительной ткани",PhysTherMethodGroup=physTherMethodGroups.ElementAt(9)},
                    new PhysTherMethod{Title="Мочегонные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Title="Корр.Эрект.Ф-цию", FullTitle="Корригирующие эректильную функцию",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Title="Стим.Репрод.Ф-цию", FullTitle="Стимулирующие репродуктивную функцию",PhysTherMethodGroup=physTherMethodGroups.ElementAt(10)},
                    new PhysTherMethod{Title="Стим.Гипот.Гипоф", FullTitle="Стимулирующие гипоталамус и гипофиз",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Title="Стим.ЩитовЖел", FullTitle="Стимулирующие щитовидную железу",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Title="Стим.Надпочечники", FullTitle="Стимулирующие надпочечники",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Title="Стим.ПоджЖел", FullTitle="Стимулирующие поджелудочную железу",PhysTherMethodGroup=physTherMethodGroups.ElementAt(11)},
                    new PhysTherMethod{Title="Энзимстимулирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Title="Пластические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Title="Ионокоррегирующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Title="Витаминостимулир",PhysTherMethodGroup=physTherMethodGroups.ElementAt(12)},
                    new PhysTherMethod{Title="Имунностимулрующие",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Title="Имунносупрессивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Title="Гипосенсибилизир",PhysTherMethodGroup=physTherMethodGroups.ElementAt(13)},
                    new PhysTherMethod{Title="Противовирусные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(14)},
                    new PhysTherMethod{Title="БактерЦид.МикоЦид", FullTitle="Бактерицидные и микоцидные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(14)},
                    new PhysTherMethod{Title="Заживл.Ран.Поврежд", FullTitle="Стимулирующие заживление ран и повреждений",PhysTherMethodGroup=physTherMethodGroups.ElementAt(15)},
                    new PhysTherMethod{Title="Противоожоговые",PhysTherMethodGroup=physTherMethodGroups.ElementAt(15)},
                    new PhysTherMethod{Title="Онкодеструктивные",PhysTherMethodGroup=physTherMethodGroups.ElementAt(16)},
                    new PhysTherMethod{Title="Цитолитические",PhysTherMethodGroup=physTherMethodGroups.ElementAt(16)}
                };
                List<PhysTherFactGroup> physTherFactGroups = new List<PhysTherFactGroup>
                {
                    new PhysTherFactGroup { FullTitle="Постоянный электрический ток Низкого напряжения", Title="ПТНН"},
                    new PhysTherFactGroup { FullTitle="Импульсный ток Низкого напряжения", Title="ИТНН"},
                    new PhysTherFactGroup { FullTitle="Электрический ток Высокого напряжения", Title="ЭТВН"},
                    new PhysTherFactGroup { FullTitle="Электрические, магнитные, электромангнитные поля", Title="ЭлМагПоля"},
                    new PhysTherFactGroup { FullTitle="Электромагнитные колебания оптического диапазона", Title="ЭлКолебанОпт"},
                    new PhysTherFactGroup { FullTitle="Механические колебания среды", Title="МеханКолебанСреды"},
                    new PhysTherFactGroup { FullTitle="Измененная или особая воздушная среда", Title="ВоздушнСреда"},
                    new PhysTherFactGroup { FullTitle="Пресная вода, природные минеральные воды и их аналоги", Title="Воды"},
                    new PhysTherFactGroup { FullTitle="Теплолечение, Криолечение", Title="ТеплоКриоЛечение"}
                };
                List<PhysioTherapyFactor> physioTherapyFactors = new List<PhysioTherapyFactor>
                {
                    new PhysioTherapyFactor{ Title="Гальванизация", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
                    new PhysioTherapyFactor{ FullTitle="Лекарственный Электрофорез", Title="Электрофорез", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
                    new PhysioTherapyFactor{Title="Электросон", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{Title="Диадинамотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Амплипульстерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Интерференцтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Флюктуризация", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Электродиагностика", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Электростимуляция", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
                    new PhysioTherapyFactor{ Title="Ультратонотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ FullTitle="Местная дарсонвализация", Title="Дарсонвализация", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Title="Индуктотермия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ FullTitle="Ультравысокочастотная терапия", Title="УльтраВысокЧастТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ FullTitle="Микроволновая терапия", Title="МикроволновТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
                    new PhysioTherapyFactor{ Title="Франклинизация", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
                    new PhysioTherapyFactor{ Title="Магнитотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
                    new PhysioTherapyFactor{ FullTitle="Терапия инфракрасным излученим", Title="ИнфракраснИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ FullTitle="Терапия видимым излучением", Title="ВидимоеИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ FullTitle="Терапия ультрафиолетовым излучением", Title="УльтрафиолетИзлуч", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ FullTitle="Лазерная терапия", Title="ЛазернаяТер", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
                    new PhysioTherapyFactor{ FullTitle="Лечебный массаж", Title="МассажЛечебн", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ FullTitle="Ультразвуковая терапия", Title="УльтразвукТер", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ FullTitle="Лекарственный фонофорез", Title="ЛекарствФоноф", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
                    new PhysioTherapyFactor{ Title="Аэрозольтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ FullTitle="Электроаэрозольтерапия", Title="ЭлектроАэрозТер", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Аэроионтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Галотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ FullTitle="Гипербарическая оксигенерация", Title="ГипербарОксиген", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Климатотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
                    new PhysioTherapyFactor{ Title="Пресная вода", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
                    new PhysioTherapyFactor{ FullTitle="Минеральные соли", Title="МинСоли", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
                    new PhysioTherapyFactor{ Title="Теплолечение",PhysTherFactGroup=physTherFactGroups.ElementAt(8)},
                    new PhysioTherapyFactor{ Title="Криолечение", PhysTherFactGroup=physTherFactGroups.ElementAt(8)}
                };
                List<PhysioTherapyData> physioTherapyDatas = new List<PhysioTherapyData>
                {
                    new PhysioTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        Staff = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Ожидание,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        Staff = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Ожидание,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        Staff = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Неявка,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                    new PhysioTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        Staff = staffs.ElementAt(new Random().Next(staffs.Count)),
                        CreateDateTime = DateTime.Now,
                        PhysTherStatus = PhysTherStatus.Готов,
                        PhysTherMethod = physTherMethods.ElementAt(new Random().Next(physTherMethods.Count)),
                        PhysioTherapyFactor = physioTherapyFactors.ElementAt(new Random().Next(physioTherapyFactors.Count)),
                        TargetDateTime = DateTime.Now
                    },
                };

                List<SurgencyEndoscop> surgencyEndoscops = new List<SurgencyEndoscop>
                {
                    new SurgencyEndoscop { Title="Торакоскопия"},
                    new SurgencyEndoscop { Title="Артроскопия"},
                    new SurgencyEndoscop { Title="Лапароскопия"},
                    new SurgencyEndoscop { Title="Биопсия"},
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
                    new SurgencyOperation{ Title="Вазэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Title="Полипэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Title="Эктомия Зуба", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Title="Гастроэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Title="Гепатэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},
                    new SurgencyOperation{ Title="Гистерэктомия", SurgencyGroup=surgencyGroups.ElementAt(0)},

                    new SurgencyOperation{ Title="ПДР", FullTitle="Панкреатодуоденальная резекция", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="ПРЖ", FullTitle="Продольная резекция желудка", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезКореньЗуба", FullTitle="Резекция корня зуба", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезПоджелудЖел", FullTitle="Резекция поджелудочной железы", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезПрямКишка", FullTitle="Резекция прямой кишки", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезЯичники", FullTitle="Резекция яичников", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезПечени",FullTitle="Резекция печени", SurgencyGroup=surgencyGroups.ElementAt(1)},
                    new SurgencyOperation{ Title="РезОбодКишка", FullTitle="Резекция ободочной кишки", SurgencyGroup=surgencyGroups.ElementAt(1)},

                    new SurgencyOperation{Title="Гастростомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Title="Трахеостомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Title="Илео-стомия",SurgencyGroup=surgencyGroups.ElementAt(2)},
                    new SurgencyOperation{Title="ХолПанкрСтомия",FullTitle="Холангиопанкреатостомия",SurgencyGroup=surgencyGroups.ElementAt(2)},

                    new SurgencyOperation{Title="Лапаротомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                    new SurgencyOperation{Title="Торакотомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                    new SurgencyOperation{Title="Коникотомия",SurgencyGroup=surgencyGroups.ElementAt(12)},
                };
                List<SurgencyTherapyData> surgencyTherapyDatas = new List<SurgencyTherapyData>
                {
                    new SurgencyTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Диагностическая,
                        SurgencyPriority = SurgencyPriority.Плановая,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyEndoscop = surgencyEndoscops.ElementAt(new Random().Next(surgencyEndoscops.Count)),
                        SurgencyStatus = SurgencyStatus.Ожидание
                    },
                    new SurgencyTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Лечебная,
                        SurgencyPriority = SurgencyPriority.Срочная,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyEndoscop = surgencyEndoscops.ElementAt(new Random().Next(surgencyEndoscops.Count)),
                        SurgencyStatus = SurgencyStatus.Ожидание
                    },
                    new SurgencyTherapyData
                    {
                        Visit = visits.FirstOrDefault(),
                        CreateDateTime= DateTime.Now,
                        TargetDateTime = DateTime.Now,
                        SurgencyClass = SurgencyClass.Паллативная,
                        SurgencyPriority = SurgencyPriority.Плановая,
                        SurgencyOperation = surgencyOperations.ElementAt(new Random().Next(surgencyOperations.Count)),
                        SurgencyEndoscop = surgencyEndoscops.ElementAt(new Random().Next(surgencyEndoscops.Count)),
                        SurgencyStatus = SurgencyStatus.Готово
                    }
                };

                db.DepartmentTitles.AddRange(departmentTitles);
                db.Changes.AddRange(changes);
                db.Departments.AddRange(departments);
                db.Belays.AddRange(belays);
                db.Patients.AddRange(patients);
                db.Staffs.AddRange(staffs);
                db.Entries.AddRange(entries);
                db.Visits.AddRange(visits);

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

                db.SurgencyEndoscops.AddRange(surgencyEndoscops);
                db.SurgencyGroups.AddRange(surgencyGroups);
                db.SurgencyOperations.AddRange(surgencyOperations);
                db.SurgencyTherapyDatas.AddRange(surgencyTherapyDatas);

                db.SaveChanges();
            }
        }
    }
}
