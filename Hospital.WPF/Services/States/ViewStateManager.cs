using Hospital.ViewModel.Factories;
using Hospital.WPF.Navigators;
using Hospital.WPF.Views;
using System;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace Hospital.WPF.Services.States
{
    public class ViewStateManager
    {
        private readonly IRootViewModelFactory _viewModelFactory;

        public Navigator Navigator { get; }

        public ViewStateManager(IRootViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            Navigator = new Navigator(new ObservableCollection<INavigatorItem>() { new Login(viewModelFactory.CreateLoginViewModel()) });
            Navigator.CurrentBody = Navigator.Bodies[0];
        }

        public void Setup(IPrincipal _claimPrincipal)
        {
            if (_claimPrincipal.Identity.IsAuthenticated)
            {
                if (_claimPrincipal.IsInRole("Administrator"))
                {
                    Navigator.Bodies.Clear();
                    Navigator.Bodies.Add(new Registrator(_viewModelFactory.CreateRegistratorViewModel()));
                    Navigator.Bodies.Add(new Schedule(_viewModelFactory.CreateScheduleViewModel()));
                }
                else if (_claimPrincipal.IsInRole("Registrator"))
                {
                    Navigator.Bodies.Clear();
                    Navigator.Bodies.Add(new Registrator(_viewModelFactory.CreateRegistratorViewModel()));
                }
                else if (_claimPrincipal.IsInRole("Ambulatorer"))
                {
                    Navigator.Bodies.Clear();
                    Navigator.Bodies.Add(new Schedule(_viewModelFactory.CreateScheduleViewModel()));
                }
                else throw new NotImplementedException($"поведение для роли {_claimPrincipal.Identity.Name} не реализовано");
                Navigator.CurrentBody = Navigator.Bodies[0];
            }
            else throw new UnauthorizedAccessException("Пользователь не распознан");
        }
    }
}
