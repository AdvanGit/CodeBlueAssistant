using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Hospital.ASP.Services
{
    public interface INotificationService
    {
        public string NotificationType { get; }
        public string Message { get; }
        public bool IsExist { get; }

        void AddError(string message);
        void AddInfo(string message);
        void AddWarning(string message);
        void AddSuccess(string message);

        /// <summary>
        /// Use it for redirect. Initializing current notification state as TempData
        /// </summary>
        void ApplyForRedirect(ITempDataDictionary tempData);
    }
}
