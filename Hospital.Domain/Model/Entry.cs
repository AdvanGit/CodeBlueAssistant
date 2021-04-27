using System;

namespace Hospital.Domain.Model
{
    public enum EntryStatus { Открыта = 0, Закрыта = 1, Отмена = 2, Ожидание = 3, Неявка = 4, Резерв = 5}
    public enum InviteStatus { Первичное = 0, Повторное = 1, Трансфер = 2, Срочное = 3, Профилактика = 4}

    public class Entry : DomainObject
    {
        private EntryStatus _entryStatus;
        private InviteStatus _inviteStatus;

        private MedCard _medCard;
        private Patient _patient;
        private Staff _registrator;
        private Staff _doctorDestination;
        private DateTime _targetDateTime;
        private DateTime _createDateTime;
        private Entry _entryOut;

        public EntryStatus EntryStatus { get => _entryStatus; set { _entryStatus = value; OnPropertyChanged("EntryStatus"); } }
        public InviteStatus InviteStatus { get => _inviteStatus; set { _inviteStatus = value; OnPropertyChanged(nameof(InviteStatus)); } }

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }
        //public int? MedCardId { get; set; }

        public Patient Patient { get => _patient; set { _patient = value; OnPropertyChanged("Patient"); } }
        //public int PatientId { get; set; }

        public Staff Registrator { get => _registrator; set { _registrator = value; OnPropertyChanged(nameof(Registrator)); } }
        //public int RegistratorId { get; set; }
        public Staff DoctorDestination { get => _doctorDestination; set { _doctorDestination = value; OnPropertyChanged(nameof(DoctorDestination)); } }

        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public DateTime CreateDateTime { get => _createDateTime; set { _createDateTime = value; OnPropertyChanged(nameof(CreateDateTime)); } }

        public Entry EntryOut { get => _entryOut; set { _entryOut = value; OnPropertyChanged(nameof(EntryOut)); } }
        //public int? EntryOutId { get; set; }
    }
}