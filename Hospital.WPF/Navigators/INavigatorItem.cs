using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace Hospital.WPF.Navigators
{
    public interface INavigatorItem
    {
        public string Label { get; }
        public Type Type { get; }
    }
}
