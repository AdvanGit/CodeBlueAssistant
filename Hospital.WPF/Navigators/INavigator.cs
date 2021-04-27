using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace Hospital.WPF.Navigators
{
    interface INavigator : INotifyPropertyChanged
    {
        public UserControl CurrentBody { get; set; }
        public ICollection<UserControl> Bodies { get; set; }
        public void SetBody(string bodyName);
    }
}
