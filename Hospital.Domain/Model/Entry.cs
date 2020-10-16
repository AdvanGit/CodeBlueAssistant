using System;

namespace Hospital.Domain.Model
{
    public enum EntryStatus { Open, Closed, OutOfDate }

    public class Entry : ModelBase
    {
        private DateTime _dateCreate;
        private EntryStatus _entryStatus;
        private Schedule _schedule;
        private Presence _origin;
        private Presence _resume;

        public int Id { get; set; }
        public int ChainId { get; set; }
        public DateTime DateCreate {get => _dateCreate; set { _dateCreate = value; OnPropertyChanged("DateCreate"); } }
        public Presence Origin { get => _origin; set { _origin = value; OnPropertyChanged("Origin"); } }
        public Presence Resume { get => _resume; set { _resume = value; OnPropertyChanged("Resume"); } }
        public Schedule Schedule { get => _schedule; set { _schedule = value; OnPropertyChanged("Schedule"); } }
        public EntryStatus EntryStatus { get => _entryStatus; set { _entryStatus = value; OnPropertyChanged("EntryStatus"); } }

    }
}