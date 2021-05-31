using System;

namespace Hospital.ViewModel.Notificator
{
    public enum NotificationType
    {
        Information,
        Success,
        Warning,
        Error,
    }


    public struct NotificationItem
    {
        public NotificationItem(NotificationType type, TimeSpan hold, string message, bool isStop = false, bool isManual = false)
        {
            Type = type;
            Message = message;
            Hold = hold;
            IsStop = isStop;
            IsManual = isManual;
        }

        public NotificationType Type { get; }

        public string Message { get; }

        public TimeSpan Hold { get; }
        public bool IsStop { get; }
        public bool IsManual { get; }
    }
}
