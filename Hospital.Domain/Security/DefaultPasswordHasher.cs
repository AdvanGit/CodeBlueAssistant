using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace Hospital.Domain.Security
{
    public class DefaultPasswordHasher : IPasswordHasher
    {
        public string GetPasswordHash(long phoneNumber, string passwordString)
        {
            if (string.IsNullOrEmpty(passwordString))
            {
                throw new ArgumentNullException(nameof(passwordString));
            }

            byte[] salt = Encoding.ASCII.GetBytes(phoneNumber.ToString());

            string result = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordString,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 500,
                numBytesRequested: 256 / 8));

            return result;
        }
    }
}
