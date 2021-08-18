namespace Hospital.Domain.Security
{
    public interface IPasswordHasher
    {
        string GetPasswordHash(string passwordString);
    }
}
