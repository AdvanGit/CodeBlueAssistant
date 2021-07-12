using Hospital.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.WPF.Navigators
{
    public class Navigator : INotifyPropertyChanged
    {
        private INavigatorItem _currentBody;
        public Navigator(ObservableCollection<INavigatorItem> controls)
        {
            Bodies = controls;
        }

        public INavigatorItem CurrentBody { get => _currentBody; set { _currentBody = value; OnPropertyChanged(nameof(CurrentBody)); } }

        public ObservableCollection<INavigatorItem> Bodies { get; }

        public void SetBody(string typeName)
        {
            foreach (INavigatorItem control in Bodies)
                if (control.GetType().Name == typeName) CurrentBody = control;
        }
        public void SetBody(Type type)
        {
            foreach (INavigatorItem control in Bodies)
                if (control.GetType() == type) CurrentBody = control;
        }

        public Command SetByIndex => new Command(i => CurrentBody = Bodies[int.Parse(i.ToString())], i => int.Parse(i.ToString()) + 1 >= Bodies.Count && int.Parse(i.ToString()) + 1 <= Bodies.Count);
        public Command SetByTypeName => new Command(i => SetBody(i.ToString()), i => i != null);
        public Command SetByType => new Command(i => SetBody((Type)i), i => i != null);

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
