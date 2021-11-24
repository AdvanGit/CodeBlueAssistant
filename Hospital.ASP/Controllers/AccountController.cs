using Hospital.ASP.Filters;
using Hospital.ASP.Services;
using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
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
	//TODO: Belay to BelayId prop refactoring
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAuthenticationService<Patient> _authenticationService;
		private readonly IGenericRepository<Patient> _patientRepository;
		private readonly IGenericRepository<Belay> _belayRepository;
		private readonly INotificationService _notificationService;

		public AccountController(IAuthenticationService<Patient> authenticationService, IGenericRepository<Patient> patientRepository, IGenericRepository<Belay> belayRepository, INotificationService notificationService)
		{
			_authenticationService = authenticationService;
			_patientRepository = patientRepository;
			_belayRepository = belayRepository;
			_notificationService = notificationService;
		}

		public async Task<IActionResult> Index()
		{
			if (int.TryParse(User.FindFirstValue("id"), out int id))
			{
				var patient = (await _patientRepository.GetWithInclude(p => p.Id == id, p => p.Belay)).FirstOrDefault();
				if (!patient.IsValid())
				{
					_notificationService.AddWarning("Личная информация указана не полностью, некорые функции могут быть не доступны");
				}
				return View(patient);
			}
			_notificationService.AddError("Ошибка идентификации");
			_notificationService.ApplyForRedirect(TempData);
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
					_notificationService.AddWarning("Такой пользователь не найден");
					return View();
				}
			}
			_notificationService.AddWarning("Ошибка cookie: не найдено подходящее утверждение");
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken, ServiceFilter(typeof(CheckCookieServiceFilter))]
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
					_notificationService.AddSuccess("Данные успешно сохранены");
					_notificationService.ApplyForRedirect(TempData);
					await HttpContext.SignOutAsync();
					await SignIn(user);
				}
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.BelaysList = await GetBelaysSelectList(patient.Belay.Id);
				return View(patient);
			}
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
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
				_notificationService.AddWarning("Некорректные логин или пароль");
			}
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public async Task<IActionResult> Register()
		{
			var belays = await _belayRepository.GetAll();
			ViewBag.BelaysList = new SelectList(belays, "Id", "Title");
			return View();
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
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
					var belay = await _belayRepository.GetById(patient.Belay.Id);
					patient.Belay = belay;
					var user = await _authenticationService.Register(patient, password);
					await SignIn(user);
					RedirectToAction("Index", "Account");
				}
				return View();
			}
			return View();
		}

		public IActionResult Security()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken, ServiceFilter(typeof(CheckCookieServiceFilter))]
		public async Task<IActionResult> ChangePassword(long phoneNumber, string oldPassword, string newPassword, string confirmPassword)
		{
			if (newPassword == confirmPassword)
			{
				try
				{
					await _authenticationService.ChangePassword(phoneNumber, oldPassword, newPassword);
					_notificationService.AddSuccess("Пароль успешно обновлен");
				}
				catch (Exception ex)
				{
					_notificationService.AddWarning(ex.Message);
				}
			}
			else
			{
				_notificationService.AddWarning("Пароли не совпадают");
			}
			return View("Security");
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> GetDemoAccounts()
		{
			IEnumerable<Patient> patients = await _patientRepository.GetWhere(p => p.Id == 75 || p.Id == 1);
			return PartialView("_DemoAccountsPartial", patients);
		}

		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginDemo(Patient patient)
		{
			if (ModelState.IsValid)
			{
				Patient user = await _patientRepository.GetById(patient.Id);
				if (user != null && user.PhoneNumber == patient.PhoneNumber)
				{
					await SignIn(user);
					return RedirectToAction("Index", "Home");
				}
				_notificationService.AddError("Некорректные данные демо-аккаунта");
			}
			else
			{
				_notificationService.AddError("Некорректные данные запроса");
			}
			_notificationService.ApplyForRedirect(TempData);
			return RedirectToAction("Login");
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
