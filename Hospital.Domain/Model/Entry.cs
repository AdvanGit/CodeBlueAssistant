using System;

namespace Hospital.Domain.Model
{
    public enum EntryStatus { Open, Closed, OutOfDate }

    public class Entry : ModelBase
    {
        private DateTime _createDateTime;
        private EntryStatus _entryStatus;
        private Patient _patient;
        private Presence _origin;
        private Presence _resume;
        private Staff _destination;
        private DateTime _targetDateTime;

        public int Id { get; set; }
        public int Chain { get; set; }
        //public Patient Patient { get => _patient; set { _patient = value;OnPropertyChanged("Patient"); } }
        public Staff Destination { get => _destination; set { _destination = value; OnPropertyChanged("Destination"); } }
        public EntryStatus EntryStatus { get => _entryStatus; set { _entryStatus = value; OnPropertyChanged("EntryStatus"); } }

        public DateTime TargetDateTime { get => _targetDateTime; set { _targetDateTime = value; OnPropertyChanged("TargetDateTime"); } }
        public DateTime CreateDateTime {get => _createDateTime; set { _createDateTime = value; OnPropertyChanged("CreateDateTime"); } }


        public Presence Origin { get => _origin; set { _origin = value; OnPropertyChanged("Origin"); } }
        public int OriginId { get; set; }

        public Presence Resume { get => _resume; set { _resume = value; OnPropertyChanged("Resume"); } }
        public int ResumeId { get; set; }


    }
}