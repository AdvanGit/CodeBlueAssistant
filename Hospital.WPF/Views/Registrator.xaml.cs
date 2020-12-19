using Hospital.ViewModel.Services;
using Hospital.WPF.Controls.Registrator;
using System.Collections;
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

            SetBody = new RelayCommand(parameter =>
            { 
                if (parameter != null)
                {
                    switch (parameter.ToString())
                    {
                        case "Doctor": CurrentControl = _bodies[0]; break;
                        case "Entry": CurrentControl = _bodies[1]; break;
                        case "Patient": CurrentControl = _bodies[2]; break;
                        case "Edit": CurrentControl = _bodies[3]; break;
                        default: break;
                    }
                }
            });

            SetFocusPatient = new RelayCommand(obj =>
            {
                if ( obj != null )
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

        public ICommand SetBody
        {
            get { return (ICommand)GetValue(SetBodyProperty); }
            set { SetValue(SetBodyProperty, value); }
        }
        public static readonly DependencyProperty SetBodyProperty = DependencyProperty.Register("SetBody", typeof(ICommand), typeof(Registrator));
        public ICommand SetFocusPatient
        {
            get { return (ICommand)GetValue(SetFocusPatientProperty); }
            set { SetValue(SetFocusPatientProperty, value); }
        }
        public static readonly DependencyProperty SetFocusPatientProperty = DependencyProperty.Register("SetFocusPatient", typeof(ICommand), typeof(Registrator));


    }
}
