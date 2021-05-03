using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbEntryInfo : UserControl, INavigatorItem
    {
        public AmbEntryInfo()
        {
            InitializeComponent();
        }

        public string Label => "AmbEntryInfo";
        public Type Type => typeof(AmbEntryInfo);
    }
}
