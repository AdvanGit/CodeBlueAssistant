
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace Hospital.ASP.Services
{
    public class NotificationService : INotificationService
    {
        public string CssClassString { get; private set; }
        public string Message { get; private set; }

        public bool IsExist => !string.IsNullOrEmpty(Message);

        public void AddError(string message)
        {
            CssClassString = "danger";
            Message = message;
        }

        public void AddInfo(string message)
        {
            CssClassString = "info";
            Message = message;
        }

        public void AddWarning(string message)
        {
            CssClassString = "warning";
            Message = message;
        }

        public void AddSuccess(string message)
        {
            CssClassString = "success";
            Message = message;
        }

        public void ApplyForRedirect(ITempDataDictionary tempData)
        {
            tempData["message"] = Message;
            tempData["notificationType"] = CssClassString;
        }
    }
}
