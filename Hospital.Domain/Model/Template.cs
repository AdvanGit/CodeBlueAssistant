using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hospital.Domain.Model
{
    public class Template<TEntity> : DomainObject where TEntity : DomainObject
    {
        private Department _owner;
        private TEntity _category;

        public Department Owner { get => _owner; set => _owner = value; }
        public TEntity Category { get => _category; set => _category = value; }

        public string _Objects { get; set; }

        //ignore
        public ICollection<TEntity> Objects
        {
            get { return _Objects == null ? null : JsonConvert.DeserializeObject<ICollection<TEntity>>(_Objects); }
            set { _Objects = JsonConvert.SerializeObject(value); }
        }
    }
}
