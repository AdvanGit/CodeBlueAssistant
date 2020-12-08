using Hospital.UI.ViewModels;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.UI.Views
{
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            _getControls(UserAccess.admin);
        }

        public List<UserControl> Pages
        {
            get { return (List<UserControl>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }
        public UserControl CurrentPage
        {
            get { return (UserControl)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register("Pages", typeof(List<UserControl>), typeof(Main));
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(UserControl), typeof(Main));

        private void _getControls (UserAccess userAccess)
        {
            switch (userAccess)
            {
                case UserAccess.admin:
                    Pages = new List<UserControl>
                    {
                        new Registrator(),
                        new Schedule(),
                        new Ambulatory()
                    };
                    CurrentPage = Pages[0];
                    break;
                case UserAccess.doctor:
                    break;
                case UserAccess.registrator:
                    Pages = new List<UserControl>
                    {
                        new Registrator(),
                    };
                    CurrentPage = Pages[0];
                    break;
                case UserAccess.manager:
                    break;
                default:
                    break;
            }

        }
    }
}
