using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Registrator
{
    public partial class RegEntryTable : UserControl, INavigatorItem
    {
        public RegEntryTable()
        {
            InitializeComponent();
        }

        public string Label => "Записи";
        public Type Type => typeof(RegEntryTable);
    }
}
