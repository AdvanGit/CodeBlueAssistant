using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Registrator
{
    public partial class RegEditPanel : UserControl, INavigatorItem
    {
        public RegEditPanel()
        {
            InitializeComponent();
        }

        public string Label => "Редактировать";
        public Type Type => typeof(RegEditPanel);
    }
}
