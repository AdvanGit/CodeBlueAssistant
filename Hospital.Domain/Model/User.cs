﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Hospital.Domain.Model
{
    public enum Gender { Мужской = 0, Женский = 1 }
    public enum WeekDays { FiveTwo, TwoTwo, FourTwo, Even, Odd }
    public enum Role { Administrator, Manager, Ambulatorer, Stationeer, Registrator }


    public abstract class User : DomainObject
    {
        private string _firstName;
        private string _midName;
        private string _lastName;
        private long _phoneNumber;
        private DateOnly _birthDay;
        private Gender? _gender;
        private DateTime _createDate;
        private string _passwordHash;

        public string PasswordHash { get => _passwordHash; set => _passwordHash = value; }

        
        [Required(ErrorMessage = "не указана фамилия")]
        [Display(Name = "Фамилия")]
        [StringLength(30, ErrorMessage = "{0} длинна должна быть от {2} до {1} символов", MinimumLength = 2)]
        [RegularExpression(@"^[А-я][а-я-]*$", ErrorMessage = "Неверный формат")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }


        [Required(ErrorMessage = "не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(30, ErrorMessage = "{0} длинна должна быть от {2} до {1} символов", MinimumLength = 2)]
        [RegularExpression(@"^[А-я][а-я-]*$", ErrorMessage = "Неверный формат")]
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

        [Required(ErrorMessage = "не указан номер телефона")]
        [Range(10000, 99999999999, ErrorMessage = "длинна должна быть от 5 до 11 символов")]
        public long PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        [Required(ErrorMessage = "не указана дата рождения")]
        public DateOnly BirthDay
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

        public string _Adress { get; set; }
        public Adress Adress
        {
            get { return _Adress == null ? null : JsonSerializer.Deserialize<Adress>(_Adress); }
            set { _Adress = JsonSerializer.Serialize(value); }
        }

        public Gender? Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string GetShortName() => _firstName + " " + _midName[0] + ". " + _lastName[0] + ".";
        public string GetLongName() => _firstName + " " + _midName + " " + _lastName;
    }

    public class Staff : User
    {
        private bool _isEnabled;
        private string _qualification;
        private WeekDays _weekDays;
        private int _cabinet;
        private Department _department;
        private Role _role;

        public ObservableCollection<Entry> Registrators { get; set; }
        public ObservableCollection<Entry> DoctorDestinations { get; set; }

        public bool IsEnabled { get => _isEnabled; set { _isEnabled = value; OnPropertyChanged("IsEnabled"); } }
        public string Qualification { get => _qualification; set { _qualification = value; OnPropertyChanged("Qualification"); } }
        public WeekDays WeekDays { get => _weekDays; set { _weekDays = value; OnPropertyChanged("WeekDays"); } }
        public int Cabinet { get => _cabinet; set { _cabinet = value; OnPropertyChanged("Cabinet"); } }
        public Department Department { get => _department; set { _department = value; OnPropertyChanged("Department"); } }
        public Role Role { get => _role; set { _role = value; OnPropertyChanged(nameof(Role)); } }
    }

    public class Patient : User
    {
        private Belay _belay;
        private int? _belayCode;
        private DateTime? _belayDateOut;

        private bool? _isMarried;
        private bool? _hasChild;

        //TODO: isConfirmed

        public Belay Belay { get => _belay; set { _belay = value; OnPropertyChanged("Belay"); } }
        public int? BelayCode { get => _belayCode; set { _belayCode = value; OnPropertyChanged("BelayCode"); } }
        public DateTime? BelayDateOut { get => _belayDateOut; set { _belayDateOut = value; OnPropertyChanged("BelayDateOut"); } }
        public bool? IsMarried { get => _isMarried; set { _isMarried = value; OnPropertyChanged("IsMarried"); } }
        public bool? HasChild { get => _hasChild; set { _hasChild = value; OnPropertyChanged("HasChild"); } }


        public bool IsValid() => _belay != null && _belayCode != null && _belayDateOut != null && _isMarried != null && _hasChild != null && Gender != null;
    }
}