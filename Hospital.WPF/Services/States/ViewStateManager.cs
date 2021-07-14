using Hospital.Domain.Model;
using Hospital.ViewModel.Factories;
using Hospital.ViewModel.Services;
using Hospital.WPF.Navigators;
using Hospital.WPF.Views;
using System;
using System.Collections.ObjectModel;

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

        public void Setup(IAuthenticator authenticator)
        {
            if (authenticator.IsLoggedIn)
            {
                switch (authenticator.CurrentUser.Role)
                {
                    case Role.Administrator:
                        Navigator.Bodies.Clear();
                        Navigator.Bodies.Add(new Registrator(_viewModelFactory.CreateRegistratorViewModel()));
                        Navigator.Bodies.Add(new Schedule(_viewModelFactory.CreateScheduleViewModel()));
                        break;
                    case Role.Registrator:
                        Navigator.Bodies.Clear();
                        Navigator.Bodies.Add(new Registrator(_viewModelFactory.CreateRegistratorViewModel()));
                        break;
                    default:
                        Navigator.Bodies.Clear();
                        Navigator.Bodies.Add(new Login(_viewModelFactory.CreateLoginViewModel()));
                        Navigator.CurrentBody = Navigator.Bodies[0];
                        throw new NotImplementedException($"для роли {authenticator.CurrentUser.Role} не задан параметр инициализации");
                }
                Navigator.CurrentBody = Navigator.Bodies[0];
            }
            else
            {
                Navigator.Bodies.Clear();
                Navigator.Bodies.Add(new Login(_viewModelFactory.CreateLoginViewModel()));
                Navigator.CurrentBody = Navigator.Bodies[0];
            }
        }
    }
}
