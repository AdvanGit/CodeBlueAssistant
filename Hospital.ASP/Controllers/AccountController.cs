using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ASP.Controllers
{
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

        //Refactoring with int.tryparse(id) - see Edit action
        public async Task<IActionResult> Index()
        {
            if (User.HasClaim(p => p.Type == "phoneNumber"))
            {
                var patients = await _patientRepository.GetWithInclude(p => p.PhoneNumber.ToString() == User.FindFirstValue("phoneNumber"), p => p.Belay);
                return View(patients.FirstOrDefault());
            }
            ModelState.AddModelError("", "Ошибка идентификации");
            return View();
        }

        //TODO:Antiforgery Token on Get Request?
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
                    ModelState.AddModelError("", "Такой пользователь не найден");
                    return View();
                }
            }
            ModelState.AddModelError("", "Ошибка cookie: не найдено подходящее утверждение");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            return RedirectToAction("Index");
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
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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

                if ((await _patientRepository.GetWhere(s=>s.PhoneNumber == patient.PhoneNumber)).FirstOrDefault() != null)
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
    }
}
