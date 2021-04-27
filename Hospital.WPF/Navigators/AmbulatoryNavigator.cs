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

        private readonly List<UserControl> bodies = new List<UserControl>
        {
            new AmbDiagnostic(),
            new AmbTherapy(),
            new AmbEntry(),
            new AmbReport()
        };


        public void SetBody(string bodyName)
        {
            switch (bodyName)
            {
                case "Diagnostic": CurrentBody = bodies[0]; return;
                case "Therapy": CurrentBody = bodies[1]; return;
                case "Entry": CurrentBody = bodies[2]; return;
                case "Report": CurrentBody = bodies[3]; return;
                default: break;
            }
        }
        public List<UserControl> GetBodies()
        {
            return bodies;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
