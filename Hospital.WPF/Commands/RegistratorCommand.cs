using Hospital.ViewModel;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class RegistratorCommand
    {
        private static RegistratorViewModel _vm;
        private static Registrator _view;

        private static readonly Command _setBody = new Command(param => 
            {
                _view.Navigator.SetBody(param.ToString());
                if (param.ToString() == "Doctors") _view.SearchBar.TabDoctor.IsSelected = true;
                else if (param.ToString() == "Patients") _view.SearchBar.TabPatient.IsSelected = true;
            });
        private static readonly Command _findDoctor = new Command(async param => await _vm.SearchDoctor(_view.SearchBar.TextBoxSearch.Text));
        private static readonly Command _findPatient = new Command(async param => await _vm.SearchPatient(_view.SearchBar.TextBoxSearch.Text));
        private static readonly Command _getEntries = new Command(async obj =>
        {
            if (obj != null)
            {
                await _vm.GetEntries(obj);
                _view.Navigator.SetBody("Entries");
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
                    _view.Navigator.SetBody("Patients");
                }
            });
        private static readonly Command _editPatient = new Command(async obj =>
            {
                if (obj.ToString() == "true") _vm.EditPatient(true);
                else _vm.EditPatient(false);
                _view.Navigator.SetBody("Edit");
                await _vm.GetBelays();
            }, obj =>
            {
                if (obj.ToString() == "true") return true;
                else return _vm.SelectedPatient != null;
            });
        private static readonly Command _savePatient = new Command(async obj =>
            {
                var command = _vm.SavePatient();
                await command;
                if (command.IsCompletedSuccessfully) _view.Navigator.SetBody("Patients");
            });
        private static readonly Command _createEntry = new Command(async obj => await _vm.CreateEntry(), obj => { return (_vm.SelectedEntry != null && _vm.SelectedPatient != null); });

        public RegistratorCommand(RegistratorViewModel viewModel, Registrator view)
        {
            _vm = viewModel;
            _view = view;
        }

        public static Command SetBody => _setBody;
        public static Command FindDoctor => _findDoctor;
        public static Command FindPatient => _findPatient;
        public static Command GetEntries => _getEntries;
        public static Command SelectPatient => _selectPatient;
        public static Command SelectEntry => _selectEntry;
        public static Command EditPatient => _editPatient;
        public static Command SavePatient => _savePatient;
        public static Command CreateEntry => _createEntry;
    }
}
