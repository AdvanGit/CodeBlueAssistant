using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital.ViewModel.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IDbContextFactory<HospitalDbContext> _contextFactory;

        private readonly AmbulatoryViewModelFactory _ambulatoryViewModelFactory;
        private readonly ScheduleDataService _scheduleDataServices;
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly IAuthenticationService<Staff> _authenticationService;

        public RootViewModelFactory(
            IDbContextFactory<HospitalDbContext> contextFactory,
            AmbulatoryViewModelFactory ambulatoryViewModelFactory,
            ScheduleDataService scheduleDataServices,
            IAuthenticationService<Staff> authenticationService,
            ClaimsPrincipal claimsPrincipal = null)
        {
            _contextFactory = contextFactory;
            _ambulatoryViewModelFactory = ambulatoryViewModelFactory;
            _scheduleDataServices = scheduleDataServices;
            _claimsPrincipal = claimsPrincipal;
            _authenticationService = authenticationService;
        }

        public LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_claimsPrincipal, _authenticationService);
        }

        public ScheduleViewModel CreateScheduleViewModel()
        {
            return new ScheduleViewModel(_scheduleDataServices, _ambulatoryViewModelFactory, _claimsPrincipal);
        }

        public MainViewModel CreateMainViewModel()
        {
            return new MainViewModel();
        }

        public RegistratorViewModel CreateRegistratorViewModel()
        {
            return new RegistratorViewModel(_contextFactory);
        }
    }
}
