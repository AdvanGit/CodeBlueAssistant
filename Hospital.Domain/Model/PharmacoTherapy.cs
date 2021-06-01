using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public enum DrugForm { таблетки, капсулы, микструра, раствор, порошок, ампулы }

    public class PharmacoTherapyData : TherapyBase, ITherapyData
    {
        private MedCard _medCard;
        private Drug _drug;
        private string _trademark;
        private string _dose;
        private DateTime _dateCreate;
        private Treatment _treatment;

        public MedCard MedCard { get => _medCard; set { _medCard = value; OnPropertyChanged(nameof(MedCard)); } }
        public Drug Drug { get => _drug; set { _drug = value; OnPropertyChanged("Drug"); } }
        public string Trademark { get => _trademark; set { _trademark = value; OnPropertyChanged(nameof(Trademark)); OnPropertyChanged(nameof(Option)); } }
        public string Dose { get => _dose; set { _dose = value; OnPropertyChanged("Dose"); OnPropertyChanged(nameof(Value)); } }
        public DateTime DateCreate { get => _dateCreate; set { _dateCreate = value; OnPropertyChanged("DateCreate"); } }
        public Treatment Treatment { get => _treatment; set { _treatment = value; OnPropertyChanged(nameof(Treatment)); } }

        [NotMapped]
        public string Option { get => Trademark; set => Trademark = value; }
        [NotMapped]
        public string Value { get => Dose; set => Dose = value; }
        [NotMapped]
        public string Title => Drug.Substance;
        [NotMapped]
        public string Group => Drug.DrugSubGroup.DrugGroup.DrugSubClass.Title;
        [NotMapped]
        public DateTime Entry => DateCreate;
        [NotMapped]
        public string Type => "Фармакотерапия";
    }

    public class Drug : DomainObject
    {
        private string _title;
        private string _substance;
        private DrugForm _drugForm;
        private DrugSubGroup _drugSubGroup;

        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Substance { get => _substance; set { _substance = value; OnPropertyChanged("Substance"); } }
        public DrugForm DrugForm { get => _drugForm; set { _drugForm = value; OnPropertyChanged("DrugForm"); } }
        public DrugSubGroup DrugSubGroup { get => _drugSubGroup; set { _drugSubGroup = value; OnPropertyChanged("DrugSubGroup"); } }
    }

    public class DrugSubGroup : DomainObject
    {
        private string _title;
        private string _caption;
        private bool _isReciept;
        private DrugGroup _drugGroup;

        public ObservableCollection<Drug> Drugs { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged("Caption"); } }
        public bool IsReciept { get => _isReciept; set { _isReciept = value; OnPropertyChanged("IsReciept"); } }
        public DrugGroup DrugGroup { get => _drugGroup; set { _drugGroup = value; OnPropertyChanged("DrugGroup"); } }
    }

    public class DrugGroup : DomainObject
    {
        private string _title;
        private string _caption;
        private DrugSubClass _drugSubClass;

        public ObservableCollection<DrugSubGroup> DrugSubGroups { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public string Caption { get => _caption; set { _caption = value; OnPropertyChanged("Caption"); } }
        public DrugSubClass DrugSubClass { get => _drugSubClass; set { _drugSubClass = value; OnPropertyChanged("DrugSubClass"); } }
    }

    public class DrugSubClass : DomainObject
    {
        private string _title;
        private DrugClass _drugClass;

        public ObservableCollection<DrugGroup> DrugGroups { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
        public DrugClass DrugClass { get => _drugClass; set { _drugClass = value; OnPropertyChanged("DrugClass"); } }
    }

    public class DrugClass : DomainObject
    {
        private string _title;

        public ObservableCollection<DrugSubClass> DrugSubClasses { get; set; }
        public string Title { get => _title; set { _title = value; OnPropertyChanged("Title"); } }
    }
}
