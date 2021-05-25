using Hospital.ViewModel;
using Hospital.WPF.Commands;
using Hospital.WPF.Controls.Registrator;
using Hospital.WPF.Navigators;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Registrator : UserControl, INavigatorItem
    {
        public string Label => "Регистратура";
        public Type Type => typeof(Registrator);

        private static readonly RegistratorViewModel registratorViewModel = new RegistratorViewModel();
        public Navigator Navigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>()
        {
            new RegDoctorTable(),
            new RegEntryTable(),
            new RegPatientTable(),
            new RegEditPanel()
        });

        public RegistratorCommand Command { get; }

        public Registrator()
        {
            InitializeComponent();
            DataContext = registratorViewModel;
            Command = new RegistratorCommand(registratorViewModel, this);
            foreach (INavigatorItem item in Navigator.Bodies) AddLogicalChild(item);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Navigator.SetBody(typeof(RegDoctorTable));
        }
    }
}
