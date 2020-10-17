﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital.Domain.Model
{
    public class Presence : ModelBase
    {
        private Entry _entyIn;
        private Entry _entryOut;
        private Diagnosis _diagnosis;
        private string _conclusion;
        private string _recomendation;

        public int Id { get; set; }

        public Entry EntryIn
        {
            get => _entyIn;
            set
            {
                _entyIn = value;
                OnPropertyChanged("EntryIn");
            }
        }
        public int EntryInId { get; set; }

        public ObservableCollection<TestData> TestDatas { get; set; }
        public ObservableCollection<ProcAsset> ProcAssets { get; set; }

        public Diagnosis Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }
        }
        public int DiagnosisId { get; set; }
        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                OnPropertyChanged("Conclusion");
            }
        }
        public string Recomendation
        {
            get => _recomendation;
            set
            {
                _recomendation = value;
                OnPropertyChanged("Recomendation");
            }
        }

        public Entry EntryOut
        {
            get => _entryOut;
            set
            {
                _entryOut = value;
                OnPropertyChanged("EntryOut");
            }
        }
        public int? EntryOutId { get; set; }

    }
}