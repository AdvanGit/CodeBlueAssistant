using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbEntrySelectPanel : UserControl, INavigatorItem
    {
        public AmbEntrySelectPanel()
        {
            InitializeComponent();
        }

        public string Label => "AmbEntrySelectPanel";
        public Type Type => GetType();
    }
}
