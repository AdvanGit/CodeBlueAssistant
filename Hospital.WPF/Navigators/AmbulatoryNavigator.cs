using Hospital.WPF.Controls.Ambulatory;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace Hospital.WPF.Navigators
{
    public class AmbulatoryNavigator : UserControl, INotifyPropertyChanged
    {
        private UserControl _currentBody;
        public UserControl CurrentBody { get => _currentBody; set { _currentBody = value; OnPropertyChanged(nameof(CurrentBody)); } }
        public List<UserControl> GetControls { get => bodies; }

        private UserControl _currentPhysDiagPanel = new AmbDiagPhysAddPanel();
        public UserControl CurrentPhysDiagPanel { get => _currentPhysDiagPanel; set { _currentPhysDiagPanel = value; OnPropertyChanged(nameof(CurrentPhysDiagPanel)); } }


        private readonly List<UserControl> bodies = new List<UserControl>
        {
            new AmbDiagnostic(),
            new AmbMedCard(),
            new AmbReport()
        };

        private readonly List<UserControl> physDiagPanels = new List<UserControl>
        {
            new AmbDiagPhysAddPanel(),
            new AmbDiagPhysDialogPanel()
        };

        public void SetBody(string bodyName)
        {
            switch (bodyName)
            {
                case "MedCard": CurrentBody = bodies[0]; return;
                case "Diagnostic": CurrentBody = bodies[1]; return;
                case "Report": CurrentBody = bodies[2]; return;
                default: break;
            }
        }
        public List<UserControl> GetBodies()
        {
            return bodies;
        }

        public void SetDiagPhysPanel(string panelName)
        {
            switch (panelName)
            {
                case "Add": CurrentPhysDiagPanel = physDiagPanels[0]; return;
                case "Dialog": CurrentPhysDiagPanel = physDiagPanels[1]; return;
                default: break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
