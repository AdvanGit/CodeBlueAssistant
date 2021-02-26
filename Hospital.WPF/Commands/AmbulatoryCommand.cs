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
            _createTemplate = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.CreateDbDataTemplate(obj, "test1"), obj => ((obj != null) && ((System.Collections.IList)obj).Count != 0));
            _addLabTemplate = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Лабараторная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.CurrentLabTemplate != null));
            _addPhysicalTemplate = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Физикальная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.CurrentPhysicalTemplate != null));
            _addToolTemplate = new Command(obj => ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Инструментальная), obj => (ambulatoryViewModel.DiagnosticViewModel != null) && (ambulatoryViewModel.DiagnosticViewModel.CurrentToolTemplate != null));
        }

        private Command _addPhysicalTestData,
            _addLabTestData,
            _addToolTestData,
            _removeTestData,
            _createTemplate,
            _addLabTemplate,
            _addToolTemplate,
            _addPhysicalTemplate;
        public Command AddPhysicalTestData => _addPhysicalTestData;
        public Command AddLabTestData => _addLabTestData;
        public Command AddToolTestData => _addToolTestData;
        public Command RemoveTestData => _removeTestData;
        public Command CreateTemplate => _createTemplate;
        public Command AddLabTemplate => _addLabTemplate;
        public Command AddToolTemplate => _addToolTemplate;
        public Command AddPhysicalTemplate => _addPhysicalTemplate;
    }
}
