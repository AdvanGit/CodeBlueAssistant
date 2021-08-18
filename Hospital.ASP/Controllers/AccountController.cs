using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IGenericRepository<Patient> _dataServicesPatient;

        public AccountController(IAuthenticationService<Patient> authenticationService, IGenericRepository<Patient> dataServicesPatient)
        {
            _authenticationService = authenticationService;
            _dataServicesPatient = dataServicesPatient;
        }

        public async Task<IActionResult> Index()
        {
            if (User.HasClaim(p => p.Type == "phoneNumber"))
            {
                var patients = await _dataServicesPatient.GetWhere(p => p.PhoneNumber.ToString() == User.FindFirstValue("phoneNumber"));
                return View(patients.FirstOrDefault());
            }
            ModelState.AddModelError("", "Ошибка идентификации");
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
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


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignIn(Patient patient)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, patient.FirstName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "patient"),
                new Claim("phoneNumber", patient.PhoneNumber.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
