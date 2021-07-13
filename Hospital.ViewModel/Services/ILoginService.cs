using Hospital.Domain.Security;

namespace Hospital.ViewModel.Services
{
    public interface ILoginService
    {
        IAccount GetAccount(long PhoneNumber, string password);


    }
}
