using Hospital.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace Hospital.WPF.Navigators
{
    public class Navigator : INotifyPropertyChanged
    {
        private UserControl _currentBody;
        private bool isHistory;
        private List<UserControl> controlsQueue = new List<UserControl>();
        private int queueIndex;
        private void AddHistory(UserControl control)
        {
            if (isHistory)
            {
                if (controlsQueue.Count > 10) controlsQueue.RemoveAt(0);
                controlsQueue.Add(control);
                queueIndex = controlsQueue.Count - 1;
            }
        }

        public Navigator(ObservableCollection<UserControl> controls, bool isHistory = false)
        {
            this.isHistory = isHistory;
            Bodies = controls;
        }

        public UserControl CurrentBody { get => _currentBody; set { _currentBody = value; OnPropertyChanged(nameof(CurrentBody)); AddHistory(value); } }

        public ObservableCollection<UserControl> Bodies { get; }

        public void SetBody(string typeName)
        {
            foreach (UserControl control in Bodies)
                if (control.GetType().Name == typeName)
                {
                    CurrentBody = control;
                    AddHistory(control);
                }
        }
        public void SetBody(Type type)
        {
            foreach (UserControl control in Bodies)
                if (control.GetType() == type)
                {
                    CurrentBody = control;
                    AddHistory(control);
                }
        }

        public void Previous()
        {
            if (isHistory && queueIndex > 0)
            {
                queueIndex--;
                CurrentBody = controlsQueue[queueIndex];
            }
        }
        public void Next()
        {
            if (isHistory && queueIndex < controlsQueue.Count - 1)
            {
                queueIndex++;
                CurrentBody = controlsQueue[queueIndex];
            }
        }

        public Command SetByIndex => new Command(i => CurrentBody = Bodies[int.Parse(i.ToString())], i => int.Parse(i.ToString())+1 >= Bodies.Count && int.Parse(i.ToString())+1 <= Bodies.Count );
        public Command SetByTypeName => new Command(i => SetBody(i.ToString()), i=> i!=null);
        public Command SetByType => new Command(i => SetBody((Type)i), i => i != null);
        public Command PreviousHistory => new Command(i => Previous());
        public Command NextHistory => new Command(i => Next());

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
