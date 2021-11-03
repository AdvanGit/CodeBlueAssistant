using Hospital.Domain.Model;
using Hospital.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ASP.Filters
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IGenericRepository<User> _userRepository;

        public CustomCookieAuthenticationEvents(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            var id = int.Parse(userPrincipal.FindFirst("id")?.Value);
            var phoneNumber = long.Parse(userPrincipal.FindFirst("phoneNumber")?.Value);
            var lastChanged = userPrincipal.FindFirst("lastChanged")?.Value;
            var isNullOrEmpty = id == 0 && phoneNumber == 0;

            if (isNullOrEmpty && !(await _userRepository.GetWhere(u => u.Id == id && u.PhoneNumber == phoneNumber)).Any())
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
