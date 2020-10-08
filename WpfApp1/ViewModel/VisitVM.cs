using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public class VisitVM : INotifyPropertyChanged
    {
        public Staff staff { get; }
        public ObservableCollection<Checkup> Checkups { get; }
        public ObservableCollection<Diagnosis> DiagnosisList { get; }
        public ObservableCollection<Inspect> Inspects { get; }
  

        public Model.Visit CurrentVisit { get; }
        public string Test { get; set; }

        public Diagnosis CurDiagnosis
        {
            get
            {
                return CurrentVisit.CurrentDiagnosis;
            }
            set
            {
                CurrentVisit.CurrentDiagnosis = value;
                OnPropertyChanged("CurDiagnosis");
            }
        }

        public VisitVM()
        {
            Checkups = CreateDataCheck();
            DiagnosisList = CreateDiagList();
            Inspects = CreateInspList();

            CurrentVisit = new Model.Visit { InspectList=Inspects,  CurrentDiagnosis=DiagnosisList.ElementAt(0), Conclusion = "Человек явно здоров но показывает недовольный вид", Recomendation = "Пить, Курить, Случать Моргенштерна" };
        }

        private static ObservableCollection<Checkup> CreateDataCheck()
        {
            return new ObservableCollection<Checkup>
                {
                new Checkup {Title = "Пульс", Normal = "75"},
                new Checkup {Title= "Давление", Normal = "120/80"},
                new Checkup {Title= "Температура", Normal = "36.6"},
                new Checkup {Title= "Осмотр"}
                };
        }
        private static ObservableCollection<Diagnosis> CreateDiagList()
        {
            return new ObservableCollection<Diagnosis>
            {
                new Diagnosis {Description = "Нет", Code="000"},
                new Diagnosis {Description = "Здоров", Code="001"},
                new Diagnosis {Description = "Вылечен", Code="002"},
                new Diagnosis {Description = "Грипп", Code="141"},
                new Diagnosis {Description = "Недержание мочи", Code="142"},
                new Diagnosis {Description = "Малярия", Code="143"},
                new Diagnosis {Description = "Перелом", Code="144"},
                new Diagnosis {Description = "Расстройство пищеварения", Code="145"},
                new Diagnosis {Description = "Тромбоэмболия легочной артерии", Code="146"},
                new Diagnosis {Description = "Камни в почках", Code="147"},
            };
        }
        private static ObservableCollection<Inspect> CreateInspList()
        {
            return new ObservableCollection<Inspect>
            {
                new Inspect { Checkup = new Checkup { Title = "wierd", Normal = "full" }, Definition = "property1", Result="not Bad"},
                new Inspect { Checkup = new Checkup { Title = "wierd2", Normal = "full" }, Definition = "property11", Result="very Bad"},

            };
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
