using Hospital.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для Registrator.xaml
    /// </summary>
    public partial class Registrator : UserControl
    {
        public Registrator()
        {
            InitializeComponent();
            DataContext = new RegistratorViewModel();
            CurrentControl = new Controls.Registrator.RegDoctorTable();
         
        }

        public static string Label { get; } = "Регистратура";

        public UserControl CurrentControl
        {
            get { return (UserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }

        public static readonly DependencyProperty CurrentControlProperty =
            DependencyProperty.Register("CurrentControl", typeof(UserControl), typeof(Registrator));


    }
}
