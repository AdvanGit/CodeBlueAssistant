using System;

namespace Hospital.Domain.Model
{
    public enum EntryStatus { Open, Closed, OutOfDate }

    public class Entry : ModelBase
    {
        private EntryStatus _entryStatus;
        private Patient _patient;
        private Staff _destination;
        private DateTime _targetDateTime;


        public DateTime CreateDateTime { get; }
        public int Id { get; set; }
        public int Chain { get; set; }

        public Patient Patient { get => _patient; set { _patient = value; OnPropertyChanged("Patient"); } }
        public int PatientId { get; set; }

        public Staff Destination { get => _destination; set { _destination = value; OnPropertyChanged("Destination"); } }
        public int DestinationId { get; set; }
       
        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }

        public EntryStatus EntryStatus { get => _entryStatus; set { _entryStatus = value; OnPropertyChanged("EntryStatus"); } }
    }
}