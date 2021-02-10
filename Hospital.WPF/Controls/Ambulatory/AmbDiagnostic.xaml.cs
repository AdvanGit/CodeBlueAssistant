using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital.WPF.Controls.Ambulatory
{

    public partial class AmbDiagnostic : UserControl
    {
        public string Title { get; } = "Диагностика";

        public AmbDiagnostic()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var sortPath = e.Column.SortMemberPath.ToString();
            var colectionView = CollectionViewSource.GetDefaultView(((DataGrid)sender).ItemsSource);
            
            colectionView.SortDescriptions.Clear();
            
            if (e.Column.SortDirection == null || e.Column.SortDirection.Value == ListSortDirection.Ascending)
            {
                e.Column.SortDirection = ListSortDirection.Descending;
                colectionView.SortDescriptions.Add(new SortDescription(sortPath, ListSortDirection.Descending));
            }  
            else
            {
                e.Column.SortDirection = ListSortDirection.Ascending;
                colectionView.SortDescriptions.Add(new SortDescription(sortPath, ListSortDirection.Ascending));
            }
            e.Handled = true;
        }
    }
}
