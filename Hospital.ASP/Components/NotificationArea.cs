using Hospital.ASP.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.ASP.Components
{
    public class NotificationArea : ViewComponent
    {
        private readonly INotificationService _notificationService;

        public NotificationArea(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IViewComponentResult Invoke()
        {
            if (_notificationService.IsExist)
            {
                ViewBag.Message = _notificationService.Message;
                ViewBag.Type = _notificationService.NotificationType;
                return View();
            }
            else if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Type = TempData["notificationType"];
                return View();
            }
            else return Content(string.Empty);
        }
    }
}
