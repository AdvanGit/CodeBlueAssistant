using Hospital.ViewModel;
using Hospital.ViewModel.Factories;
using Hospital.WPF.Commands;
using Hospital.WPF.Navigators;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Schedule : UserControl, INavigatorItem
    {
        public string Label => "Расписание";
        public Type Type => GetType();

        public ScheduleCommand Command { get; private set; }


        public Schedule(ScheduleViewModel scheduleViewModel)
        {
            DataContext = scheduleViewModel;
            Command = new ScheduleCommand(this);
            InitializeComponent();
        }

        #region dependency properties for column managment

        public int TileColumnCount
        {
            get { return (int)GetValue(TileColumnCountProperty); }
            set { SetValue(TileColumnCountProperty, value); }
        }
        public static readonly DependencyProperty TileColumnCountProperty =
            DependencyProperty.Register("TileColumnCount", typeof(int), typeof(Schedule), new PropertyMetadata(3));

        public GridLength InfoPanelWidth
        {
            get { return (GridLength)GetValue(InfoPanelWidthProperty); }
            set { SetValue(InfoPanelWidthProperty, value); }
        }
        public static readonly DependencyProperty InfoPanelWidthProperty =
            DependencyProperty.Register("InfoPanelWidth", typeof(GridLength), typeof(Schedule), new PropertyMetadata(new GridLength(299)));

        public GridLength GridLenghtAuto
        {
            get { return (GridLength)GetValue(GridLenghtAutoProperty); }
            set { SetValue(GridLenghtAutoProperty, value); }
        }
        public static readonly DependencyProperty GridLenghtAutoProperty =
            DependencyProperty.Register("GridLenghtAuto", typeof(GridLength), typeof(Schedule), new PropertyMetadata(new GridLength(0)));

        #endregion

    }
}
