using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : UserControl
    {
        public static string Label { get; private set; }

        public Schedule()
        {
            Label = "Расписание";
            InitializeComponent();
        }
    }
}
