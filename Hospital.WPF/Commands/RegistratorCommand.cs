using Hospital.ViewModel;
using Hospital.WPF.Controls.Registrator;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class RegistratorCommand
    {
        private static RegistratorViewModel _vm;
        private static Registrator _view;

        private static readonly Command _setBody = new Command(param =>
            {
                _view.Navigator.SetBody(param.ToString()); ;
                if (param.ToString() == "RegDoctorTable") _view.SearchBar.TabDoctor.IsSelected = true;
                else if (param.ToString() == "RegPatientTable") _view.SearchBar.TabPatient.IsSelected = true;
            });
        private static readonly Command _getEntries = new Command(async obj =>
        {
            if (obj != null)
            {
                await _vm.GetEntries(obj);
                _view.Navigator.SetBody(typeof(RegEntryTable));
            }
        });
        private static readonly Command _selectPatient = new Command(obj => { if (obj != null) _vm.SelectEntity(obj); });
        private static readonly Command _selectEntry = new Command(obj =>
            {
                if (obj != null)
                {
                    _vm.SelectEntity(obj);
                    _view.SearchBar.TabPatient.IsSelected = true;
                    _view.SearchBar.TextBoxSearch.Focus();
                    _view.Navigator.SetBody(typeof(RegPatientTable));
                }
            });
        private static readonly Command _editPatient = new Command(async obj =>
            {
                if (obj.ToString() == "true") _vm.EditPatient(true);
                else _vm.EditPatient(false);
                _view.Navigator.SetBody(typeof(RegEditPanel));
                await _vm.GetBelays();
            }, obj =>
            {
                if (obj.ToString() == "true") return true;
                else return _vm.SelectedPatient != null;
            });
        private static readonly Command _savePatient = new Command(async obj =>
            {
                await _vm.SavePatient(); _view.Navigator.SetBody(typeof(RegPatientTable));
            }, obj => _vm.EditingPatient!=null &&  _vm.EditingPatient.FirstName!=null && _vm.EditingPatient.MidName != null && _vm.EditingPatient.LastName != null && _vm.EditingPatient.PhoneNumber !=0);
        private static readonly Command _createEntry = new Command(async obj => await _vm.CreateEntry(), obj => _vm.SelectedEntry != null && _vm.SelectedPatient != null );
        private static readonly Command _getSearchFunc = new Command(async obj =>
        {
            if (obj.ToString() == "0") await _vm.SearchDoctor();
            if (obj.ToString() == "1") await _vm.SearchPatient();
        }, obj => _vm.SearchString != "");
        private static readonly Command _cleanEntry = new Command(obj => _vm.SelectedEntry = null);
        private static readonly Command _cleanPatient = new Command(obj => _vm.SelectedPatient = null);
        private static readonly Command _findEntryPrevious = new Command(async obj => { await _vm.GetEntries(true); }, obj => _vm != null);
        private static readonly Command _findEntryNext = new Command(async obj => { await _vm.GetEntries(false); }, obj => _vm != null);

        public RegistratorCommand(RegistratorViewModel viewModel, Registrator view)
        {
            _vm = viewModel;
            _view = view;
        }

        public static Command SetBody => _setBody;
        public static Command GetEntries => _getEntries;
        public static Command SelectPatient => _selectPatient;
        public static Command SelectEntry => _selectEntry;
        public static Command EditPatient => _editPatient;
        public static Command SavePatient => _savePatient;
        public static Command CreateEntry => _createEntry;
        public static Command GetSearchFunc => _getSearchFunc;
        public static Command CleanEntry => _cleanEntry;
        public static Command CleanPatient => _cleanPatient;
        public static Command FindEntryPrevious => _findEntryPrevious;
        public static Command FindEntryNext => _findEntryNext;
    }
}
