using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для Ambulatory.xaml
    /// </summary>
    public partial class Ambulatory : UserControl
    {
        public static string Label { get; private set; }

        public Ambulatory()
        {
            Label = "Амбулатория";
            InitializeComponent();
        }
    }
}
