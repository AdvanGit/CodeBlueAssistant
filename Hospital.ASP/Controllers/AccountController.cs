using Hospital.ASP.Filters;
using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Hospital.ViewModel.Notificator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
	//TODO: checkusercookie attribute pipeline
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAuthenticationService<Patient> _authenticationService;
		private readonly IGenericRepository<Patient> _patientRepository;
		private readonly IGenericRepository<Belay> _belayRepository;

		public AccountController(IAuthenticationService<Patient> authenticationService, IGenericRepository<Patient> patientRepository, IGenericRepository<Belay> belayRepository)
		{
			_authenticationService = authenticationService;
			_patientRepository = patientRepository;
			_belayRepository = belayRepository;
		}

		public async Task<IActionResult> Index()
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var patient = (await _patientRepository.GetWithInclude(p => p.Id == id, p => p.Belay)).FirstOrDefault();
				if (!patient.IsValid())
                {
					ViewBag.NotificationItem = new NotificationItem(NotificationType.Warning, new TimeSpan(), "Личная информация указана не полностью, некорые функции могут быть не доступны");
				}
				return View(patient);
			}
			ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), "Ошибка идентификации");
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Edit()
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var patient = (await _patientRepository.GetWithInclude(p => p.Id == id, p => p.Belay)).FirstOrDefault();
				if (patient != null)
				{
					var belays = await _belayRepository.GetAll();
					ViewBag.BelaysList = new SelectList(belays, "Id", "Title", belays.Where(b => b.Id == patient.Belay.Id).FirstOrDefault());
					return View(patient);
				}
				else
				{
					ViewBag.NotificationItem = new NotificationItem(NotificationType.Warning, new TimeSpan(), "Такой пользователь не найден");
					return View();
				}
			}
			ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), "Ошибка cookie: не найдено подходящее утверждение");
			return View();
		}

		//TODO: BelayId prop
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ServiceFilter(typeof(CheckCookieServiceFilter))]
		public async Task<IActionResult> Edit(Patient patient)
		{
			if (ModelState.IsValid)
			{
				if (int.TryParse(User.FindFirstValue("id"), out int id))
				{
					var res = (await _patientRepository.GetWithInclude(p => p.Id == id)).FirstOrDefault();
					var belay = await _belayRepository.GetById(patient.Belay.Id);
					patient.PasswordHash = res.PasswordHash;
					patient.CreateDate = res.CreateDate;
					patient.Belay = belay;
					var user = await _patientRepository.Update(id, patient);

					await HttpContext.SignOutAsync();
					await SignIn(user);
					TempData["message"] = "информация обновлена";
					TempData["type"] = "Success";

				}
				else
				{
					ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), "ошибка cookies, утверждение не найдено");
				}
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.BelaysList = await GetBelaysSelectList(patient.Belay.Id);
				return View(patient);
			}
		}

		public IActionResult Security()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(Patient patient, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				Patient user = await _authenticationService.Authenticate(patient.PhoneNumber, patient.PasswordHash);
				if (user != null)
				{
					await SignIn(user);

					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), "Некорректные логин или пароль");
			}
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(Patient patient, string password, string confirmPassword)
		{
			if (ModelState.IsValid)
			{
				if (password != confirmPassword)
					ModelState.AddModelError("", "пароли не совпадают");
				else if ((await _patientRepository.GetWhere(s => s.PhoneNumber == patient.PhoneNumber)).FirstOrDefault() != null)
					ModelState.AddModelError("", "номер уже используется");

				if (ModelState.IsValid)
				{
					var user = await _authenticationService.Register(patient, password);
					await SignIn(user);
					RedirectToAction("Index", "Account");
				}
				return View();
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ServiceFilter(typeof(CheckCookieServiceFilter))]
		public async Task<IActionResult> ChangePassword(long phoneNumber, string oldPassword, string newPassword, string confirmPassword)
        {
			if (newPassword == confirmPassword)
            {
                try 
                {
					await _authenticationService.ChangePassword(phoneNumber, oldPassword, newPassword);
					ViewBag.NotificationItem = new NotificationItem(NotificationType.Success, new TimeSpan(), "Пароль успешно обновлен");
				}
                catch (Exception ex)
                {
					ViewBag.NotificationItem = new NotificationItem(NotificationType.Error, new TimeSpan(), ex.Message);
				}
			}
            else
            {
				ViewBag.NotificationItem = new NotificationItem(NotificationType.Warning, new TimeSpan(), "Пароли не совпадают");
			}
			return View("Security");
        }

		private async Task SignIn(Patient patient)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, patient.FirstName),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, "patient"),
				new Claim("phoneNumber", patient.PhoneNumber.ToString()),
				new Claim("shortName", patient.GetShortName()),
				new Claim("id", patient.Id.ToString())
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		private async Task<SelectList> GetBelaysSelectList(int currentItemId)
        {
			var belays = await _belayRepository.GetAll();
			return new SelectList(belays, "Id", "Title", belays.Where(b => b.Id == currentItemId).FirstOrDefault());
		}

	}
}
