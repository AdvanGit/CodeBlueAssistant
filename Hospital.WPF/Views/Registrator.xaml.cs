using Hospital.ViewModel;
using Hospital.ViewModel.Services;
using Hospital.WPF.Controls.Registrator;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital.WPF.Views
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
            Bodies = new List<UserControl> { new RegDoctorTable(), new RegEntryTable() };
            CurrentControl = Bodies[0];
            SetBody = new RelayCommand(obj => CurrentControl = Bodies[1]);
        }

        public static string Label { get; } = "Регистратура";

        public UserControl CurrentControl
        {
            get { return (UserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }
        public static readonly DependencyProperty CurrentControlProperty =
            DependencyProperty.Register("CurrentControl", typeof(UserControl), typeof(Registrator));

        public List<UserControl> Bodies
        {
            get { return (List<UserControl>)GetValue(BodiesProperty); }
            set { SetValue(BodiesProperty, value); }
        }
        public static readonly DependencyProperty BodiesProperty =
            DependencyProperty.Register("Bodies", typeof(List<UserControl>), typeof(UserControl));

        public ICommand SetBody
        {
            get { return (ICommand)GetValue(SetBodyProperty); }
            set { SetValue(SetBodyProperty, value); }
        }
        public static readonly DependencyProperty SetBodyProperty =
            DependencyProperty.Register("SetBody", typeof(ICommand), typeof(UserControl));



    }
}
