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
        public NotificationType Type { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public TimeSpan Hold { get; set; }
    }
}
