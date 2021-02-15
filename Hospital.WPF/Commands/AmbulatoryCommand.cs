using Hospital.ViewModel;
using Hospital.WPF.Views;

namespace Hospital.WPF.Commands
{
    public class AmbulatoryCommand
    {
        public AmbulatoryCommand(AmbulatoryViewModel ambulatoryViewModel, Ambulatory ambulatoryView)
        {
            _addRow = new Command(obj =>
            {
                if (ambulatoryViewModel.SelectedTest != null)
                ambulatoryViewModel.PhysicalDiagData.Add(ambulatoryViewModel.CreatePhysDiag(ambulatoryViewModel.SelectedTest));
            });

        }

        private Command _addRow;

        public Command AddRow { get => _addRow; }
    }
}
