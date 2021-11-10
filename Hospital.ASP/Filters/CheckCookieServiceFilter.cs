using Hospital.ASP.Services;
using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.ViewModel;
using Hospital.ViewModel.Notificator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ASP.Filters
{
	public class CheckCookieServiceFilter : IAsyncActionFilter
	{
		private readonly IGenericRepository<Patient> _patientRepository;
		private readonly INotificationService _notificationService;

        public CheckCookieServiceFilter(IGenericRepository<Patient> patientRepository, INotificationService notificationService)
        {
            _patientRepository = patientRepository;
            _notificationService = notificationService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			int.TryParse(context.HttpContext.User.FindFirst("id")?.Value, out int id);
			long.TryParse(context.HttpContext.User.FindFirst("phoneNumber")?.Value, out long phoneNumber);
			//var timeStamp = context.HttpContext.User.FindFirst("timeStamp")?.Value;

			if (id == 0 || phoneNumber == 0 || !(await _patientRepository.GetWhere(u => u.Id == id && u.PhoneNumber == phoneNumber)).Any())
			{
				await context.HttpContext.SignOutAsync();

				var controller = context.Controller as Controller;
				if (controller == null) return;

				_notificationService.AddError("Ошибка cookie, одно из утверждений не соответствует действительности. Авторизируйтесь повторно");
				_notificationService.ApplyForRedirect(controller.TempData);
				context.Result = new RedirectResult("/Account/Login");
            }
			else await next();
		}
	}
}
