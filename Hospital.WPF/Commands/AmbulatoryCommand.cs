using Hospital.Domain.Model; //отступ от mvvm, ссылки на enum TestMethod заменить string, либо переопределить отдельно во viewModel
using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Controls;
using Hospital.WPF.Controls.Ambulatory;
using Hospital.WPF.Views;
using System;
using System.Collections;
using System.Linq;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        #region CommandFields
        private readonly Command _addPhysicalTemplate;
        private readonly Command _addPhysicalData;
        private readonly Command _addLabTemplate;
        private readonly Command _addLabTestData;
        private readonly Command _addToolTemplate;
        private readonly Command _addToolTestData;
        private readonly Command _removeTestData;

        private readonly Command _addPharmacoTherapyData;
        private readonly Command _addPhysioData;
        private readonly Command _addSurgeryData;
        private readonly Command _removePharmacoTherapyData;
        private readonly Command _removePhysioData;
        private readonly Command _removeSurgeryData;

        private readonly Command _findEntryPrevious;
        private readonly Command _findEntryNext;
        private readonly Command _findEntry;
        private readonly Command _findBySelect;
        private readonly Command _setSavePanel;
        private readonly Command _toAbsence;
        private readonly Command _saveDatas;

        private readonly Command _removePhysicalData;
        #endregion

        private readonly Command _closeTab;

        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            AmbulatoryViewModel vm = ambulatoryViewModel;
            Ambulatory view = ambulatoryView;
            TestContainer physicalContainer = vm.DiagnosticViewModel.PhysicalContainer;

            _addPhysicalTemplate = new Command(
                obj => physicalContainer.AddTemplate(),
                obj => (vm.DiagnosticViewModel != null) && (physicalContainer.CurrentTemplate != null));
            _addPhysicalData = new Command( 
                obj => physicalContainer.Datas.Add(physicalContainer.GenerateData()),
                obj => (vm.DiagnosticViewModel != null) && (physicalContainer.CurrentTest != null));
            _removePhysicalData = new Command(obj =>
                new ConfirmDialog(_obj => physicalContainer.RemoveRangeData((obj as IList).Cast<TestData>().ToList()), "вы действительно хотите удалить данные?"),
                obj => ((obj != null) && ((IList)obj).Count != 0));


            _removeTestData = new Command(obj => new ConfirmDialog(_obj => vm.DiagnosticViewModel.RemoveData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));

            _addLabTemplate = new Command(obj => vm.DiagnosticViewModel.AddTemplate(TestMethod.Лабараторная), obj => (vm.DiagnosticViewModel != null) && (vm.DiagnosticViewModel.CurrentLabTemplate != null));
            _addLabTestData = new Command(obj => vm.DiagnosticViewModel.AddData(obj, TestMethod.Лабараторная), obj => (vm.DiagnosticViewModel != null) && (vm.DiagnosticViewModel.SelectedLabTest != null));
            _addToolTemplate = new Command(obj => vm.DiagnosticViewModel.AddTemplate(TestMethod.Инструментальная), obj => (vm.DiagnosticViewModel != null) && (vm.DiagnosticViewModel.CurrentToolTemplate != null));
            _addToolTestData = new Command(obj => vm.DiagnosticViewModel.AddData(obj, TestMethod.Инструментальная), obj => (vm.DiagnosticViewModel != null) && (vm.DiagnosticViewModel.SelectedToolTest != null));
            _removePhysioData = new Command(obj => new ConfirmDialog(_obj => vm.TherapyViewModel.RemovePhysioTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));

            _addPharmacoTherapyData = new Command(obj => vm.TherapyViewModel.AddPharmacoTherapyData(), obj => (vm.TherapyViewModel != null) && (vm.TherapyViewModel.PharmacoData.Drug != null));
            _addPhysioData = new Command(obj => vm.TherapyViewModel.AddPhysioTherapyData(), obj => (vm.TherapyViewModel != null) && (vm.TherapyViewModel.PhysioData.PhysioTherapyFactor != null));
            _addSurgeryData = new Command(obj => vm.TherapyViewModel.AddSurgeryTherapyData(), obj => (vm.TherapyViewModel != null) && (vm.TherapyViewModel.SurgeryData.SurgeryOperation != null));
            _removePharmacoTherapyData = new Command(obj => new ConfirmDialog(_obj => vm.TherapyViewModel.RemovePharmacoTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));
            _removeSurgeryData = new Command(obj => new ConfirmDialog(_obj => vm.TherapyViewModel.RemoveSurgeryTherapyData(obj), "вы действительно хотите удалить данные?"), obj => ((obj != null) && ((IList)obj).Count != 0));

            _findEntryPrevious = new Command(obj =>
            {
                vm.EntryViewModel.SelectedEntry.TargetDateTime -= TimeSpan.FromDays(1);
                vm.EntryViewModel.FindBySelect(vm.EntryViewModel.SelectedEntry);
            }, obj => vm.EntryViewModel != null);
            _findEntryNext = new Command(obj =>
            {
                vm.EntryViewModel.SelectedEntry.TargetDateTime += TimeSpan.FromDays(1);
                vm.EntryViewModel.FindBySelect(vm.EntryViewModel.SelectedEntry);
            }, obj => vm.EntryViewModel != null);
            _findEntry = new Command(obj =>
            {
                vm.EntryViewModel.FindEntry(obj.ToString());
                view.EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            }, obj => (vm.EntryViewModel != null && obj.ToString().Length != 0));
            _findBySelect = new Command(obj =>
            {
                vm.EntryViewModel.FindBySelect(obj);
                view.EntrySearchNavigator.SetBody(typeof(AmbEntrySelectPanel));
            }, obj => (vm.EntryViewModel != null && obj != null));
            _setSavePanel = new Command(obj =>
            {
                view.EntrySearchNavigator.SetBody(typeof(AmbEntrySavePanel));
                vm.EntryViewModel.SetEntryOut(obj);
            }, obj => vm.EntryViewModel != null && obj != null);
            _toAbsence = new Command(obj => new ConfirmDialog(_obj => vm.EntryViewModel.ToAbsence(), "Вы подтверждаете неявку лица\n" + vm.CurrentEntry.Patient.FirstName + " " + vm.CurrentEntry.Patient.MidName + " " + vm.CurrentEntry.Patient.LastName + "?"));
            _saveDatas = new Command(obj => new ConfirmDialog(async _obj => { await vm.DiagnosticViewModel.UpdateData(); await vm.TherapyViewModel.UpdateData(); await vm.EntryViewModel.UpdateEntry(); }, "Записать и сохранить?"));

            _closeTab = new Command(obj => new ConfirmDialog(_obj =>
            {
                Main.TabNavigator.Bodies.Remove(view);
                int tabcount = Main.TabNavigator.Bodies.Count;
                if (tabcount != 0) Main.CurrentPage = Main.TabNavigator.Bodies[tabcount - 1];
                else Main.CurrentPage = Main.MenuNavigator.Bodies[0];
            }, "Вы действительно хотите закрыть вкладку?\n\nВнимение: все несохраненные данные будут утеряны!"));
        }

        public Command AddPhysicalData => _addPhysicalData;
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

        public Command RemovePhysicalData => _removePhysicalData;
    }
}
