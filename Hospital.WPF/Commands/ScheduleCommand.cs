﻿using Hospital.ViewModel;
using Hospital.WPF.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Commands
{
    public class ScheduleCommand
    {
        private static ScheduleViewModel _vm;
        private static Schedule _view;

        private static readonly Command _nextDate = new Command(obj => _vm.SelectedDate += TimeSpan.FromDays(1));
        private static readonly Command _previousDate = new Command(obj => _vm.SelectedDate -= TimeSpan.FromDays(1));
        private static readonly Command _addColumn = new Command(obj => { if (_view.TileColumnCount < 5) _view.TileColumnCount += 1; });
        private static readonly Command _removeColumn = new Command(obj => { if (_view.TileColumnCount > 1) _view.TileColumnCount -= 1; });

        private static readonly Command _openTab = new Command(obj =>
        {
            bool isExist = false;
            foreach (Ambulatory control in Main.TabNavigator.Bodies)
            {
                if (control.DataContext.GetType().GetProperty("EntryId").GetValue(control.DataContext).Equals(_vm.CurrentEntry.Id))
                {
                    isExist = true;
                    Main.CurrentPage = control;
                    break;
                }
            }
            if (!isExist)
            {
                var view = new Ambulatory(_vm.CurrentEntry.Id);
                Main.TabNavigator.Bodies.Add(view);
                Main.CurrentPage = view;
            }
        });

        public ScheduleCommand(ScheduleViewModel scheduleViewModel, Schedule scheduleView)
        {
            _vm = scheduleViewModel;
            _view = scheduleView;
        }

        public static Command NextDate => _nextDate;
        public static Command PreviousDate => _previousDate;
        public static Command AddColumn => _addColumn;
        public static Command RemoveColumn => _removeColumn;
        public static Command OpenTab => _openTab;
    }
}
