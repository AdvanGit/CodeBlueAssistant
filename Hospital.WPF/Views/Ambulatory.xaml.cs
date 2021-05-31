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

        public string Label => "Ambulatory";
        public Type Type => typeof(Ambulatory);

        public AmbulatoryCommand Command { get; }

        public Navigator MenuNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>()
        {
            new AmbDiagnostic(),
            new AmbTherapy(),
            new AmbEntry(),
            new AmbReport()
        });
        public Navigator EntryTabNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>() { new AmbEntryInfo(), new AmbEntrySearchBar() });
        public Navigator EntrySearchNavigator { get; } = new Navigator(new ObservableCollection<INavigatorItem>() { new AmbEntrySearchPanel(), new AmbEntrySelectPanel(), new AmbEntrySavePanel() });

        public Ambulatory(int entryId)
        {
            InitializeComponent();
            ambulatoryViewModel = new AmbulatoryViewModel(entryId);
            DataContext = ambulatoryViewModel;
            Command = new AmbulatoryCommand(ambulatoryViewModel, this);

            foreach (INavigatorItem control in MenuNavigator.Bodies) AddLogicalChild(control);
            foreach (INavigatorItem control in EntryTabNavigator.Bodies) AddLogicalChild(control);
            foreach (INavigatorItem control in EntrySearchNavigator.Bodies) AddLogicalChild(control);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MenuNavigator.SetBody(typeof(AmbDiagnostic));
            EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            EntryTabNavigator.SetBody(typeof(AmbEntryInfo));
        }
    }
}
