using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Documents;

namespace WpfApp1.Model
{
    public enum Gender : byte {male, female}

    public class User : INotifyPropertyChanged
    {
        private string _firstName;
        private string _midName;
        private string _lastName;
        private long _phoneNumber;
        private DateTime _birthDay;
        //private Adress _adress;
        private Gender _gender;
        private DateTime _createDate;

        public int Id { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string MidName
        {
            get { return _midName; }
            set
            {
                _midName = value;
                OnPropertyChanged("MidName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public long PhoneNumeber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public DateTime BirthDay
        {
            get => _birthDay;
            set
            {
                _birthDay = value;
                OnPropertyChanged("BirthDay");
            }
        }
        public DateTime CreateDate
        {
            get => _createDate;
            set
            {
                _createDate = value;
                OnPropertyChanged("CreateDate");
            }
        }

        //public Adress Adress
        //{
        //    get => _adress;
        //    set
        //    {
        //        _adress = value;
        //        OnPropertyChanged("Adress");
        //    }

        //} 
        public Gender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public class Staff : User
    {
        private string _password;
        private Department _department;
        private string _qualification;
        private bool _isEnabled;


        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        public string Qualification
        {
            get { return _qualification; }
            set
            {
                _qualification = value;
                OnPropertyChanged("Qualification");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }
    }

    public class Patient : User
    {
        private Belay _belay;
        private int _belayCode;
        private DateTime _belayDateOut;
        private bool _isMarried;
        private bool _hasChild;

        public Belay Belay
        {
            get => _belay;
            set
            {
                _belay = value;
                OnPropertyChanged("Belay");
            }
        }
        public int BelayCode
        {
            get => _belayCode;
            set
            {
                _belayCode = value;
                OnPropertyChanged("BelayCode");
            }
        }
        public DateTime BelayDateOut
        {
            get => _belayDateOut;
            set
            {
                _belayDateOut = value;
                OnPropertyChanged("BelayDateOut");
            }
        }
        public bool IsMarried
        {
            get => _isMarried;
            set
            {
                _isMarried = value;
                OnPropertyChanged("IsMarried");
            }
        }
        public bool HasChild
        {
            get => _hasChild;
            set
            {
                _hasChild = value;
                OnPropertyChanged("HasChild");
            }
        }
    }

    public class Belay : INotifyPropertyChanged
    {
        private string _title;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public List<Patient> Patients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }

    public struct Adress : INotifyPropertyChanged
    {
        private string _city, _street;
        private int _number, _subNumber, _room;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }
        public int Number
        {
            get => _number;
                set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        public int SubNumber
        {
            get => _subNumber;
            set
            {
                _subNumber = value;
                OnPropertyChanged("SubNumber");
            }
        }
        public int Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged("Room");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}