using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            bt_authorization.Click += bt_auth_Click;
            bt_cancel.Click += bt_can_Click;
        }
        
        
        private void bt_auth_Click(object sender, RoutedEventArgs e)
        {
            if ((tb_username.Text != "") && (tb_username.Text == pb_password.Password))
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            else MessageBox.Show($"Неверные данные {tb_username.Text} != {pb_password.Password}");
        }
        
        private void bt_can_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
