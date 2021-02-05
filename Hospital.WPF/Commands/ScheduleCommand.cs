using Hospital.ViewModel;
using Hospital.WPF.Views;
using System;

namespace Hospital.WPF.Commands
{
    public class ScheduleCommand
    {

        private Command _nextDate;
        private Command _previousDate;
        private Command _addColumn;
        private Command _removeColumn;

        public Command NextDate { get => _nextDate; }
        public Command PreviousDate { get => _previousDate; }
        public Command AddColumn { get => _addColumn; }
        public Command RemoveColumn { get => _removeColumn; }

        public ScheduleCommand(ScheduleViewModel scheduleViewModel, Schedule scheduleView)
        {
            _nextDate = new Command(obj => scheduleViewModel.SelectedDate += TimeSpan.FromDays(1));
            _previousDate = new Command(obj => scheduleViewModel.SelectedDate -= TimeSpan.FromDays(1));
            _addColumn = new Command(obj => { if (scheduleView.TileColumnCount < 5) scheduleView.TileColumnCount += 1; });
            _removeColumn = new Command(obj => { if (scheduleView.TileColumnCount > 1) scheduleView.TileColumnCount -= 1; });
        }
    }
}
