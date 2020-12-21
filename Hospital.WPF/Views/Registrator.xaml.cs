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
        private readonly List<UserControl> _bodies = new List<UserControl>
        {
            new RegDoctorTable(),
            new RegEntryTable(),
            new RegPatientTable(),
            new RegEditPanel()
        };
        public static string Label { get; } = "Регистратура";

        public Registrator()
        {
            InitializeComponent();
            //DataContext = new RegistratorViewModel();

            foreach (UserControl control in _bodies) RegistratorView.AddLogicalChild(control);

            CurrentControl = _bodies[0];

            SetEntry = new RelayCommand(parameter => CurrentControl = _bodies[1]);
            SetDoctor = new RelayCommand(parameter => { CurrentControl = _bodies[0]; SearchBar.TabDoctor.IsSelected = true; });
            SetEdit = new RelayCommand(parameter => CurrentControl = _bodies[3]);
            SetPatient = new RelayCommand(parameter => { CurrentControl = _bodies[2]; SearchBar.TabPatient.IsSelected = true; });
            SetFocusPatient = new RelayCommand(parameter =>
            {
                if (parameter != null)
                {
                    SearchBar.TextBoxSearch.Focus();
                    SearchBar.TabPatient.IsSelected = true;
                    CurrentControl = _bodies[2];
                }
            });

        }

        public UserControl CurrentControl
        {
            get { return (UserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }
        public static readonly DependencyProperty CurrentControlProperty = DependencyProperty.Register("CurrentControl", typeof(UserControl), typeof(Registrator));

        public ICommand SetFocusPatient
        {
            get { return (ICommand)GetValue(SetFocusPatientProperty); }
            set { SetValue(SetFocusPatientProperty, value); }
        }
        public static readonly DependencyProperty SetFocusPatientProperty = DependencyProperty.Register("SetFocusPatient", typeof(ICommand), typeof(Registrator));
        public ICommand SetEntry
        {
            get { return (ICommand)GetValue(SetEntryProperty); }
            set { SetValue(SetEntryProperty, value); }
        }
        public static readonly DependencyProperty SetEntryProperty = DependencyProperty.Register("SetEntry", typeof(ICommand), typeof(Registrator));
        public ICommand SetDoctor
        {
            get { return (ICommand)GetValue(SetDoctorProperty); }
            set { SetValue(SetDoctorProperty, value); }
        }
        public static readonly DependencyProperty SetDoctorProperty = DependencyProperty.Register("SetDoctor", typeof(ICommand), typeof(Registrator));
        public ICommand SetEdit
        {
            get { return (ICommand)GetValue(SetEditProperty); }
            set { SetValue(SetEditProperty, value); }
        }
        public static readonly DependencyProperty SetEditProperty = DependencyProperty.Register("SetEdit", typeof(ICommand), typeof(Registrator));
        public ICommand SetPatient
        {
            get { return (ICommand)GetValue(SetPatientProperty); }
            set { SetValue(SetPatientProperty, value); }
        }
        public static readonly DependencyProperty SetPatientProperty = DependencyProperty.Register("SetPatient", typeof(ICommand), typeof(Registrator));




    }
}
