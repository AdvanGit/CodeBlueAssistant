using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Ambulatory : UserControl
    {
        public static string Label => "Амбулатория";

        public AmbulatoryCommand Command { get; private set; }
        public AmbulatoryNavigator Navigator { get; } = new AmbulatoryNavigator();

        private AmbulatoryViewModel ambulatoryViewModel = new AmbulatoryViewModel();

        public Ambulatory()
        {
            InitializeComponent();
            DataContext = ambulatoryViewModel;
            Command = new AmbulatoryCommand(ambulatoryViewModel, AmbulatoryView);

            foreach (UserControl userControl in Navigator.GetBodies()) { AmbulatoryView.AddLogicalChild(userControl); }
            Navigator.SetBody("Therapy");
        }
    }
}
