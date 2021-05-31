using System.Windows;
using System.Windows.Input;

namespace Hospital.WPF.Resources.Behaviors
{
    /// <summary>
    /// Allow to ignore MouseWheel event 
    /// https://stackoverflow.com/questions/2189053/disable-mouse-wheel-on-itemscontrol-in-wpf
    /// Modified as IInputElement
    /// </summary>
    public sealed class IgnoreMouseWheelBehavior
    {
        public static bool GetIgnoreMouseWheel(DependencyObject element)
        {
            return (bool)element.GetValue(IgnoreMouseWheelProperty);
        }

        public static void SetIgnoreMouseWheel(DependencyObject element, bool value)
        {
            element.SetValue(IgnoreMouseWheelProperty, value);
        }

        public static readonly DependencyProperty IgnoreMouseWheelProperty =
            DependencyProperty.RegisterAttached("IgnoreMouseWheel", typeof(bool),
            typeof(IgnoreMouseWheelBehavior), new UIPropertyMetadata(false, OnIgnoreMouseWheelChanged));

        static void OnIgnoreMouseWheelChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as IInputElement;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.PreviewMouseWheel += OnPreviewMouseWheel;
            else
                item.PreviewMouseWheel -= OnPreviewMouseWheel;
        }

        static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            { RoutedEvent = UIElement.MouseWheelEvent };

            var gv = sender as IInputElement;
            if (gv != null) gv.RaiseEvent(e2);
        }
    }
}
