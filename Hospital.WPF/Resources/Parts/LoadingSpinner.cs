using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hospital.WPF.Resources.Parts
{
    public class LoadingSpinner : Control
    {
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingSpinner), new PropertyMetadata(false));

        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }
        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(double), typeof(LoadingSpinner), new PropertyMetadata(50.0));

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(LoadingSpinner), new PropertyMetadata(1.0));

        public PenLineCap LineCap
        {
            get { return (PenLineCap)GetValue(LineCapProperty); }
            set { SetValue(LineCapProperty, value); }
        }
        public static readonly DependencyProperty LineCapProperty =
            DependencyProperty.Register("LineCap", typeof(PenLineCap), typeof(LoadingSpinner), new PropertyMetadata(PenLineCap.Flat));

        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(LoadingSpinner), new PropertyMetadata(new CornerRadius(0.0)));



        static LoadingSpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingSpinner), new FrameworkPropertyMetadata(typeof(LoadingSpinner)));
        }
    }
}
