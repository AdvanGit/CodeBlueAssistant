using Hospital.Domain.Model; //ссылки на enum TestMethod, при желании можно заменить string, либо переопределить отдельно во viewModel
using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Controls;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        private static AmbulatoryViewModel _ambulatoryViewModel;
        private static Ambulatory _ambulatoryView;

        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _ambulatoryViewModel = ambulatoryViewModel;
            _ambulatoryView = ambulatoryView;
        }

        private static readonly Command _addPhysicalTemplate = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Физикальная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.CurrentPhysicalTemplate != null));
        private static readonly Command _addPhysicalTestData = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Физикальная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.SelectedPhysicalTest != null));
        private static readonly Command _addLabTemplate = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Лабараторная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.CurrentLabTemplate != null));
        private static readonly Command _addLabTestData = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Лабараторная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.SelectedLabTest != null));
        private static readonly Command _addToolTemplate = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddTemplate(TestMethod.Инструментальная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.CurrentToolTemplate != null));
        private static readonly Command _addToolTestData = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.AddData(obj, TestMethod.Инструментальная), obj => (_ambulatoryViewModel.DiagnosticViewModel != null) && (_ambulatoryViewModel.DiagnosticViewModel.SelectedToolTest != null));
        private static readonly Command _removeTestData = new Command(obj => new ConfirmDialog(_obj => _ambulatoryViewModel.DiagnosticViewModel.RemoveData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((System.Collections.IList)obj).Count != 0));
        private static readonly Command _updateTestData = new Command(obj => _ambulatoryViewModel.DiagnosticViewModel.SaveChanges(), obj => (_ambulatoryViewModel.DiagnosticViewModel != null));

        private static readonly Command _addPharmacoTherapyData = new Command(obj => _ambulatoryViewModel.TherapyViewModel.AddPharmacoTherapyData(), obj => (_ambulatoryViewModel.TherapyViewModel != null) && ( _ambulatoryViewModel.TherapyViewModel.PharmacoData.Drug != null));
        private static readonly Command _removePharmacoTherapyData = new Command(obj => new ConfirmDialog(_obj => _ambulatoryViewModel.TherapyViewModel.RemovePharmacoTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((System.Collections.IList)obj).Count != 0));

        public Command AddPhysicalTestData => _addPhysicalTestData;
        public Command AddLabTestData => _addLabTestData;
        public Command AddToolTestData => _addToolTestData;
        public Command RemoveTestData => _removeTestData;
        public Command AddLabTemplate => _addLabTemplate;
        public Command AddToolTemplate => _addToolTemplate;
        public Command AddPhysicalTemplate => _addPhysicalTemplate;
        public Command UpdateTestData => _updateTestData;
        public Command AddPharmacoTherapyData => _addPharmacoTherapyData;
        public Command RemovePharmacoTherapyData => _removePharmacoTherapyData;
    }
}
