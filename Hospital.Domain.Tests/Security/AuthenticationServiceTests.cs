using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Hospital.Domain.Tests.Security
{
    public class AuthenticationServiceTests
    {
        const long expectedPhoneNumber = 89999999999;
        const string password = "testPassword";

        Mock<IPasswordHasher> mockPasswordHasher = new Mock<IPasswordHasher>();
        Mock<IGenericRepository<Patient>> mockPatientRepository = new Mock<IGenericRepository<Patient>>();

        [Fact]
        public async Task Authenticate_WithExistPhoneNumberAndValidPassword_ReturnPatient()
        {
            // Arrange
            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, password)).Returns("testPasswordHash");
            mockPatientRepository.Setup(m => m.GetWhere(p => p.PhoneNumber == expectedPhoneNumber && p.PasswordHash == "testPasswordHash")).ReturnsAsync(new List<Patient> { new Patient() { PhoneNumber = expectedPhoneNumber } });
            IAuthenticationService<Patient> authenticationService = new AuthenticationService<Patient>(mockPasswordHasher.Object, mockPatientRepository.Object);

            // Act
            Patient patient = await authenticationService.Authenticate(expectedPhoneNumber, password);

            // Assert
            Assert.NotNull(patient);
            Assert.Equal(expectedPhoneNumber, patient.PhoneNumber);
        }

        [Fact]
        public async Task Authenticate_DontExistPhoneNumberOrInValidPassword_ReturnNull()
        {
            // Arrange
            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, password)).Returns("testPasswordHash");
            mockPatientRepository.Setup(m => m.GetWhere(p => p.PhoneNumber == expectedPhoneNumber && p.PasswordHash == "testPasswordHash")).ReturnsAsync(new List<Patient>());
            IAuthenticationService<Patient> authenticationService = new AuthenticationService<Patient>(mockPasswordHasher.Object, mockPatientRepository.Object);

            // Act
            Patient patient = await authenticationService.Authenticate(expectedPhoneNumber, password);

            // Assert
            Assert.Null(patient);
        }

        [Fact]
        public async Task ChangePassword_ExistPhoneNumberAndValidPassword_ReturnPatient()
        {
            //Arrange
            Patient patient = new Patient() { PhoneNumber = expectedPhoneNumber, PasswordHash = "oldPasswordHash", Id = 1 };

            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, "oldPassword")).Returns("oldPasswordHash");
            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, "newPassword")).Returns("newPasswordHash");

            mockPatientRepository.Setup(m => m.GetWhere(p => p.PhoneNumber == expectedPhoneNumber && p.PasswordHash == "oldPasswordHash")).ReturnsAsync(new List<Patient> { patient }); ;
            mockPatientRepository.Setup(m => m.Update(1, patient)).ReturnsAsync(patient);

            IAuthenticationService<Patient> authenticationService = new AuthenticationService<Patient>(mockPasswordHasher.Object, mockPatientRepository.Object);

            //Act
            Patient patientResult = await authenticationService.ChangePassword(expectedPhoneNumber, "oldPassword", "newPassword");

            //Assert
            Assert.Equal(expectedPhoneNumber, patientResult.PhoneNumber);
            Assert.Equal("newPasswordHash", patientResult.PasswordHash);
        }

        [Fact]
        public async Task ChangePassword_DontExistPhoneNumberOrInvalidPassword_ReturnException()
        {
            //Arrange
            Patient patient = new Patient() { PhoneNumber = expectedPhoneNumber, PasswordHash = "oldPasswordHash", Id = 1 };

            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, "oldPassword")).Returns("oldPasswordHash");
            mockPasswordHasher.Setup(m => m.GetPasswordHash(expectedPhoneNumber, "newPassword")).Returns("newPasswordHash");

            mockPatientRepository.Setup(m => m.GetWhere(p => p.PhoneNumber == expectedPhoneNumber && p.PasswordHash == "oldPasswordHash")).ReturnsAsync(new List<Patient>() ); ;
            mockPatientRepository.Setup(m => m.Update(1, patient)).ReturnsAsync(patient);

            IAuthenticationService<Patient> authenticationService = new AuthenticationService<Patient>(mockPasswordHasher.Object, mockPatientRepository.Object);

            //Act
            Exception exeption = await Assert.ThrowsAsync<Exception>(() => authenticationService.ChangePassword(expectedPhoneNumber, "oldPassword", "newPassword")) ;

            //Assert
            Assert.Equal("номер или пароль неверны", exeption.Message);
        }

    }
}
