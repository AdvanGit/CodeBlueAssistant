namespace Hospital.Domain.Security
{
    public interface IPasswordHasher
    {
        string GetPasswordHash(long phoneNumber, string passwordString);
    }
}
