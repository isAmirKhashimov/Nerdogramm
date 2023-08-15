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

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        MainWindow mainWindow;
        public AuthWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void NotRegistrated_Click(object sender, MouseButtonEventArgs e)
        {
            mainWindow.regWindow = new RegWindow(mainWindow);
            mainWindow.regWindow.Show();
            this.Hide();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            string pretmpkey = mainWindow.serv.login(loginField.Text, passwordField.Password);
            if (pretmpkey == null)
            {
                MessageBox.Show("Неверные входные данные");
                return;
            }
            mainWindow.tmpkey = pretmpkey;
            mainWindow.Show();
            this.Hide();
        }
    }
}
