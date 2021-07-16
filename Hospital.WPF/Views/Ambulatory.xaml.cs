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
        public string Label => "Ambulatory";
        public Type Type => GetType();

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


        public Ambulatory(AmbulatoryViewModel ambulatoryViewModel)
        {
            DataContext = ambulatoryViewModel;

            Command = new AmbulatoryCommand(this);

            foreach (INavigatorItem control in MenuNavigator.Bodies) AddLogicalChild(control);
            foreach (INavigatorItem control in EntryTabNavigator.Bodies) AddLogicalChild(control);
            foreach (INavigatorItem control in EntrySearchNavigator.Bodies) AddLogicalChild(control);

            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            MenuNavigator.SetBody(typeof(AmbDiagnostic));
            EntrySearchNavigator.SetBody(typeof(AmbEntrySearchPanel));
            EntryTabNavigator.SetBody(typeof(AmbEntryInfo));

            base.OnApplyTemplate();
        }
    }
}
