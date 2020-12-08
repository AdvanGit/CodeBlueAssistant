using Hospital.EntityFramework;
using Hospital.UI.Services;
using System.Threading.Tasks;

public enum UserAccess { admin, doctor, registrator, manager }

namespace Hospital.UI.ViewModels
{
    public class MainViewModel
    {
        private RelayCommand _createData;

        public RelayCommand CreateData
        {
            get => _createData ?? (_createData = new RelayCommand(obj =>
                { Task.Run(() => (new ContentCreate(new HospitalDbContextFactory()))); }));
        }
    }
}
