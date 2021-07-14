using Hospital.Domain.Model;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Notificator;
using Hospital.ViewModel.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class LoginViewModel : MainViewModel
    {
        IAuthenticator _authenticator;

        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public async Task<bool> CheckUser(long phoneNumber)
        {
            IsLoading = true;
            try
            {
                var role = await _authenticator.Login(phoneNumber, "");
                if (Role.Administrator == role)
                {
                    CurrentStuffId = _authenticator.CurrentUser.Id;
                    HeaderCaption = _authenticator.CurrentUser.FirstName + " " + _authenticator.CurrentUser.MidName[0] + ". " + _authenticator.CurrentUser.LastName[0] + ".";
                    return true;
                }
                else
                {
                    NotificationManager.AddItem(new NotificationItem(NotificationType.Information, TimeSpan.FromSeconds(4), "Запись не найдена"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                NotificationManager.AddException(ex, 8);
                return false;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
