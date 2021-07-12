using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbEntrySavePanel : UserControl, INavigatorItem
    {
        public AmbEntrySavePanel()
        {
            InitializeComponent();
        }

        public string Label => "AmbEntrySavePanel";
        public Type Type => GetType();
    }
}
