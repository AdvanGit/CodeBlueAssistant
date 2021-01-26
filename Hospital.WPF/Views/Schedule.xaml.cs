using Hospital.ViewModel;
using Hospital.WPF.Commands;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Schedule : UserControl
    {
        public static string Label { get; } = "Расписание";

        private static readonly ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
        public ScheduleCommand Command { get; private set; }

        public int TileColumnCount
        {
            get { return (int)GetValue(TileColumnCountProperty); }
            set { SetValue(TileColumnCountProperty, value); }
        }
        public static readonly DependencyProperty TileColumnCountProperty =
            DependencyProperty.Register("TileColumnCount", typeof(int), typeof(Schedule), new PropertyMetadata(3));

        public Schedule()
        {
            DataContext = scheduleViewModel;
            Command = new ScheduleCommand(scheduleViewModel, this);
            InitializeComponent();
        }
    }
}
