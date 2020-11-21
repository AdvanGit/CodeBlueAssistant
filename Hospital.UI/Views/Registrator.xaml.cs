using System.Windows.Controls;

namespace Hospital.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для Registrator.xaml
    /// </summary>
    public partial class Registrator : UserControl
    {
        public static string Label { get; private set; }
        
        public Registrator()
        {
            Label = "Регистратура";
            InitializeComponent();
        }
    }
}
