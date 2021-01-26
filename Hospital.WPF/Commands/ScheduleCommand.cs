using Hospital.ViewModel;
using Hospital.WPF.Views;
using System;

namespace Hospital.WPF.Commands
{
    public class ScheduleCommand
    {

        private Command _nextDate;
        private Command _previousDate;

        public Command NextDate { get => _nextDate; }
        public Command PreviousDate { get => _previousDate; }

        public ScheduleCommand(ScheduleViewModel scheduleViewModel, Schedule scheduleView)
        {
            _nextDate = new Command(obj => scheduleViewModel.SelectedDate += TimeSpan.FromDays(1));
            _previousDate = new Command(obj => scheduleViewModel.SelectedDate -= TimeSpan.FromDays(1));
        }
    }
}
