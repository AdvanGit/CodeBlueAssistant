using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Hospital.ViewModel;
using Hospital.ViewModel.Notificator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ASP.Filters
{
	public class CheckCookieServiceFilter : IAsyncActionFilter
	{
		private readonly IGenericRepository<Patient> _patientRepository;
		public CheckCookieServiceFilter(IGenericRepository<Patient> patientRepository)
		{
			_patientRepository = patientRepository;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			int.TryParse(context.HttpContext.User.FindFirst("id")?.Value, out int id);
			long.TryParse(context.HttpContext.User.FindFirst("phoneNumber")?.Value, out long phoneNumber);
			//var lastChanged = context.HttpContext.User.FindFirst("lastChanged")?.Value;

			var controller = context.Controller as Controller;
			if (controller == null) return;
			controller.ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), "Ошибка cookie, одно из утверждений не соответствует действительности. Авторизируйтесь повторно");

			if (id == 0 || phoneNumber == 0 || !(await _patientRepository.GetWhere(u => u.Id == id && u.PhoneNumber == phoneNumber)).Any())
			{
				await context.HttpContext.SignOutAsync();
				context.Result = new ViewResult()
				{
					ViewName = "Error",
					ViewData = new ViewDataDictionary(controller.ViewData)
					{
						Model = new ErrorViewModel()
						{
							//TODO: implement errorinfo, edit ViewModel
						}
					}
				};
			}
			else await next();
		}
	}
}
