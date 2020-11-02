using System;
using System.Collections.ObjectModel;

namespace Hospital.Domain.Model
{
    public enum DrugForm {таблетки, капсулы, микструра, раствор, порошок, ампулы}
    public enum Treatment {Этиотропная, Патогенетическая, Симптоматическая, Заместительная, Профилактическая}

    public class PharmacoTherapyData : ModelBase
    {
        private Visit _visit;
        private Drug _drug;
        private string _dose;
        private Treatment _treatment;
        private DateTime _dateCreate;

        public int Id { get; set; }
        public Visit Visit { get => _visit; set { _visit = value; OnPropertyChanged("Visit"); } }
        public Drug Drug { get => _drug; set { _drug = value; OnPropertyChanged("Drug"); } }
        public string Dose { get => _dose; set { _dose = value; OnPropertyChanged("Dose"); } }
        public Treatment Treatment { get => _treatment; set { _treatment = value; OnPropertyChanged("Treatment"); } }
        public DateTime DateCreate { get => _dateCreate; set { _dateCreate = value; OnPropertyChanged("DateCreate"); } }
    }

    public class Drug : ModelBase
    {
        private string _title;
        private string _substance;
        private DrugForm _drugForm;
        // trademarks list jsonparse
        private DrugSubGroup _drugSubGroup;

        public int Id { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Substance { get => _substance; set { _substance = value; OnPropertyChanged("Substance"); } }
        public DrugForm DrugForm { get => _drugForm; set { _drugForm = value; OnPropertyChanged("DrugForm"); } }
        public DrugSubGroup DrugSubGroup { get => _drugSubGroup; set { _drugSubGroup = value; OnPropertyChanged("DrugSubGroup"); } }
    }

    public class DrugSubGroup : ModelBase
    {
        private string _title;
        private string _shortTitle;
        private bool _isReciept;
        private DrugGroup _drugGroup;

        public int Id { get; set; }
        public ObservableCollection<Drug> Drugs { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string ShortTitle { get => _shortTitle; set { _shortTitle = value; OnPropertyChanged("ShortTitle"); } }
        public bool IsReciept { get => _isReciept; set { _isReciept = value; OnPropertyChanged("IsReciept"); } }
        public DrugGroup DrugGroup { get => _drugGroup; set { _drugGroup = value; OnPropertyChanged("DrugGroup"); } }
    }

    public class DrugGroup : ModelBase
    {
        private string _title;
        private string _shortTitle;
        private DrugSubClass _drugSubClass;

        public int Id { get; set; }
        public ObservableCollection<DrugSubGroup> DrugSubGroups { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string ShortTitle { get => _shortTitle; set { _shortTitle = value; OnPropertyChanged("ShortTitle"); } }
        public DrugSubClass DrugSubClass { get => _drugSubClass; set { _drugSubClass = value; OnPropertyChanged("DrugSubClass"); } }
    }

    public class DrugSubClass : ModelBase
    {
        private string _title;
        private DrugClass _drugClass;

        public int Id { get; set; }
        public ObservableCollection<DrugGroup> DrugGroups { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public DrugClass DrugClass { get => _drugClass; set { _drugClass = value; OnPropertyChanged("DrugClass"); } }
    }

    public class DrugClass : ModelBase
    {
        private string _title;

        public int Id { get; set; }
        public ObservableCollection<DrugSubClass> DrugSubClasses { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
    }
}
