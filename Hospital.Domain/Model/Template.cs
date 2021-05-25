using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public string JsonObjects { get; 
            set; }

        [NotMapped] //хранение в int потому как нужно будет делать запрос к актуальным тестам, хранение объектов нецелесообразно
        public IEnumerable<int> Objects
        {
            get { return JsonObjects == null ? null : JsonSerializer.Deserialize<IEnumerable<int>>(JsonObjects); }
            set { JsonObjects = JsonSerializer.Serialize(value, typeof(DomainObject)); }
        }

        [NotMapped] //только запись
        public IEnumerable<Test> Test
        {
            set { JsonObjects = JsonSerializer.Serialize(value.Select(v => v.Id).Cast<int>()); }
        }

    }
}
