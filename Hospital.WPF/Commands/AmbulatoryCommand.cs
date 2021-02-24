using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _addRow = new Command(obj =>
            {
                if (ambulatoryViewModel.DiagnosticViewModel.SelectedTest != null)
                    ambulatoryViewModel.DiagnosticViewModel.PhysicalDiagData.Add(ambulatoryViewModel.DiagnosticViewModel.CreatePhysDiag(ambulatoryViewModel.DiagnosticViewModel.SelectedTest, obj.ToString(), ambulatoryViewModel.DiagnosticViewModel.TestOption));
            });

            //_addTemplate = new Command(obj =>
            // {
            //     if (ambulatoryViewModel.Diagnostic.SelectedTemplate != null)
            //         ambulatoryViewModel.Diagnostic.AddTemplate();
            // });

            _deleteRows = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.DeleteRows(obj),
                obj => ((obj != null) && ((System.Collections.IList)obj).Count != 0));

            //_setPhysPanel = new Command(obj =>
            //{
            //    ambulatoryView.Navigator.SetDiagPhysPanel(obj.ToString());
            //});
        }

        private Command _addRow;
        public Command AddRow { get => _addRow; }
        private Command _addTemplate;
        public Command AddTemplate => _addTemplate;
        private Command _deleteRows;
        public Command DeleteRows => _deleteRows;
        //private Command _setPhysPanel;
        //public Command SetPhysPanel { get => _setPhysPanel; }

    }
}
