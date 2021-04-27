using Hospital.Domain.Model; //ссылки на enum TestMethod, при желании можно заменить string, либо переопределить отдельно во viewModel
using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Controls;
using Hospital.WPF.Controls.Ambulatory;
using Hospital.WPF.Views;
using System;
using System.Collections;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        private static AmbulatoryViewModel _vm;
        private static Ambulatory _view;

        #region Diagnostic Command
        private static readonly Command _addPhysicalTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Физикальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentPhysicalTemplate != null));
        private static readonly Command _addPhysicalTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Физикальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedPhysicalTest != null));
        private static readonly Command _addLabTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Лабараторная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentLabTemplate != null));
        private static readonly Command _addLabTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Лабараторная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedLabTest != null));
        private static readonly Command _addToolTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Инструментальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentToolTemplate != null));
        private static readonly Command _addToolTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Инструментальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedToolTest != null));
        private static readonly Command _removeTestData = new Command(obj => new ConfirmDialog(_obj => _vm.DiagnosticViewModel.RemoveData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
        #endregion

        #region Therapy Command
        private static readonly Command _addPharmacoTherapyData = new Command(obj => _vm.TherapyViewModel.AddPharmacoTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.PharmacoData.Drug != null));
        private static readonly Command _addPhysioData = new Command(obj => _vm.TherapyViewModel.AddPhysioTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.PhysioData.PhysioTherapyFactor != null));
        private static readonly Command _addSurgencyData = new Command(obj => _vm.TherapyViewModel.AddSurgencyTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.SurgencyData.SurgencyOperation != null));
        private static readonly Command _removePharmacoTherapyData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemovePharmacoTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
        private static readonly Command _removePhysioData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemovePhysioTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
        private static readonly Command _removeSurgencyData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemoveSurgencyTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
        #endregion

        #region Entry Command
        private static readonly Command _findEntryPrevious = new Command(obj =>
        {
            _vm.EntryViewModel.SelectedEntry.TargetDateTime -= TimeSpan.FromDays(1);
            _vm.EntryViewModel.FindBySelect(_vm.EntryViewModel.SelectedEntry);
        }, obj => _vm.EntryViewModel != null);
        private static readonly Command _findEntryNext = new Command(obj =>
        {
            _vm.EntryViewModel.SelectedEntry.TargetDateTime += TimeSpan.FromDays(1);
            _vm.EntryViewModel.FindBySelect(_vm.EntryViewModel.SelectedEntry);
        }, obj => _vm.EntryViewModel != null);
        private static readonly Command _findEntry = new Command(obj =>
            {
                _vm.EntryViewModel.FindEntry(obj.ToString());
                _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            }, obj => (_vm.EntryViewModel != null && obj.ToString().Length != 0));
        private static readonly Command _findBySelect = new Command(obj =>
            {
                _vm.EntryViewModel.FindBySelect(obj);
                _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySelectPanel));
            }, obj => (_vm.EntryViewModel != null && obj != null));
        private static readonly Command _setSavePanel = new Command(obj =>
        {
            _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySavePanel));
            _vm.EntryViewModel.SetEntryOut(obj);
        }, obj => _vm.EntryViewModel != null && obj != null);
        private static readonly Command _toAbsence = new Command(obj => new ConfirmDialog(_obj => _vm.EntryViewModel.ToAbsence(), "Вы подтверждаете неявку лица\n" + _vm.CurrentEntry.Patient.FirstName + " " + _vm.CurrentEntry.Patient.MidName + " " + _vm.CurrentEntry.Patient.LastName + "?"));
        private static readonly Command _saveDatas = new Command(obj => new ConfirmDialog(async _obj => { await _vm.DiagnosticViewModel.UpdateData();await _vm.TherapyViewModel.UpdateData(); await _vm.EntryViewModel.UpdateEntry(); }, "Записать и сохранить?"));
        #endregion



        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _vm = ambulatoryViewModel;
            _view = ambulatoryView;
        }

        public Command AddPhysicalTestData => _addPhysicalTestData;
        public Command AddLabTestData => _addLabTestData;
        public Command AddToolTestData => _addToolTestData;
        public Command RemoveTestData => _removeTestData;
        public Command AddLabTemplate => _addLabTemplate;
        public Command AddToolTemplate => _addToolTemplate;
        public Command AddPhysicalTemplate => _addPhysicalTemplate;

        public Command AddPharmacoTherapyData => _addPharmacoTherapyData;
        public Command RemovePharmacoTherapyData => _removePharmacoTherapyData;
        public Command AddPhysioData => _addPhysioData;
        public Command RemovePhysioData => _removePhysioData;
        public Command AddSurgencyData => _addSurgencyData;
        public Command RemoveSurgencyData => _removeSurgencyData;

        public Command FindEntry => _findEntry;
        public Command FindBySelect => _findBySelect;
        public Command SetSavePanel => _setSavePanel;
        public Command FindEntryPrevious => _findEntryPrevious;
        public Command FindEntryNext => _findEntryNext;

        public Command ToAbsence => _toAbsence;
        public Command SaveDatas => _saveDatas;
    }
}
