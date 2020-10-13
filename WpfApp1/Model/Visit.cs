using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1.Model
{
    public class Visit : INotifyPropertyChanged
    {

        public DateTime VisitDateTime { get; }

        private Diagnosis currentDiagnosis;
        public Diagnosis CurrentDiagnosis
        {
            get
            {
                return currentDiagnosis;
            }

            set
            {
                currentDiagnosis = value;
                OnPropertyChanged("CurrentDiagnosis");
            }
        }

        private string conclusion;
        public string Conclusion
        {
            get
            {
                return conclusion;
            }
            set
            {
                conclusion = value;
                OnPropertyChanged("Conclusion");
            }
        }

        private string recomendation;
        public string Recomendation
        {
            get
            {
                return recomendation;
            }
            set
            {
                recomendation = value;
                OnPropertyChanged("Recomendation");
            }
        }

        public ObservableCollection<Inspect> InspectList { get; set;}
        public ObservableCollection<Symptom> SimptomList { get; set; }
        public ObservableCollection<ProcQueue> ProcQueues { get; set; }

        // Лист Анализов
        // ------ Код анализа ------ Название анализа ------ Норма ------Результат

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class Inspect:INotifyPropertyChanged
    {
        private Checkup checkup;
        private string definition;
        private string result;

        public Checkup Checkup
        {
            get
            {
                return checkup;
            }
            set
            {
                checkup = value;
                OnPropertyChanged("Checkup");
            }
        }
        public string Definition
        {
            get
            {
                return definition;
            }
            set
            {
                definition = value;
                OnPropertyChanged("Definition");
            }
        }
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }

    }

    public struct Symptom : INotifyPropertyChanged
    {
        private Inspect inspect;
        private string conclusion;

        public Inspect Inspect
        {
            get
            {
                return inspect;
               }
            set
            {
                inspect = value;
                OnPropertyChanged("Inspect");
            }
        }
        public string Conclusion
        {
            get
            {
                return conclusion;
            }
            set
            {
                conclusion = value;
                OnPropertyChanged("Conclusion");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }


}
