using Hospital.WPF.Controls.Registrator;
using Hospital.WPF.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace Hospital.WPF.Navigators
{
    public class RegistratorNavigator : UserControl, INotifyPropertyChanged
    {
        private UserControl _currentBody;
        public UserControl CurrentBody { get => _currentBody; set { _currentBody = value; OnPropertyChanged(nameof(CurrentBody)); }}

        private readonly List<UserControl> bodies = new List<UserControl>
        {
            new RegDoctorTable(),
            new RegEntryTable(),
            new RegPatientTable(),
            new RegEditPanel()
        };

        public void SetBody(string bodyName)
        {
            switch (bodyName)
            {
                case "Doctors": CurrentBody = bodies[0]; return;
                case "Entries": CurrentBody = bodies[1]; return;
                case "Patients": CurrentBody = bodies[2]; return;
                case "Edit": CurrentBody = bodies[3]; return;
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
