using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Notificator
{
    public static class NotificationManager 
    {
        private static ICollection<NotificationItem> notificationQueue = new List<NotificationItem>();
        private static bool isLocked;
        private static string message;
        private static bool isOpen;

        public static string Message { get => message; private set { message = value; NotifyStaticPropertyChanged(nameof(Message)); } }
        public static bool IsOpen { get => isOpen; private set { isOpen = value; NotifyStaticPropertyChanged(nameof(IsOpen)); } }

        public async static void AddItem(NotificationItem item)
        {
            notificationQueue.Add(item);
            if (!isLocked) await Show();
        }

        private async static Task Show()
        {
            isLocked = true;
            while (notificationQueue.Count > 0)
            {
                var item = notificationQueue.First();
                Message = item.Message;
                IsOpen = true;
                await Task.Delay(item.Hold);
                IsOpen = false;
                notificationQueue.Remove(item);
                await Task.Delay(200);
            }
            isLocked = false;
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
