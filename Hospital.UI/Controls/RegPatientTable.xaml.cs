using System.Windows.Controls;

namespace Hospital.UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для RegPatientTable.xaml
    /// </summary>
    public partial class RegPatientTable : UserControl
    {
        public string Label { get; set; }

        public RegPatientTable()
        {
            Label = "ПАЦИЕНТЫ";
            InitializeComponent();
        }
    }
}
