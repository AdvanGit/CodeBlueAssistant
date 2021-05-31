using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Notificator
{
    public static class NotificationManager
    {
        private static ICollection<NotificationItem> notificationQueue = new List<NotificationItem>();
        private static bool isLocked;

        private static bool _isOpen;
        private static string _message;
        private static NotificationType _type;
        private static TimeSpan _span = TimeSpan.FromSeconds(3);

        public static bool IsOpen { get => _isOpen; private set { _isOpen = value; NotifyStaticPropertyChanged(nameof(IsOpen)); } }
        public static string Message { get => _message; private set { _message = value; NotifyStaticPropertyChanged(nameof(Message)); } }
        public static NotificationType Type { get => _type; private set { _type = value; NotifyStaticPropertyChanged(nameof(Type)); } }
        public static TimeSpan Span { get => _span; set { _span = value; NotifyStaticPropertyChanged(nameof(Span)); } }

        private static CancellationTokenSource source = new CancellationTokenSource();
        private static CancellationToken token = source.Token;

        private async static void Show()
        {
            isLocked = true;
            while (notificationQueue.Count > 0)
            {
                var item = notificationQueue.First();
                Message = item.Message;
                Type = item.Type;
                Span = item.Hold;
                IsOpen = true;
                try
                {
                    await Task.Delay(item.IsManual ? TimeSpan.FromMilliseconds(-1) : item.Hold, token);
                }
                catch (TaskCanceledException) { }
                IsOpen = false;
                notificationQueue.Remove(item);
                await Task.Delay(200);
            }
            isLocked = false;
        }

        public static void AddException(Exception exception, int timeSpan = 0, bool isHold = false)
        {
            AddItem(new NotificationItem(NotificationType.Error, TimeSpan.FromSeconds(timeSpan),
                exception.GetType().Name + ": " + exception.Message, false, isHold));
        }
        public static void AddItem(NotificationItem item)
        {
            if (item.IsStop && notificationQueue.Count != 0)
            {
                Cancel();
                notificationQueue.Clear();
            }
            notificationQueue.Add(item);
            if (!isLocked) Show();
        }
        public static void Cancel()
        {
            source.Cancel();
            source = new CancellationTokenSource();
            token = source.Token;
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = delegate { };
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
