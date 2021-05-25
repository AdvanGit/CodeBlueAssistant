using Hospital.WPF.Navigators;
using System;
using System.Windows.Controls;

namespace Hospital.WPF.Controls.Registrator
{
    /// <summary>
    /// Логика взаимодействия для RegDoctorTable.xaml
    /// </summary>
    public partial class RegDoctorTable : UserControl, INavigatorItem
    {
        public string Label => "ДОКТОРА";
        public Type Type => typeof(RegDoctorTable);

        public RegDoctorTable()
        {
            InitializeComponent();
        }
    }
}
