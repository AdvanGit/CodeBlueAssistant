using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hospital.EntityFramework.DataGenerator
{
    public static class ContentCreate
    {
        static List<Belay> belays = new List<Belay>
        {
            new Belay {Title="Росгосстрах-медицина"},
            new Belay {Title="СОГАЗ-Мед"},
            new Belay {Title="ВТБ МС"},
            new Belay {Title="МАКС-М"},
            new Belay {Title="АльфаСтрахование-ОМС"}
        };
        static List<DepartmentTitle> departmentTitles = new List<DepartmentTitle>
        {
            new DepartmentTitle{Title = "Общая терапия", Code="Therapy", Caption="Терапия"},
            new DepartmentTitle{Title = "Педиатрическое отделение", Code="Pediatric", Caption="Педиатрия"},
            new DepartmentTitle{Title = "Стоматологическое отделение", Code="Stomatologic", Caption="Стоматология"},
            new DepartmentTitle{Title = "Станция переливания крови", Code="Transfusiologic", Caption="Трансфузиология"},
            new DepartmentTitle{Title = "Отделение интенсивной терапии", Code="ICU", Caption = "ОИТ" },

            new DepartmentTitle{Title = "Кардиологическое отделение", Code="Cardiologic", Caption = "Кардиология" },
            new DepartmentTitle{Title = "Неврологическое отделение", Code="Neurologic", Caption = "Неврология" },
            new DepartmentTitle{Title = "Ортопедическое отделение", Code="Orthopedic", Caption = "Ортопедия" },
            new DepartmentTitle{Title = "Хирургическое отделение", Code="Surgery", Caption = "Хирургия" },
            new DepartmentTitle{Title = "Эндокринологическое отделение", Code="Endocrinologic", Caption = "Эндокринология" },

            new DepartmentTitle{Title = "Клинико-диагностическая лабаратория", Code="Сlinical", Caption = "Клинич. диагностика" },
            new DepartmentTitle{Title = "Микробиологическоя лабаратория", Code="Microbiolocal", Caption = "Микробиология" },
            new DepartmentTitle{Title = "Биохимическая лабаратория", Code="Biochemical", Caption = "Биохимия" },
            new DepartmentTitle{Title = "Серологическая лабаратория", Code="Serological", Caption = "Серология" },
            new DepartmentTitle{Title = "Лабаратория Молекулярной диагностики", Code="Molecular", Caption = "Молекулярка" },

            new DepartmentTitle{Title = "Эндоскопическое отделене", Code="", Caption = "Эндоскопия" },
            new DepartmentTitle{Title = "Рентгенологическое отделение", Code="X-ray", Caption = "Рентгенология" },
            new DepartmentTitle{Title = "Отделение ультразвуковой диагностики", Code="", Caption = "УЗИ" },
            new DepartmentTitle{Title = "Отделение функциональной диагностики", Code="", Caption = "Функционал. диаг." },
            new DepartmentTitle{Title = "Отделение физиотерапии и ЛФК", Code="Physiotherapy", Caption = "Физиотерапия и Лфк" },
            new DepartmentTitle{Title = "Отделение медицинской реабилитации", Code="Reability", Caption = "Реабилитация" },
        };
        static List<Adress> adressesStaff = GenerateAdresses(10);
        static List<Adress> adressesPatient = GenerateAdresses(200);
        static List<TestType> testTypes = new List<TestType>
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
        static List<Test> tests = new List<Test>
                {
                    new Test { Title="Опрос", Caption="Опрос", TestType=testTypes.ElementAt(0)},
                    new Test { Title="Аудиометрия", Caption="Аудиометр", Measure="Дб", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Аускультация", Caption="Аускульт", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Пальпация", Caption="Пальпация", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Перкуссия", Caption="Перкуссия", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Давление", Caption="Давление", TestType=testTypes.ElementAt(0)},
                    new Test {Title="Пульс", Caption="Пульс", TestType=testTypes.ElementAt(0)},

                    new Test {Title="Гемоглобин", Caption="Гемогл", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Гематокрит", Caption="Гематокр", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Эритроциты", Caption="Эритроц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Объем Эритроцита", Caption="Об.Эритроц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Скорость оседания Эритроцита", Caption="Ск.Осед.Эрит", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Лейкоциты", Caption="Лейкоц", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Лейкоцтарная Формула", Caption="Лейк.Форм", TestType=testTypes.ElementAt(1)},
                    new Test {Title="Тромбоциты", Caption="Тромбоц", TestType=testTypes.ElementAt(1)},

                    new Test {Title="Адренналин", Caption="Адреннал", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Аммиак", Caption="Аммиак", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Остаточный Азот", Caption="Азот.Остат", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Альбумин", Caption="Альбумин", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Ацетон", Caption="Ацетон", TestType=testTypes.ElementAt(2)},
                    new Test {Title="цАМФ", Caption="цАМФ", TestType=testTypes.ElementAt(2)},
                    new Test {Title="АльфаГлобулин", Caption="Альф.Глоб", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Билирубин", Caption="блРубин", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Витамин А", Caption="вит.А", TestType=testTypes.ElementAt(2)},
                    new Test {Title="Витамин В1", Caption="вит.В1", TestType=testTypes.ElementAt(2)},

                    new Test {Title="Плотность Утренней Порции", Caption="ПлотУтрПорц", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Максимальная осмотическая концентрация", Caption="МаксОсмКонц", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Цвет", Caption="Цвет", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Прозрачность", Caption="Прозрачн", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Реакция", Caption="Реакция", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Белок", Caption="Белок", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Глюкоза", Caption="Глюкоза", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Ацетон", Caption="Ацетон", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Кетоновые тела", Caption="Кетон.Тела", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Осадок:Лейкоциты", Caption="Осад:Лейк", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Осадок:Эритроциты", Caption="Осад:Эритр", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Билирубин", Caption="Билирубин", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Аммиак", Caption="Аммиак", TestType=testTypes.ElementAt(3)},
                    new Test {Title="Слизь", Caption="Слизь", TestType=testTypes.ElementAt(3)},

                    new Test {Title="Ангиография", Caption="Ангиограф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Ирригоскопия", Caption="Ирригоскопия", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Коронаграфия", Caption="Коронограф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Маммография", Caption="Маммогр", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген грудн клетки", Caption="РентГрудКл", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген желудка", Caption="РентЖелуд", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген легких", Caption="РентЛегк", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Рентген пазух носа", Caption="РентНос", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Сальпингография", Caption="СальпГраф", TestType=testTypes.ElementAt(4)},
                    new Test {Title="Урография", Caption="Урограф", TestType=testTypes.ElementAt(4)},

                    new Test {Title="Велоэргометрия", Caption="ВЭМ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Измерение плече-лодыжечного индекса", Caption="ПлечЛодИнд", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Кардиореспираторный мониторинг", Caption="КРМ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Кардиотокография плода", Caption="КТГ Плод", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Ортостатическая проба", Caption="ОртстатПроб", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Пульсоксиметрия", Caption="ПульсОксМетр", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Реоэнцефалография", Caption="РЭГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Суточный мониторинг артериальног давления", Caption="СМАД", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Эхокардиография", Caption="ЭХОКГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Холтеровское мониторирование ЭКГ", Caption="ХолЭКГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Электронейромиография", Caption="ЭНМГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Эхоэнцефалография", Caption="ЭХО-ЭГ", TestType=testTypes.ElementAt(5)},
                    new Test {Title="Электроенцефалография", Caption="ЭЭГ", TestType=testTypes.ElementAt(5)},

                    new Test {Title="КТ Гортани", Caption="КТГортань", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудного отдела", Caption="КТГрудОтд", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудной Клетки с Контрастом", Caption="КТГрудКлКонт", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Грудной Полости с Контрастом", Caption="КТГрудПолКон", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Забрюшинного Пространства", Caption="КТЗабрюшПрос", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Брюшной Полости", Caption="КТБрюшПол", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Коленного сустава", Caption="КТКолСуст", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Костей и суставов", Caption="КТКостИСуст", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Легких", Caption="КТЛегких", TestType=testTypes.ElementAt(6)},
                    new Test {Title="КТ Матки", Caption="КТМатки", TestType=testTypes.ElementAt(6)},

                    new Test {Title="Дифузионная МРТ всего тела", Caption="МРТТелаДиф", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ брюшной полости", Caption="МРТБрюшПол", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ всего тела", Caption="МРТТела", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головного мозга", Caption="МРТГолМозг", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головного мозга с контрастом", Caption="МРТГолМозгКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головы", Caption="МРТГол", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Головы с контрастом", Caption="МРТГолКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Крестцового отдела позвоночника", Caption="МРТПозвКрестц", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Молочных желез", Caption="МРТМолЖел", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Малого таза", Caption="МРТМалТаз", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Позвоночника", Caption="МРТПозв", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Позвоночника с контрастом", Caption="МРТПозвКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Сосудов головного мозга", Caption="МРТГолМозгСос", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Сосудов шеи", Caption="МРТШеяСос", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Спинного мозга", Caption="МРТСпинМозг", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Спинного мозга с контрастом", Caption="МРТСпинМозгКонт", TestType=testTypes.ElementAt(7)},
                    new Test {Title="МРТ Шейного отдела позвоночника", Caption="МРТПозвШейн", TestType=testTypes.ElementAt(7)},

                    new Test {Title="3D-4D Узи", Caption="3D4DУзи", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Доплерография Сердца", Caption="ДопГрафСердц", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Доплерометрия", Caption="ДоплМетр", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование", Caption="УЗДС", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование брахиоцефальных артерий", Caption="УЗДСБрахАрт", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Стресс Эхокардиография", Caption="СтрессЭхоКГ", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Транскраниальное дуплексное сканирование", Caption="ТКДС", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплекское сканирование почек", Caption="УЗДСПочек", TestType=testTypes.ElementAt(8)},
                    new Test {Title="Дуплексное сканирование сосудов головы и шеи", Caption="УЗДССосГоловШеи", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ брюшной полости", Caption="УЗИБрюшПол", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Головного мозга", Caption="УЗИГолМозг", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Желудка", Caption="УЗИЖелуд", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Желчного Пузыря", Caption="УЗИЖелчПуз", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Кишечника", Caption="УЗИКишеч", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Коленного сустава", Caption="УЗИКолСуст", TestType=testTypes.ElementAt(8)},
                    new Test {Title="УЗИ Лимфоузлов", Caption="УЗИЛимфоуз", TestType=testTypes.ElementAt(8)},
                };

        static List<DrugClass> drugClasses = new List<DrugClass>
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
        static List<DrugSubClass> drugSubClasses = new List<DrugSubClass>
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
        static List<DrugGroup> drugGroups = new List<DrugGroup>
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
        static List<DrugSubGroup> drugSubGroups = new List<DrugSubGroup>
                {
                    //фенофт и трицикл
                    new DrugSubGroup {Title="Алифатические производные", DrugGroup=drugGroups.ElementAt(0)},
                    new DrugSubGroup {Title="Пиперазиновые производные", DrugGroup=drugGroups.ElementAt(0)},
                    new DrugSubGroup {Title="Пиперидиновые производные", DrugGroup=drugGroups.ElementAt(0)},
                    //тиоксе
                    new DrugSubGroup {Title="Производные тиоксантена", DrugGroup=drugGroups.ElementAt(1)},


                };
        static List<Drug> drugs = new List<Drug>
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

        static List<SurgeryGroup> surgeryGroups = new List<SurgeryGroup>
        {
            new SurgeryGroup { Title="Эктомия", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Резекция", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Стомия", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Ушивание", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Дилатация", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Экстракция", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Ампутация", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Реплантация", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Трансплантация", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Протезирование", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Шунтирование", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Пункция", SurgeryType=SurgeryType.Оперативная },
            new SurgeryGroup { Title="Вскрытие", SurgeryType=SurgeryType.Оперативная },

            new SurgeryGroup {Title="Торакоскопия", SurgeryType = SurgeryType.Малоинвазивная}, //13
            new SurgeryGroup{Title="Артроскопия", SurgeryType = SurgeryType.Малоинвазивная},
            new SurgeryGroup{Title="Лапароскопия", SurgeryType = SurgeryType.Малоинвазивная},
            new SurgeryGroup{Title="Биопсия", SurgeryType = SurgeryType.Малоинвазивная},
            new SurgeryGroup{Title="Пункция", SurgeryType = SurgeryType.Малоинвазивная},

        };
        static List<SurgeryOperation> surgeryOperations = new List<SurgeryOperation>
        {
            new SurgeryOperation{ Caption="Вазэктомия", Title="Вазэктомия", SurgeryGroup=surgeryGroups.ElementAt(0)},
            new SurgeryOperation{ Caption="Полипэктомия",Title="Полипэктомия", SurgeryGroup=surgeryGroups.ElementAt(0)},
            new SurgeryOperation{ Caption="Эктомия Зуба",Title="Эктомия Зуба", SurgeryGroup=surgeryGroups.ElementAt(0)},
            new SurgeryOperation{ Caption="Гастроэктомия",Title="Гастроэктомия", SurgeryGroup=surgeryGroups.ElementAt(0)},
            new SurgeryOperation{ Caption="Гепатэктомия",Title="Гепатэктомия", SurgeryGroup=surgeryGroups.ElementAt(0)},
            new SurgeryOperation{ Caption="Гистерэктомия",Title="Гистерэктомия", SurgeryGroup=surgeryGroups.ElementAt(0)},

            new SurgeryOperation{ Caption="ПДР", Title="Панкреатодуоденальная резекция", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="ПРЖ", Title="Продольная резекция желудка", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезКореньЗуба", Title="Резекция корня зуба", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезПоджелудЖел", Title="Резекция поджелудочной железы", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезПрямКишка", Title="Резекция прямой кишки", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезЯичники", Title="Резекция яичников", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезПечени",Title="Резекция печени", SurgeryGroup=surgeryGroups.ElementAt(1)},
            new SurgeryOperation{ Caption="РезОбодКишка", Title="Резекция ободочной кишки", SurgeryGroup=surgeryGroups.ElementAt(1)},

            new SurgeryOperation{Caption="Гастростомия",Title="Гастростомия", SurgeryGroup=surgeryGroups.ElementAt(2)},
            new SurgeryOperation{Caption="Трахеостомия",Title="Трахеостомия", SurgeryGroup=surgeryGroups.ElementAt(2)},
            new SurgeryOperation{Caption="Илео-стомия",Title="Илео-стомия", SurgeryGroup=surgeryGroups.ElementAt(2)},
            new SurgeryOperation{Caption="ХолПанкрСтомия",Title="Холангиопанкреатостомия",SurgeryGroup=surgeryGroups.ElementAt(2)},

            new SurgeryOperation{Caption="Лапаротомия",Title="Лапаротомия", SurgeryGroup=surgeryGroups.ElementAt(12)},
            new SurgeryOperation{Caption="Торакотомия",Title="Торакотомия", SurgeryGroup=surgeryGroups.ElementAt(12)},
            new SurgeryOperation{Caption="Коникотомия",Title="Коникотомия", SurgeryGroup=surgeryGroups.ElementAt(12)},

            new SurgeryOperation{Caption="ТИАБ", Title="ТИАБ", SurgeryGroup=surgeryGroups.ElementAt(16)},
            new SurgeryOperation{Caption="Лапароцентез",Title="Лапароцентез", SurgeryGroup=surgeryGroups.ElementAt(17)},
            new SurgeryOperation{Caption="Торакоцентез", Title="Торакоцентез", SurgeryGroup=surgeryGroups.ElementAt(17)},
        };

        static List<PhysTherMethodGroup> physTherMethodGroups = new List<PhysTherMethodGroup>
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
        static List<PhysTherMethod> physTherMethods = new List<PhysTherMethod>
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
        static List<PhysTherFactGroup> physTherFactGroups = new List<PhysTherFactGroup>
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
        static List<PhysioTherapyFactor> physioTherapyFactors = new List<PhysioTherapyFactor>
        {
            new PhysioTherapyFactor{ Title="Гальванизация", Caption="Гальванизация", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
            new PhysioTherapyFactor{ Title="Лекарственный Электрофорез", Caption="Электрофорез", PhysTherFactGroup=physTherFactGroups.ElementAt(0)},
            new PhysioTherapyFactor{ Title="Электросон", Caption="Электросон", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Диадинамотерапия", Caption="Диадинамотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Амплипульстерапия",  Caption="Амплипульстерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Интерференцтерапия",  Caption="Интерференцтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Флюктуризация",  Caption="Флюктуризация", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Электродиагностика",  Caption="Электродиагностика", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Электростимуляция",  Caption="Электростимуляция", PhysTherFactGroup=physTherFactGroups.ElementAt(1)},
            new PhysioTherapyFactor{ Title="Ультратонотерапия",  Caption="Ультратонотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
            new PhysioTherapyFactor{ Title="Местная дарсонвализация", Caption="Дарсонвализация", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
            new PhysioTherapyFactor{ Title="Индуктотермия",  Caption="Индуктотермия", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
            new PhysioTherapyFactor{ Title="Ультравысокочастотная терапия", Caption="УльтраВысокЧастТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
            new PhysioTherapyFactor{ Title="Микроволновая терапия", Caption="МикроволновТер", PhysTherFactGroup=physTherFactGroups.ElementAt(2)},
            new PhysioTherapyFactor{ Title="Франклинизация",  Caption="Франклинизация", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
            new PhysioTherapyFactor{ Title="Магнитотерапия",  Caption="Магнитотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(3)},
            new PhysioTherapyFactor{ Title="Терапия инфракрасным излученим", Caption="ИнфракраснИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
            new PhysioTherapyFactor{ Title="Терапия видимым излучением", Caption="ВидимоеИзл", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
            new PhysioTherapyFactor{ Title="Терапия ультрафиолетовым излучением", Caption="УльтрафиолетИзлуч", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
            new PhysioTherapyFactor{ Title="Лазерная терапия", Caption="ЛазернаяТер", PhysTherFactGroup=physTherFactGroups.ElementAt(4)},
            new PhysioTherapyFactor{ Title="Лечебный массаж", Caption="МассажЛечебн", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
            new PhysioTherapyFactor{ Title="Ультразвуковая терапия", Caption="УльтразвукТер", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
            new PhysioTherapyFactor{ Title="Лекарственный фонофорез", Caption="ЛекарствФоноф", PhysTherFactGroup=physTherFactGroups.ElementAt(5)},
            new PhysioTherapyFactor{ Title="Аэрозольтерапия",  Caption="Аэрозольтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Электроаэрозольтерапия", Caption="ЭлектроАэрозТер", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Аэроионтерапия",  Caption="Аэроионтерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Галотерапия",  Caption="Галотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Гипербарическая оксигенерация", Caption="ГипербарОксиген", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Климатотерапия",  Caption="Климатотерапия", PhysTherFactGroup=physTherFactGroups.ElementAt(6)},
            new PhysioTherapyFactor{ Title="Пресная вода",  Caption="Пресная вода", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
            new PhysioTherapyFactor{ Title="Минеральные соли", Caption="МинСоли", PhysTherFactGroup=physTherFactGroups.ElementAt(7)},
            new PhysioTherapyFactor{ Title="Теплолечение",  Caption="Теплолечение",PhysTherFactGroup=physTherFactGroups.ElementAt(8)},
            new PhysioTherapyFactor{ Title="Криолечение",  Caption="Криолечение", PhysTherFactGroup=physTherFactGroups.ElementAt(8)}
        };

        static List<DiagnosisClass> diagnosisClasses = new List<DiagnosisClass>();
        static List<DiagnosisGroup> diagnosisGroups = new List<DiagnosisGroup>();
        static List<Diagnosis> diagnoses = new List<Diagnosis>();

        static void CreateDiagnoses()
        {
            DiagnosisClass _diagclass = new DiagnosisClass() { Title = "Некоторые инфекционные и паразитарные заболевания", Code = "1", };
            diagnosisClasses.Add(_diagclass);
            DiagnosisGroup _diagnosisGroup = new DiagnosisGroup() { Title = "Гастроэнтерит и колит инфекционного и неопределенного происхождения", DiagnosisClass = _diagclass };
            diagnosisGroups.Add(_diagnosisGroup);
            List<Diagnosis> _diagnoses = new List<Diagnosis> {
                //Бактериальные кишечные инфекции
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Холера", Code = "1A00" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Инфекция кишечника из-за холерного вибриона", Code = "1A01" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Кишечные инфекции, вызванные Shigella", Code = "1A02" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Кишечные инфекции, вызванные Escherichia coli", Code = "1A03" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Энтеропатогенная инфекция Escherichia coli", Code = "1A03.0" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Энтеротоксигенная инфекция Escherichia coli", Code = "1A03.1" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Энтероинвазивная инфекция Escherichia coli", Code = "1A03.2" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Энтерогеморрагическая инфекция Escherichia coli", Code = "1A03.3" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Кишечные инфекции, вызванные другими уточненными Escherichia coli", Code = "1A03.Y" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Кишечные инфекции, вызванные Escherichia coli, неуточненными", Code = "1A03.Z" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Энтероколит из-за Clostridium difficile", Code = "1A04" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Инфекция кишечника из-за Yersinia enterocolitica", Code = "1A05" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит из-за Campylobacter", Code = "1A06" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Брюшной тиф", Code = "1A07" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Паратифная лихорадка", Code = "1A08" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Инфекции, вызванные другими сальмонеллами", Code = "1A09" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Абдоминальный актиномикоз", Code = "1C60.1" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Другие уточненные бактериальные кишечные инфекции", Code = "1A0Y" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Бактериальные кишечные инфекции неуточненные", Code = "1A0Z" },
                //Бактериальные интоксикации пищевого происхождения
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Пищевая стафилококковая интоксикация", Code = "1A10" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Ботулизм", Code = "1A11" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Продовольственная интоксикация ботулотоксином", Code = "1A11.0" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Другие формы ботулизма", Code = "1A11.1" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Ботулизм неуточненный", Code = "1A11.Z" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Интоксикация клостридином пищевого происхождения", Code = "1A12" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Интоксикация Bacillus cereus пищевого происхождения", Code = "1A13" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Другие уточненные бактериальные пищевые интоксикации", Code = "1A1Y" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Бактериальные пищевые интоксикации, неуточненные", Code = "1A1Z" },
                //Вирусные кишечные инфекции
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Аденовирусный энтерит", Code = "1A20" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит из-за астровируса", Code = "1A21" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит из-за ротавируса", Code = "1A22" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Норовирусный энтерит", Code = "1A23" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Инфекции кишечника, вызванные цитомегаловирусом", Code = "1A24" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Другие уточненные вирусные кишечные инфекции", Code = "1A2Y" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Вирусные кишечные инфекции неуточненные", Code = "1A2Z" },
                //Протозойные кишечные инфекции
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Инфекции, вызванные Balantidium coli", Code = "1A30" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Giardiasis", Code = "1A31" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Криптоспоридиоз", Code = "1A32" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Цистоидоспориоз", Code = "1A33" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Цистоидоспориз тонкого кишечника", Code = "1A33.0" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Цистоидоспориоз толстой кишки", Code = "1A33.1" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Другой уточненный цистоидоспориоз", Code = "1A33.Y" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Cystoisosporiasis, неуточненный", Code = "1A33.Z" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Sarcocystosis", Code = "1A34" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Бластоцистоз", Code = "1A35" },
                //Амебиаз
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Амебиаз", Code = "1А36" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Кишечный амёбиаз", Code = "1A36.0" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Внекишечный амебиаз", Code = "1A36.1" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Амебиаз неуточненны", Code = "1A36.Z" },

                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит и колит без указания инфекционного агента", Code = "1A70" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит или колит инфекционного происхождения без уточненного инфекционного агента", Code = "1A70.0" },
                new Diagnosis { DiagnosisGroup = _diagnosisGroup, Title = "Гастроэнтерит или колит без указания происхождения", Code = "1A70.1" },
            };
            diagnoses.AddRange(_diagnoses);
            _diagnosisGroup = new DiagnosisGroup() { Title = "Заболевания передаваемые преимущественно половым путем", DiagnosisClass = _diagclass };
            diagnosisGroups.Add(_diagnosisGroup);
            _diagnoses = new List<Diagnosis>()
            {
                //сифилис
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Врожденный сифилис", Code="1A90"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Ранний врожденный сифилис, симптоматический", Code="1A90.0"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Ранний врожденный сифилис, скрытый", Code="1A90.1"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Поздняя врожденная сифилитическая окулопатия", Code="1A90.2"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Поздний врожденный нейросифилис", Code="1A90.3"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Поздний врожденный сифилис, симптоматический", Code="1A90.4"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Поздний врожденный сифилис, скрытый", Code="1A90.5"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Title="Врожденный сифилис неуточненный", Code="1A90.Z "},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91", Title="Ранний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.0", Title="Первичный генитальный сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.1", Title="Первичный анальный сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.2", Title="Первичный сифилис других органов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.3", Title="Вторичный сифилис кожи или слизистых оболочек"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.4", Title="Вторичный сифилис других органов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.5", Title="Скрытый ранний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.Y", Title="Другие уточненные виды раннего сифилиса"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A91.Z ", Title="Ранний сифилис неуточненный"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92", Title="Поздний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.0", Title="Нейросифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.00", Title="Бессимптомный нейросифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.01", Title="Симптоматический поздний нейросифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.0Z", Title="Нейросифилис неуточненный"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.1", Title="Сердечно-сосудистый поздний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.2", Title="Симптоматический поздний сифилис других органов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.20", Title="Окулярный поздний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.21", Title="Поздний сифилис, связанный с костно-мышечной системой"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.22", Title="Поздний сифилис кожи или слизистых оболочек"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.2Y", Title="Симптоматический поздний сифилис других указанных сайтов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.2Z", Title="Симптоматический поздний сифилис других сайтов, неуточненный"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.Y", Title="Другой указанный поздний сифилис"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A92.Z", Title="Поздний сифилис неуточненный"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A93", Title="Скрытый сифилис, неопределенный как ранний, так и поздний"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="JB62.1", Title="Сифилис, осложняющий беременность, роды или послеродовой период"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1A9Z", Title="Сифилис неуточненный"},
                //гонококковая инфекция
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B00", Title="Гонококковая инфекция мочеполовой системы"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B00.0", Title="Гонококковая инфекция нижних мочеполовых путей без абсцедирования периуретральных или других желез"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B00.1", Title="Гонококковая инфекция нижних мочеполовых путей с абсцедированием периуретральных или других желез"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B00.Y", Title="Гонококковая инфекция других уточненных мочеполовых органов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B00.Z", Title="Гонококковая инфекция мочеполовой системы неуточненная"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B01", Title="Гонококковый пельвиоперитонит"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02", Title="Гонококковая инфекция других локализаций"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.0", Title="Гонококковая инфекция костно-мышечной системы"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.1", Title="Гонококковая инфекция прямой кишки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.2", Title="Гонококковая инфекция ануса"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.3", Title="Гонококковый фарингит"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.4", Title="Гонококковая инфекция глаз"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.Y", Title="Гонококковая инфекция других локализаций, уточненная"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B02.Z", Title="Гонококковая инфекция других локализаций, неуточненная"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B03", Title="Диссеминированная гонококковая инфекция"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B0Z", Title="Гонококковая инфекция, неуточненная"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B10", Title="Хламидийная лимфогранулема"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B11", Title="Неязвенная хламидийная инфекция, передающаяся половым путем"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B11.0", Title="Хламидийная инфекция нижних мочеполовых путей"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B11.1", Title="Хламидийная инфекция внутренних репродуктивных органов"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B11.Y", Title="Неязвенная хламидийная инфекция других уточненных локализаций, передающаяся половым путем"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B11.Z", Title="Неязвенная хламидийная инфекция, передающаяся половым путем, неуточненная"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B1Y", Title="Другие уточненные инфекции, передающиеся половым путем из-за хламидиоза"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B1Z", Title="Инфекции, передающиеся половым путем из-за хламидиоза, неуточненные"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B20", Title="Chancroid"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B21", Title="Гранулема inguinale"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B22", Title="Трихомониаз"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B23", Title="Инфекции с трансмиссивно — половым путем передачи"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1G94", Title="Чесотка"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1G94.0", Title="Классическая чесотка"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1G94.1", Title="Костная чесотка"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1G94.Y", Title="Другие неуточненные чесотки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B24", Title="Аногенитальная инфекция простого герпеса"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B24.0", Title="Простая герпетическая инфекция гениталий или урогенитального тракта"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B24.1", Title="Простая герпетическая инфекция перианальной кожи или прямой кишки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B24.Z", Title="Аногенитальная инфекция простого герпеса неуточненной локализации"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B25", Title="Аногенитальные бородавки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B25.0", Title="Анальные бородавки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B25.1", Title="Генитальные бородавки"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B25.2", Title="Экстрагенитальные кондиломы"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B2Y", Title="Другие уточненные инфекции, передающиеся половым путем"},
                new Diagnosis {DiagnosisGroup = _diagnosisGroup, Code="1B2Z", Title="Инфекции, передающиеся преимущественно половым путем, неуточненые"},

            };
            diagnoses.AddRange(_diagnoses);
        }

        static List<Adress> GenerateAdresses(int count)
        {
            Random rnd = new Random();
            List<Adress> adresses = new List<Adress>();
            string[] cities = File.ReadAllLines("DataGenerator/Cities.txt");
            string[] streets = File.ReadAllLines("DataGenerator/Streets.txt");
            for (int i = 0; i < count; i++)
            {
                var adress = new Adress { City = cities[rnd.Next(cities.Count())], Street = streets[rnd.Next(streets.Count())], Number = rnd.Next(1, 30).ToString(), Room = rnd.Next(1, 100) };
                adresses.Add(adress);
            }
            return adresses;
        }
        static List<Department> GenerateDepartments(List<DepartmentTitle> titles)
        {
            Random rnd = new Random();
            var departments = new List<Department>();
            foreach (DepartmentTitle title in titles)
            {
                var department = new Department { Title = title, Adress = adressesStaff[rnd.Next(adressesStaff.Count)], };
                departments.Add(department);
            }
            return departments;
        }
        static List<Staff> GenerateStaff(List<Department> departments, int countOnDepartment)
        {
            List<Staff> staffs = new List<Staff>();
            string[] males = File.ReadAllLines("DataGenerator/Name.Male.txt");
            string[] females = File.ReadAllLines("DataGenerator/Name.Female.txt");
            Random rnd = new Random();

            foreach (Department department in departments)
            {
                for (int i = 0; i < countOnDepartment; i++)
                {
                    string[] stringarr = females;
                    Gender gender = Gender.Женский;
                    if (rnd.Next(2) == 1)
                    {
                        stringarr = males;
                        gender = Gender.Мужской;
                    }
                    string[] words = Regex.Replace(stringarr[rnd.Next(stringarr.Count())], @"\s+", " ").Split(' ');

                    Staff staff = new Staff
                    {
                        FirstName = words[0],
                        MidName = words[1],
                        LastName = words[2],
                        Adress = adressesStaff[rnd.Next(adressesStaff.Count)],
                        Cabinet = rnd.Next(1, 50),
                        BirthDay = DateTime.Now - TimeSpan.FromDays(360 * rnd.Next(18, 80) - rnd.Next(360)),
                        Department = department,
                        PhoneNumber = 89000000000 + rnd.Next(10000000, 999999999),
                        Gender = gender,
                        IsEnabled = true,
                        WeekDays = Enum.Parse<WeekDays>(rnd.Next(5).ToString())
                    };
                    staffs.Add(staff);
                }
            }
            return staffs;
        }
        static List<Patient> GeneratePatients(int count)
        {
            Random rnd = new Random();
            List<Patient> patients = new List<Patient>();
            string[] males = File.ReadAllLines("DataGenerator/Name.Male.txt");
            string[] females = File.ReadAllLines("DataGenerator/Name.Female.txt");
            for (int i = 0; i < count; i++)
            {
                string[] stringarr = females;
                Gender gender = Gender.Женский;
                if (rnd.Next(2) == 1)
                {
                    stringarr = males;
                    gender = Gender.Мужской;
                }
                string[] words = Regex.Replace(stringarr[rnd.Next(stringarr.Count())], @"\s+", " ").Split(' ');

                Patient patient = new Patient
                {
                    FirstName = words[0],
                    MidName = words[1],
                    LastName = words[2],
                    Adress = adressesPatient[rnd.Next(adressesPatient.Count)],
                    BirthDay = DateTime.Now - TimeSpan.FromDays(360 * rnd.Next(18, 80) - rnd.Next(360)),
                    PhoneNumber = 89000000000 + rnd.Next(10000000, 999999999),
                    Gender = gender,
                    Belay = belays[rnd.Next(belays.Count)],
                    BelayCode = rnd.Next(1000000, 9999999),
                    BelayDateOut = DateTime.Now + TimeSpan.FromDays(360 * rnd.Next(1, 8) + rnd.Next(360)),
                    HasChild = rnd.Next(2) == 0,
                    IsMarried = rnd.Next(2) == 0
                };
                patients.Add(patient);
            }
            return patients;
        }
        static List<Change> GenerateChanges(List<Staff> staffs, DateTime dateStart, DateTime dateEnd)
        {
            Random rnd = new Random();
            List<Change> changes = new List<Change>();
            foreach (Staff staff in staffs)
            {
                TimeSpan span = TimeSpan.FromMinutes(rnd.Next(2, 12) * 5);
                int[] tst = { 8, 13, 17 };
                int timeStart = tst[rnd.Next(tst.Length)];
                TimeSpan workSpan = TimeSpan.FromHours(rnd.Next(6, 8));

                switch (staff.WeekDays)
                {
                    case WeekDays.FiveTwo:
                        for (DateTime _start = dateStart; _start < dateEnd; _start += TimeSpan.FromDays(1))
                        {
                            if (_start.DayOfWeek != DayOfWeek.Saturday && _start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                DateTime time = new DateTime(_start.Year, _start.Month, _start.Day, timeStart, 0, 0);
                                Change change = new Change(time, time + workSpan, span) { Staff = staff };
                                changes.Add(change);
                            }
                        }
                        break;
                    case WeekDays.TwoTwo:
                        for (DateTime _start = dateStart + TimeSpan.FromDays(rnd.Next(3)); _start < dateEnd; _start += TimeSpan.FromDays(4))
                        {
                            DateTime timeOne = new DateTime(_start.Year, _start.Month, _start.Day, timeStart, 0, 0);
                            DateTime timeTwo = timeOne + TimeSpan.FromDays(1);
                            Change changeOne = new Change(timeOne, timeOne + workSpan, span) { Staff = staff };
                            Change changeTwo = new Change(timeTwo, timeTwo + workSpan, span) { Staff = staff };
                            changes.Add(changeOne);
                            changes.Add(changeTwo);
                        }
                        break;
                    case WeekDays.FourTwo:
                        for (DateTime _start = dateStart + TimeSpan.FromDays(rnd.Next(3)); _start < dateEnd; _start += TimeSpan.FromDays(6))
                        {
                            DateTime time = new DateTime(_start.Year, _start.Month, _start.Day, timeStart, 0, 0);
                            for (int i = 0; i < 4; i++)
                            {
                                Change change = new Change(time, time + workSpan, span) { Staff = staff };
                                changes.Add(change);
                                time += TimeSpan.FromDays(1);
                            }
                        }
                        break;
                    case WeekDays.Even:
                        for (DateTime _start = dateStart + TimeSpan.FromDays(rnd.Next(3)); _start < dateEnd; _start += TimeSpan.FromDays(2))
                        {
                            var _startTwo = _start + TimeSpan.FromDays(1);
                            DateTime timeOne = new DateTime(_start.Year, _start.Month, _start.Day, 8, 0, 0);
                            DateTime timeTwo = new DateTime(_startTwo.Year, _startTwo.Month, _startTwo.Day, 13, 0, 0);
                            Change changeOne = new Change(timeOne, timeOne + workSpan, span) { Staff = staff };
                            Change changeTwo = new Change(timeTwo, timeTwo + workSpan, span) { Staff = staff };
                            changes.Add(changeOne);
                            changes.Add(changeTwo);
                        }
                        break;
                    case WeekDays.Odd:
                        for (DateTime _start = dateStart; _start < dateEnd; _start += TimeSpan.FromDays(1))
                        {
                            if (_start.DayOfWeek == DayOfWeek.Saturday || _start.DayOfWeek == DayOfWeek.Sunday)
                            {
                                DateTime time = new DateTime(_start.Year, _start.Month, _start.Day, timeStart, 0, 0);
                                Change change = new Change(time, time + workSpan, span) { Staff = staff };
                                changes.Add(change);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return changes;
        }

        public static void Invoke(IDbContextFactory<HospitalDbContext> contextFactory)
        {
            using (HospitalDbContext db = contextFactory.CreateDbContext())
            {
                //List<Department> departments = GenerateDepartments(departmentTitles);
                //List<Staff> staffs = GenerateStaff(departments, 3);
                //List<Patient> patients = GeneratePatients(200);
                //List<Change> changes = GenerateChanges(staffs, DateTime.Now, DateTime.Now + TimeSpan.FromDays(60));

                //CreateDiagnoses();

                //db.DepartmentTitles.AddRange(departmentTitles);
                //db.Departments.AddRange(departments);
                //db.Belays.AddRange(belays);
                //db.Patients.AddRange(patients);
                //db.Staffs.AddRange(staffs);
                //db.Changes.AddRange(changes);
                ////db.MedCards.AddRange(medCards);
                ////db.Entries.AddRange(entries);

                //db.TestTypes.AddRange(testTypes);
                //db.Tests.AddRange(tests);
                ////db.TestDatas.AddRange(testDatas);

                //db.DrugClasses.AddRange(drugClasses);
                //db.DrugSubClasses.AddRange(drugSubClasses);
                //db.DrugGroups.AddRange(drugGroups);
                //db.DrugSubGroups.AddRange(drugSubGroups);
                //db.Drugs.AddRange(drugs);
                ////db.PharmacoTherapyDatas.AddRange(pharmacoTherapyDatas);

                //db.PhysTherMethodGroups.AddRange(physTherMethodGroups);
                //db.PhysTherMethods.AddRange(physTherMethods);
                //db.PhysTherFactGroups.AddRange(physTherFactGroups);
                //db.PhysioTherapyFactors.AddRange(physioTherapyFactors);
                ////db.PhysioTherapyDatas.AddRange(physioTherapyDatas);

                //db.SurgeryGroups.AddRange(surgeryGroups);
                //db.SurgeryOperations.AddRange(surgeryOperations);
                ////db.SurgeryTherapyDatas.AddRange(surgeryTherapyDatas);

                //db.DiagnosisClasses.AddRange(diagnosisClasses);
                //db.DiagnosisGroups.AddRange(diagnosisGroups);
                //db.Diagnoses.AddRange(diagnoses);

                //db.SaveChanges();

            }
        }

    }
}
