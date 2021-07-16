using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital.ViewModel.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IAuthenticator _authenticator;
        private readonly IDbContextFactory<HospitalDbContext> _contextFactory;

        private readonly AmbulatoryViewModelFactory _ambulatoryViewModelFactory;
        private readonly ScheduleDataService _scheduleDataServices;

        public RootViewModelFactory(
            IAuthenticator authenticator,
            IDbContextFactory<HospitalDbContext> contextFactory,
            AmbulatoryViewModelFactory ambulatoryViewModelFactory,
            ScheduleDataService scheduleDataServices)
        {
            _authenticator = authenticator;
            _contextFactory = contextFactory;
            _ambulatoryViewModelFactory = ambulatoryViewModelFactory;
            _scheduleDataServices = scheduleDataServices;
        }

        public LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_authenticator);
        }

        public ScheduleViewModel CreateScheduleViewModel()
        {
            return new ScheduleViewModel(_scheduleDataServices, _ambulatoryViewModelFactory);
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
