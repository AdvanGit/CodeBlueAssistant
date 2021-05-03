using Hospital.Domain.Model;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged

    {
        internal static int CurrentStuffId { get; private set; }

        public async static Task<bool> GetUser(long phoneNumber)
        {
            var item = (await new GenericDataServices<Staff>(new HospitalDbContextFactory())
                .GetWhere(s => s.PhoneNumber == phoneNumber,
                         (message) => NotificationManager.AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(3), message))))
                .FirstOrDefault();
            if (item != null) 
            {
                CurrentStuffId = item.Id; 
                return true;
            }
            else return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
