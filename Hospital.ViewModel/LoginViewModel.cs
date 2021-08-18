using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.ViewModel.Notificator;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class LoginViewModel : MainViewModel
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly IAuthenticationService<Staff> _authenticationService;

        public LoginViewModel(ClaimsPrincipal claimsPrincipal, IAuthenticationService<Staff> authenticationService)
        {
            _claimsPrincipal = claimsPrincipal;
            _authenticationService = authenticationService;
        }

        public async Task<Staff> Login(long phoneNumber, string password)
        {
            IsLoading = true;
            try
            {
                var staff = await _authenticationService.Authenticate(phoneNumber, password);
                if (staff == null) 
                {
                    throw new UnauthorizedAccessException("Запись не найдена");
                }

                var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, staff.Role.ToString()),
                        new Claim(ClaimsIdentity.DefaultNameClaimType, staff.FirstName),
                        new Claim("FirstName", staff.FirstName),
                        new Claim("MidName", staff.MidName),
                        new Claim("LastName", staff.LastName),
                        new Claim("ShortName", staff.GetShortName()),
                        new Claim("Id", staff.Id.ToString())
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "Password" );
                _claimsPrincipal.AddIdentity(claimsIdentity);
                HeaderCaption = _claimsPrincipal.FindFirst(c=>c.Type == "ShortName").Value;
                return staff;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public ClaimsPrincipal GetPrincipal() => _claimsPrincipal;
    }
}
