
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace Hospital.ASP.Services
{
    public class NotificationService : INotificationService
    {
        public string NotificationType { get; private set; }
        public string Message { get; private set; }

        public bool IsExist => !string.IsNullOrEmpty(Message);

        public void AddError(string message, ITempDataDictionary tempData = null)
        {
            NotificationType = "danger";
            Message = message;
            SetTempData(tempData);
        }

        public void AddInfo(string message, ITempDataDictionary tempData = null)
        {
            NotificationType = "info";
            Message = message;
            SetTempData(tempData);
        }

        public void AddWarning(string message, ITempDataDictionary tempData = null)
        {
            NotificationType = "warning";
            Message = message;
            SetTempData(tempData);
        }

        public void AddSuccess(string message, ITempDataDictionary tempData = null)
        {
            NotificationType = "success";
            Message = message;
            SetTempData(tempData);
        }

        private void SetTempData(ITempDataDictionary tempData)
        {
            if (tempData != null)
            {
                tempData["message"] = Message;
                tempData["notificationType"] = NotificationType;
            }
        }
    }
}
