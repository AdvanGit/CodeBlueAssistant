using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Hospital.ASP.Services
{
    /// <summary>
    /// Use TempData for Redirect
    /// </summary>
    public interface INotificationService
    {
        public string NotificationType { get; }
        public string Message { get; }
        public bool IsExist { get; }

        void AddError(string message, ITempDataDictionary tempData = null);
        void AddInfo(string message, ITempDataDictionary tempData = null);
        void AddWarning(string message, ITempDataDictionary tempData = null);
        void AddSuccess(string message, ITempDataDictionary tempData = null);
    }
}
