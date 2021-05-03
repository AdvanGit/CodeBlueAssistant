using Hospital.Domain.Model; //отступ от mvvm, ссылки на enum TestMethod заменить string, либо переопределить отдельно во viewModel
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
        private readonly AmbulatoryViewModel _vm;
        private readonly Ambulatory _view;

        #region Diagnostic Command
        private readonly Command _addPhysicalTemplate;
        private readonly Command _addPhysicalTestData;
        private readonly Command _addLabTemplate;
        private readonly Command _addLabTestData;
        private readonly Command _addToolTemplate;
        private readonly Command _addToolTestData;
        private readonly Command _removeTestData;
        #endregion

        #region Therapy Command
        private readonly Command _addPharmacoTherapyData;
        private readonly Command _addPhysioData;
        private readonly Command _addSurgeryData;
        private readonly Command _removePharmacoTherapyData;
        private readonly Command _removePhysioData;
        private readonly Command _removeSurgeryData;
        #endregion

        #region Entry Command
        private readonly Command _findEntryPrevious;
        private readonly Command _findEntryNext;
        private readonly Command _findEntry;
        private readonly Command _findBySelect;
        private readonly Command _setSavePanel;
        private readonly Command _toAbsence;
        private readonly Command _saveDatas;
        #endregion

        private readonly Command _closeTab;


        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _vm = ambulatoryViewModel;
            _view = ambulatoryView;

            _addPhysicalTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Физикальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentPhysicalTemplate != null));
            _addPhysicalTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Физикальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedPhysicalTest != null));
            _addLabTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Лабараторная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentLabTemplate != null));
            _addLabTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Лабараторная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedLabTest != null));
            _addToolTemplate = new Command(obj => _vm.DiagnosticViewModel.AddTemplate(TestMethod.Инструментальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.CurrentToolTemplate != null));
            _addToolTestData = new Command(obj => _vm.DiagnosticViewModel.AddData(obj, TestMethod.Инструментальная), obj => (_vm.DiagnosticViewModel != null) && (_vm.DiagnosticViewModel.SelectedToolTest != null));
            _removeTestData = new Command(obj => new ConfirmDialog(_obj => _vm.DiagnosticViewModel.RemoveData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));

            _addPharmacoTherapyData = new Command(obj => _vm.TherapyViewModel.AddPharmacoTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.PharmacoData.Drug != null));
            _addPhysioData = new Command(obj => _vm.TherapyViewModel.AddPhysioTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.PhysioData.PhysioTherapyFactor != null));
            _addSurgeryData = new Command(obj => _vm.TherapyViewModel.AddSurgeryTherapyData(), obj => (_vm.TherapyViewModel != null) && (_vm.TherapyViewModel.SurgeryData.SurgeryOperation != null));
            _removePharmacoTherapyData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemovePharmacoTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
            _removePhysioData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemovePhysioTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
            _removeSurgeryData = new Command(obj => new ConfirmDialog(_obj => _vm.TherapyViewModel.RemoveSurgeryTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));

            _findEntryPrevious = new Command(obj =>
            {
                _vm.EntryViewModel.SelectedEntry.TargetDateTime -= TimeSpan.FromDays(1);
                _vm.EntryViewModel.FindBySelect(_vm.EntryViewModel.SelectedEntry);
            }, obj => _vm.EntryViewModel != null);
            _findEntryNext = new Command(obj =>
            {
                _vm.EntryViewModel.SelectedEntry.TargetDateTime += TimeSpan.FromDays(1);
                _vm.EntryViewModel.FindBySelect(_vm.EntryViewModel.SelectedEntry);
            }, obj => _vm.EntryViewModel != null);
            _findEntry = new Command(obj =>
            {
                _vm.EntryViewModel.FindEntry(obj.ToString());
                _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            }, obj => (_vm.EntryViewModel != null && obj.ToString().Length != 0));
            _findBySelect = new Command(obj =>
            {
                _vm.EntryViewModel.FindBySelect(obj);
                _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySelectPanel));
            }, obj => (_vm.EntryViewModel != null && obj != null));
            _setSavePanel = new Command(obj =>
            {
                _view.EntrySearchNavigator.SetBody(typeof(AmbEntrySavePanel));
                _vm.EntryViewModel.SetEntryOut(obj);
            }, obj => _vm.EntryViewModel != null && obj != null);
            _toAbsence = new Command(obj => new ConfirmDialog(_obj => _vm.EntryViewModel.ToAbsence(), "Вы подтверждаете неявку лица\n" + _vm.CurrentEntry.Patient.FirstName + " " + _vm.CurrentEntry.Patient.MidName + " " + _vm.CurrentEntry.Patient.LastName + "?"));
            _saveDatas = new Command(obj => new ConfirmDialog(async _obj => { await _vm.DiagnosticViewModel.UpdateData(); await _vm.TherapyViewModel.UpdateData(); await _vm.EntryViewModel.UpdateEntry(); }, "Записать и сохранить?"));

            _closeTab = new Command(obj => new ConfirmDialog(_obj =>
            {
                Main.TabNavigator.Bodies.Remove(_view);
                int tabcount = Main.TabNavigator.Bodies.Count;
                if (tabcount != 0) Main.CurrentPage = Main.TabNavigator.Bodies[tabcount - 1];
                else Main.CurrentPage = Main.MenuNavigator.Bodies[0];
            }, "Вы действительно хотите закрыть вкладку?\n\nВнимение: все несохраненные данные будут утеряны!"));
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
        public Command AddSurgeryData => _addSurgeryData;
        public Command RemoveSurgeryData => _removeSurgeryData;

        public Command FindEntry => _findEntry;
        public Command FindBySelect => _findBySelect;
        public Command SetSavePanel => _setSavePanel;
        public Command FindEntryPrevious => _findEntryPrevious;
        public Command FindEntryNext => _findEntryNext;

        public Command ToAbsence => _toAbsence;
        public Command SaveDatas => _saveDatas;
        public Command CloseTab => _closeTab;
    }
}
