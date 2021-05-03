using Hospital.ViewModel.Ambulatory;
using Hospital.WPF.Commands;
using Hospital.WPF.Controls.Ambulatory;
using Hospital.WPF.Navigators;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Hospital.WPF.Views
{
    public partial class Ambulatory : UserControl, INavigatorItem
    {
        private AmbulatoryViewModel ambulatoryViewModel;

        public AmbulatoryCommand Command { get; }
        public AmbulatoryNavigator Navigator { get; } = new AmbulatoryNavigator();

        public Navigator EntryTabNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>() { new AmbEntryInfo(), new AmbEntrySearchBar() });
        public Navigator EntrySearchNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>() { new AmbEntrySearchPanel(), new AmbEntrySelectPanel(), new AmbEntrySavePanel() });

        public string Label => "Ambulatory";
        public Type Type => typeof(Ambulatory);

        public Ambulatory(int entryId)
        {
            InitializeComponent();
            ambulatoryViewModel = new AmbulatoryViewModel(entryId);
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
