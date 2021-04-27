using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Commands;
using Hospital.WPF.Controls.Ambulatory;
using Hospital.WPF.Navigators;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Ambulatory : UserControl
    {
        public static string Label => "Амбулатория";

        private AmbulatoryViewModel ambulatoryViewModel = new AmbulatoryViewModel();

        public AmbulatoryCommand Command { get; }
        public AmbulatoryNavigator Navigator { get; } = new AmbulatoryNavigator();

        public Navigator EntryTabNavigator { get; } = new Navigator(new ObservableCollection<UserControl>() { new AmbEntryInfo(), new AmbEntrySearchBar() });
        public Navigator EntrySearchNavigator { get; } = new Navigator(new ObservableCollection<UserControl>() { new AmbEntrySearchPanel(), new AmbEntrySelectPanel(), new AmbEntrySavePanel() });

        public Ambulatory()
        {
            InitializeComponent();
            DataContext = ambulatoryViewModel;
            Command = new AmbulatoryCommand(ambulatoryViewModel, this);

            foreach (UserControl control in Navigator.GetBodies()) AmbulatoryView.AddLogicalChild(control);
            Navigator.SetBody("Diagnostic");

            foreach (UserControl control in EntryTabNavigator.Bodies) AddLogicalChild(control);
            foreach (UserControl control in EntrySearchNavigator.Bodies) AddLogicalChild(control);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            EntryTabNavigator.SetBody(typeof(AmbEntryInfo));
        }
    }
}
