using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbEntrySearchBar : UserControl, INavigatorItem
    {
        public AmbEntrySearchBar()
        {
            InitializeComponent();
        }

        public string Label => "AmbEntrySearchBar";
        public Type Type => typeof(AmbEntrySearchBar);
    }
}
