using Hospital.Domain.Model;
using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _addPhysicalTestData = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Физикальная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.SelectedPhysicalTest != null));
            _addLabTestData = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Лабараторная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.SelectedLabTest != null));
            _addToolTestData = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Инструментальная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.SelectedToolTest != null));
            _removeTestData = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.RemoveData(obj), obj => ((obj != null) && ((System.Collections.IList)obj).Count != 0));
        }

        private static Command _addPhysicalTestData, _addLabTestData, _addToolTestData, _removeTestData;
        public static Command AddPhysicalTestData => _addPhysicalTestData;
        public static Command AddLabTestData => _addLabTestData;
        public static Command AddToolTestData => _addToolTestData;
        public static Command RemoveTestData => _removeTestData;
    }
}
