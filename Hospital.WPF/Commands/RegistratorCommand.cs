using Hospital.ViewModel;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class RegistratorCommand
    {
        private Command _setBody;
        private Command _findDoctor;
        private Command _findPatient;
        private Command _setSearchTab;
        private Command _getEntries;
        private Command _selectPatient;
        private Command _selectEntry;

        public Command SetBody { get => _setBody; }
        public Command FindDoctor { get => _findDoctor; }
        public Command FindPatient { get => _findPatient; }
        public Command SetSearchTab { get => _setSearchTab; }
        public Command GetEntries { get => _getEntries; }
        public Command SelectPatient { get => _selectPatient; }
        public Command SelectEntry { get => _selectEntry; }

        public RegistratorCommand(RegistratorViewModel viewModel, Registrator view)
        {
            _setBody = new Command(param => view.Navigator.SetBody(param.ToString()));
            _findPatient = new Command(async param => await viewModel.SearchPatient(view.SearchBar.TextBoxSearch.Text));
            _findDoctor = new Command(async param => await viewModel.SearchDoctor(view.SearchBar.TextBoxSearch.Text));
            _setSearchTab = new Command(param =>
            {
                switch (param.ToString())
                {
                    case "Doctors":
                        view.SearchBar.TabDoctor.IsSelected = true;
                        view.Navigator.SetBody(param.ToString());
                        break;
                    case "Patients":
                        view.SearchBar.TabPatient.IsSelected = true;
                        view.Navigator.SetBody(param.ToString());
                        break;
                    default: break;
                }
            });
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
        }
    }
}
