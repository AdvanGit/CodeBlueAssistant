using Hospital.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.WPF.Navigators
{
    public class Navigator : INotifyPropertyChanged
    {
        private INavigatorItem _currentBody;
        private bool isHistory;
        private List<INavigatorItem> controlsQueue = new List<INavigatorItem>();
        private int queueIndex;
        private void AddHistory(INavigatorItem control)
        {
            if (isHistory)
            {
                if (controlsQueue.Count > 10) controlsQueue.RemoveAt(0);
                controlsQueue.Add(control);
                queueIndex = controlsQueue.Count - 1;
            }
        }

        public Navigator(ObservableCollection<INavigatorItem> controls, bool isHistory = false)
        {
            this.isHistory = isHistory;
            Bodies = controls;
        }

        public INavigatorItem CurrentBody { get => _currentBody; set { _currentBody = value; OnPropertyChanged(nameof(CurrentBody)); AddHistory(value); } }

        public ObservableCollection<INavigatorItem> Bodies { get; }

        public void SetBody(string typeName)
        {
            foreach (INavigatorItem control in Bodies)
                if (control.GetType().Name == typeName)
                {
                    CurrentBody = control;
                    AddHistory(control);
                }
        }
        public void SetBody(Type type)
        {
            foreach (INavigatorItem control in Bodies)
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

        public Command SetByIndex => new Command(i => CurrentBody = Bodies[int.Parse(i.ToString())], i => int.Parse(i.ToString()) + 1 >= Bodies.Count && int.Parse(i.ToString()) + 1 <= Bodies.Count);
        public Command SetByTypeName => new Command(i => SetBody(i.ToString()), i => i != null);
        public Command SetByType => new Command(i => SetBody((Type)i), i => i != null);
        public Command PreviousHistory => new Command(i => Previous());
        public Command NextHistory => new Command(i => Next());

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
