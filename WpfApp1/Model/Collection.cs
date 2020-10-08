using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1.Model
{ 
    public class Diagnosis : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));}

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        
        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
    }

    public class Checkup : INotifyPropertyChanged
    {
        private string title;
        private string normal;
        private string normalFemale;
        private Department department;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Name");
            }
        }
        public string Normal
        {
            get
            {
                return normal;
            }
            set
            {
                normal = value;
                OnPropertyChanged("Normal");
            }
        }
        public string NormalFemale
        {
            get
            {
                if (normalFemale == null)
                {
                    normalFemale = normal;
                    return normalFemale; 
                }
                else return normalFemale;
            }
            set
            {
                normalFemale = value;
                OnPropertyChanged("NormalFemale");
            }
        }
        public Department Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
                OnPropertyChanged("Department");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class Department : INotifyPropertyChanged
    {
        private string title;
        private string adress;
        private Staff manager;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }
        public Staff Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}

