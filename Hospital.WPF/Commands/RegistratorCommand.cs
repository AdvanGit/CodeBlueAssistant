using Hospital.ViewModel;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class RegistratorCommand
    {
        private Command _setBody;
        private Command _findDoctor;
        private Command _findPatient;
        private Command _getEntries;
        private Command _selectPatient;
        private Command _selectEntry;
        private Command _editPatient;
        private Command _savePatient;
        private Command _createEntry;

        public Command SetBody { get => _setBody; }
        public Command FindDoctor { get => _findDoctor; }
        public Command FindPatient { get => _findPatient; }
        public Command GetEntries { get => _getEntries; }
        public Command SelectPatient { get => _selectPatient; }
        public Command SelectEntry { get => _selectEntry; }
        public Command EditPatient { get => _editPatient; }
        public Command SavePatient { get => _savePatient; }
        public Command CreateEntry { get => _createEntry; }

        public RegistratorCommand(RegistratorViewModel viewModel, Registrator view)
        {
            _setBody = new Command(param =>
            {
                view.Navigator.SetBody(param.ToString());
                if (param.ToString() == "Doctors") view.SearchBar.TabDoctor.IsSelected = true;
                else if (param.ToString() == "Patients") view.SearchBar.TabPatient.IsSelected = true;
            });
            _findPatient = new Command(async param => await viewModel.SearchPatient(view.SearchBar.TextBoxSearch.Text));
            _findDoctor = new Command(async param => await viewModel.SearchDoctor(view.SearchBar.TextBoxSearch.Text));
            _getEntries = new Command(async obj =>
            {
                if (obj != null)
                {
                    await viewModel.GetEntries(obj);
                    view.Navigator.SetBody("Entries");
                }
            });
            _selectPatient = new Command(obj => { if (obj != null) viewModel.SelectEntity(obj); });
            _selectEntry = new Command(obj =>
            {
                if (obj != null)
                {
                    viewModel.SelectEntity(obj);
                    view.SearchBar.TabPatient.IsSelected = true;
                    view.SearchBar.TextBoxSearch.Focus();
                    view.Navigator.SetBody("Patients");
                }
            });
            _editPatient = new Command(async obj =>
            {
                if (obj.ToString() == "true") viewModel.EditPatient(true);
                else viewModel.EditPatient(false);
                view.Navigator.SetBody("Edit");
                await viewModel.GetBelays();
            }, obj =>
            {
                if (obj.ToString() == "true") return true;
                else return viewModel.SelectedPatient != null;
            });
            _savePatient = new Command(async obj =>
            {
                var command = viewModel.SavePatient();
                await command;
                if (command.IsCompletedSuccessfully) view.Navigator.SetBody("Patients");
            });
            _createEntry = new Command(async obj => await viewModel.CreateEntry(), obj => { return (viewModel.SelectedEntry != null && viewModel.SelectedPatient != null); });
        }
    }
}
