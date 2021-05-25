using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Registrator
{
    public partial class RegPatientTable : UserControl, INavigatorItem
    {
        public RegPatientTable()
        {
            InitializeComponent();
        }

        public string Label => "Пациенты";
        public Type Type => typeof(RegPatientTable);
    }
}
