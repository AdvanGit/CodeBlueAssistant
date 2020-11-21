using Hospital.UI.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace Hospital.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel(UserAccess.admin);
        }
    }
}
