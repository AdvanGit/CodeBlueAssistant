using Hospital.EntityFramework;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModel
{
    public class VisitVM : INotifyPropertyChanged
    {
        private readonly HospitalDbContextFactory contexFactory = new HospitalDbContextFactory();

        public VisitVM()
        {
            //new ContentCreate(contexFactory);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
