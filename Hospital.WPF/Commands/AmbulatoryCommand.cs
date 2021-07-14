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
        #region Command fields
        private readonly Command _addPhysicalData;
        private readonly Command _removePhysicalData;
        private readonly Command _addPhysicalTemplate;
        private readonly Command _addLabData;
        private readonly Command _removeLabData;
        private readonly Command _addLabTemplate;
        private readonly Command _addToolData;
        private readonly Command _removeToolData;
        private readonly Command _addToolTemplate;

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
        #endregion

        private readonly Command _closeTab;

        public AmbulatoryCommand(Ambulatory ambulatoryView)
        {
            AmbulatoryViewModel vm = ambulatoryView.DataContext as AmbulatoryViewModel;
            Ambulatory view = ambulatoryView;
            TestContainer physicalContainer = vm.DiagnosticViewModel.PhysicalContainer;
            TestContainer labContainer = vm.DiagnosticViewModel.LabContainer;
            TestContainer toolContainer = vm.DiagnosticViewModel.ToolContainer;

            #region diagnostic command
            _addPhysicalTemplate = new Command(async obj =>
            {
                await physicalContainer.AddTemplate();
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (vm.DiagnosticViewModel != null) && (physicalContainer.CurrentTemplate != null));
            _addPhysicalData = new Command(obj =>
           {
               physicalContainer.Datas.Add(physicalContainer.GenerateData());
               vm.DiagnosticViewModel.RaiseDataPropetryChange();
           }, obj => (vm.DiagnosticViewModel != null) && (physicalContainer.CurrentTest != null));
            _removePhysicalData = new Command(obj =>
            {
                new ConfirmDialog(_obj => physicalContainer.RemoveRangeData(obj), "вы действительно хотите удалить данные?");
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => ((obj != null) && ((IList)obj).Count != 0));

            _addLabTemplate = new Command(async obj =>
            {
                await labContainer.AddTemplate();
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (vm.DiagnosticViewModel != null) && (labContainer.CurrentTemplate != null));
            _addLabData = new Command(obj =>
            {
                labContainer.Datas.Add(labContainer.GenerateData());
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (vm.DiagnosticViewModel != null) && (labContainer.CurrentTest != null));
            _removeLabData = new Command(obj =>
            {
                new ConfirmDialog(_obj => labContainer.RemoveRangeData(obj), "вы действительно хотите удалить данные?");
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (obj != null) && ((IList)obj).Count != 0);

            _addToolTemplate = new Command(async obj =>
            {
                await toolContainer.AddTemplate();
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (vm.DiagnosticViewModel != null) && (toolContainer.CurrentTemplate != null));
            _addToolData = new Command(obj =>
            {
                toolContainer.Datas.Add(toolContainer.GenerateData());
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => (vm.DiagnosticViewModel != null) && (toolContainer.CurrentTest != null));
            _removeToolData = new Command(obj =>
            {
                new ConfirmDialog(_obj => toolContainer.RemoveRangeData(obj), "вы действительно хотите удалить данные?");
                vm.DiagnosticViewModel.RaiseDataPropetryChange();
            }, obj => ((obj != null) && ((IList)obj).Count != 0));
            #endregion

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

            _toAbsence = new Command(obj => new ConfirmDialog(async _obj => { await vm.EntryViewModel.ToAbsence(); await vm.Refresh(); }, "Вы подтверждаете неявку лица\n" + vm.EntryViewModel.CurrentEntry.Patient.FirstName + " " + vm.EntryViewModel.CurrentEntry.Patient.MidName + " " + vm.EntryViewModel.CurrentEntry.Patient.LastName + "?"));
            _saveDatas = new Command(obj => new ConfirmDialog(async _obj => { await vm.SaveChanges(); }, "Записать и сохранить?"));

            _closeTab = new Command(obj => new ConfirmDialog(_obj =>
            {
                Main.TabNavigator.Bodies.Remove(view);
                int tabcount = Main.TabNavigator.Bodies.Count;
                if (tabcount != 0) Main.CurrentPage = Main.TabNavigator.Bodies[tabcount - 1];
                else Main.CurrentPage = Main.ViewStateManager.Navigator.Bodies[0];
            }, "Вы действительно хотите закрыть вкладку?\n\nВнимение: все несохраненные данные будут утеряны!"));
        }

        #region Diagnostic property
        public Command AddPhysicalData => _addPhysicalData;
        public Command RemovePhysicalData => _removePhysicalData;
        public Command AddPhysicalTemplate => _addPhysicalTemplate;
        public Command AddLabData => _addLabData;
        public Command RemoveLabData => _removeLabData;
        public Command AddLabTemplate => _addLabTemplate;
        public Command AddToolData => _addToolData;
        public Command RemoveToolData => _removeToolData;
        public Command AddToolTemplate => _addToolTemplate;
        #endregion

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
