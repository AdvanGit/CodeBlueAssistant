using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfApp1.Model
{

    public class Staff : INotifyPropertyChanged

    {
        private string firstName;
        private string midName;
        private string lastName;
        private int phoneNumber;
        private string password;
        private Department myDepartment;

        public Department MyDepartment
        {
            get
            {
                return myDepartment;
            }
            set
            {
                myDepartment = value;
                OnPropertyChanged("MyDepartment");
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string MidName
        {
            get { return midName; }
            set
            {
                midName = value;
                OnPropertyChanged("MidName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public int PhoneNumeber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
