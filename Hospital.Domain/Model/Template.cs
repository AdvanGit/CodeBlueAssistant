using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Model
{
    public class TestTemplate : DomainObject
    {
        private Department _owner;
        private TestType _category;
        private string _title;

        public Department Owner { get => _owner; set { _owner = value; OnPropertyChanged(nameof(Owner)); } }
        public TestType Category { get => _category; set { _category = value; OnPropertyChanged(nameof(Category)); } }
        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }

        public string JsonObjects { get; set; }

        [NotMapped]
        public ICollection<int> Objects
        {
            get { return JsonObjects == null ? null : JsonConvert.DeserializeObject<ICollection<int>>(JsonObjects); }
            set { JsonObjects = JsonConvert.SerializeObject(value); }
        }
    }
}
