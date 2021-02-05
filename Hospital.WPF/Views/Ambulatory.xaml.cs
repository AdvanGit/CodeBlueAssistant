using Hospital.ViewModel;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Ambulatory : UserControl
    {
        public static string Label { get; } = "Амбулатория";

        public AmbulatoryCommand Command { get; private set; }
        public AmbulatoryNavigator Navigator { get; } = new AmbulatoryNavigator();

        public Ambulatory()
        {
            InitializeComponent();
            DataContext = new AmbulatoryViewModel();
            Command = new AmbulatoryCommand();

            //foreach (UserControl userControl in Navigator.GetBodies()) { AmbulatoryView.AddLogicalChild(userControl); }
            Navigator.SetBody("MedCard");
        }
    }
}
