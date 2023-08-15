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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        MainWindow mainWindow;
        public RegWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void alreadyReg_Click(object sender, MouseButtonEventArgs e)
        {
            mainWindow.authWindow = new AuthWindow(mainWindow);
            mainWindow.authWindow.Show();
            this.Hide();
        }

        private void Registrate_Click(object sender, RoutedEventArgs e)
        {
            if (userPass.Password != userRePass.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            if (mainWindow.serv.register(userName.Text, userLogin.Text, userPass.Password)<0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }
            this.Hide();
            mainWindow.authWindow.Show();
        }
    }
}
