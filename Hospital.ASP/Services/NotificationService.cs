
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace Hospital.ASP.Services
{
    public class NotificationService : INotificationService
    {
        public string NotificationType { get; private set; }
        public string Message { get; private set; }

        public bool IsExist => !string.IsNullOrEmpty(Message);

        public void AddError(string message)
        {
            NotificationType = "danger";
            Message = message;
        }

        public void AddInfo(string message)
        {
            NotificationType = "info";
            Message = message;
        }

        public void AddWarning(string message)
        {
            NotificationType = "warning";
            Message = message;
        }

        public void AddSuccess(string message)
        {
            NotificationType = "success";
            Message = message;
        }

        public void ApplyForRedirect(ITempDataDictionary tempData)
        {
            tempData["message"] = Message;
            tempData["notificationType"] = NotificationType;
        }
    }
}
