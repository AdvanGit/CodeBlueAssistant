using Hospital.ViewModel.Ambulatory;

namespace Hospital.ViewModel.Factories
{
    public interface IRootViewModelFactory<TViewModelType> where TViewModelType : MainViewModel
    {
        TViewModelType CreateViewModel();
    }

    public interface IRootViewModelFactory
    {
        LoginViewModel CreateLoginViewModel();
        ScheduleViewModel CreateScheduleViewModel();
        MainViewModel CreateMainViewModel();
        RegistratorViewModel CreateRegistratorViewModel();
    }

}
