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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        MainWindow mainWindow;
        feedPage feedpage;
        public TasksPage(MainWindow mainWindow)
        {
            InitializeComponent();
            
            feedpage = new feedPage(this);
            this.mainWindow = mainWindow;

            this.taskFrame.Content = feedpage;
        }
    }
}
