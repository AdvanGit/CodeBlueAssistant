namespace Hospital.Domain.Security
{
    public class TestPasswordHasher : IPasswordHasher
    {
        public string GetPasswordHash(string passwordString)
        {
            return passwordString;
        }
    }
}
