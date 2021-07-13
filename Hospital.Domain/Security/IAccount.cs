namespace Hospital.Domain.Security
{
    public enum Role { Unspecified, Doctor, Registrator, Manager, Administrator }

    public interface IAccount
    {
        int Id { get; }

        Role GetRole();
    }
}
