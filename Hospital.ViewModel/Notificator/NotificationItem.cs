namespace Hospital.ViewModel.Notificator
{
    public enum NotificationType
    {
        Information,
        Success,
        Warning,
        Error,
        Dialog
    }


    public class NotificationItem
    {
        public NotificationType Type { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsManualClose { get; set; }
        public int Hold { get; set; } = 5000;
    }
}
