using Hospital.ViewModel;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Registrator : UserControl
    {
        public static string Label { get; } = "Регистратура";

        private static readonly RegistratorViewModel registratorViewModel = new RegistratorViewModel();
        public RegistratorNavigator Navigator { get; } = new RegistratorNavigator();
        public RegistratorCommand Command { get; private set; }

        public Registrator()
        {
            InitializeComponent();
            DataContext = registratorViewModel;
            Command = new RegistratorCommand(registratorViewModel, this);

            foreach (UserControl userControl in Navigator.GetBodies()) AddLogicalChild(userControl);
            Navigator.SetBody("Doctors");
        }
    }
}
