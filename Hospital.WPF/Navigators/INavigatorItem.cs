using System;

namespace Hospital.WPF.Navigators
{
    public interface INavigatorItem
    {
        public string Label { get; }
        public Type Type { get; }
    }
}
