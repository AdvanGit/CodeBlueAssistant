using Hospital.Domain.Model;
using Hospital.Domain.Security;
using Hospital.Domain.Services;
using Hospital.EntityFramework.Services;
using System.Security.Claims;

namespace Hospital.ViewModel.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly AmbulatoryViewModelFactory _ambulatoryViewModelFactory;

        private readonly IAuthenticationService<Staff> _authenticationService;
        private readonly ClaimsPrincipal _claimsPrincipal;

        private readonly IGenericRepository<Belay> _belayRepository;
        private readonly IGenericRepository<Patient> _patientRepository;
        private readonly ScheduleDataService _scheduleDataServices;
        private readonly EntryDataService _entryDataService;


        public RootViewModelFactory(
            AmbulatoryViewModelFactory ambulatoryViewModelFactory,
            IAuthenticationService<Staff> authenticationService,
            ClaimsPrincipal claimsPrincipal,
            IGenericRepository<Belay> belayRepository,
            IGenericRepository<Patient> patientRepository,
            ScheduleDataService scheduleDataServices,
            EntryDataService entryDataService)
        {
            _ambulatoryViewModelFactory = ambulatoryViewModelFactory;
            _authenticationService = authenticationService;
            _claimsPrincipal = claimsPrincipal;
            _belayRepository = belayRepository;
            _patientRepository = patientRepository;
            _scheduleDataServices = scheduleDataServices;
            _entryDataService = entryDataService;
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
            return new RegistratorViewModel(_belayRepository, _patientRepository, _entryDataService);
        }
    }
}
