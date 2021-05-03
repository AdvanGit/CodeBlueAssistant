using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbEntrySearchPanel : UserControl, INavigatorItem
    {
        public AmbEntrySearchPanel()
        {
            InitializeComponent();
        }

        public string Label => "AmbEntrySearchPanel";
        public Type Type => typeof(AmbEntrySearchPanel);
    }
}
