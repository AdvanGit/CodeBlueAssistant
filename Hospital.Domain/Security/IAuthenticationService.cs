using Hospital.Domain.Model;
using System.Threading.Tasks;

namespace Hospital.Domain.Security
{
    public interface IAuthenticationService<TUser> where TUser : User
    {
        Task<TUser> Register(TUser user, string password);
        Task<TUser> Authenticate(long phoneNumber, string password);
        Task<TUser> ChangePassword(long phoneNumber, string oldPassword, string newPassword);
    }
}
