using System.Windows.Controls;

namespace Hospital.WPF.Controls.Ambulatory
{
    public partial class AmbMedCard : UserControl
    {
        public string Title { get; } = "Карта";

        public AmbMedCard()
        {
            InitializeComponent();
        }
    }
}
