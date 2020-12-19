using Hospital.ViewModel.Services;
using Hospital.WPF.Controls.Registrator;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital.WPF.Views
{
    public partial class Registrator : UserControl
    {
        private readonly List<UserControl> _bodies = new List<UserControl> { new RegDoctorTable(), new RegEntryTable(), new RegPatientTable() };
        public static string Label { get; } = "Регистратура";

        public Registrator()
        {
            InitializeComponent();
            //DataContext = new RegistratorViewModel();

            foreach (UserControl control in _bodies) RegistratorView.AddLogicalChild(control);

            CurrentControl = _bodies[0];
            SetBody = new RelayCommand(obj => CurrentControl = _bodies[0]);
            SetEntry = new RelayCommand(obj => CurrentControl = _bodies[1]);
            SetPatient = new RelayCommand(obj => CurrentControl = _bodies[2]);
            FocusSearchPatient = new RelayCommand(obj => { SearchBar.TextBoxSearch.Focus(); SearchBar.TabPatient.IsSelected = true; });
        }

        public UserControl CurrentControl
        {
            get { return (UserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }
        public static readonly DependencyProperty CurrentControlProperty = DependencyProperty.Register("CurrentControl", typeof(UserControl), typeof(Registrator));

        public ICommand SetBody
        {
            get { return (ICommand)GetValue(SetBodyProperty); }
            set { SetValue(SetBodyProperty, value); }
        }
        public static readonly DependencyProperty SetBodyProperty = DependencyProperty.Register("SetBody", typeof(ICommand), typeof(Registrator));
        public ICommand SetEntry
        {
            get { return (ICommand)GetValue(SetEntryProperty); }
            set { SetValue(SetEntryProperty, value); }
        }
        public static readonly DependencyProperty SetEntryProperty = DependencyProperty.Register("SetEntry", typeof(ICommand), typeof(Registrator));
        public ICommand SetPatient
        {
            get { return (ICommand)GetValue(SetPatientProperty); }
            set { SetValue(SetPatientProperty, value); }
        }
        public static readonly DependencyProperty SetPatientProperty = DependencyProperty.Register("SetPatient", typeof(ICommand), typeof(Registrator));
        public ICommand FocusSearchPatient
        {
            get { return (ICommand)GetValue(FocusSearchPatientProperty); }
            set { SetValue(FocusSearchPatientProperty, value); }
        }
        public static readonly DependencyProperty FocusSearchPatientProperty = DependencyProperty.Register("FocusSearchPatient", typeof(ICommand), typeof(Registrator));
    }
}
