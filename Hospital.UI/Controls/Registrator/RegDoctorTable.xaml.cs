using System.Windows.Controls;

namespace Hospital.UI.Controls.Registrator
{
    /// <summary>
    /// Логика взаимодействия для RegDoctorTable.xaml
    /// </summary>
    public partial class RegDoctorTable : UserControl
    {
        public string Label { get; private set; }

        public RegDoctorTable()
        {
            Label = "ДОКТОРА";
            InitializeComponent();
        }
    }
}
