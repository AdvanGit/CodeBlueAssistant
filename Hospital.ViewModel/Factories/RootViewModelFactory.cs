using Hospital.Domain.Services;
using Hospital.EntityFramework;
using Hospital.EntityFramework.Services;
using Hospital.ViewModel.Ambulatory;
using Hospital.ViewModel.Services;

namespace Hospital.ViewModel.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IAuthenticator _authenticator;

        private readonly AmbulatoryViewModelFactory _ambulatoryViewModelFactory;
        private readonly EntryDataService _entryDataService;
        private readonly ScheduleDataService _scheduleDataServices;

        public RootViewModelFactory(
            IAuthenticator authenticator, 
            AmbulatoryViewModelFactory ambulatoryViewModelFactory,
            EntryDataService entryDataService,
            ScheduleDataService scheduleDataServices)
        {
            _authenticator = authenticator;
            _ambulatoryViewModelFactory = ambulatoryViewModelFactory;
            _scheduleDataServices = scheduleDataServices;
            _entryDataService = entryDataService;
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
            return new RegistratorViewModel(_entryDataService);
        }
    }
}
