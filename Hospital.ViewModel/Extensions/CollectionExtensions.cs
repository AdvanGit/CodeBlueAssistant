using Hospital.Domain.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hospital.ViewModel.Extensions
{
    public static class CollectionExtensions
    {
        public static int GetEqualsCount<T>(this ObservableCollection<T> collection, string prop, string value) where T : DomainObject
        {
            return collection.Count();
        }
    }
}
