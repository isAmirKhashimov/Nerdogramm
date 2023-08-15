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
    /// Логика взаимодействия для OrganizationsPage.xaml
    /// </summary>
    public partial class OrganizationsPage : Page
    {
        MainWindow mainWindow;
        public List<ServerAPI.Organisation> Orgs;
        public SolidColorBrush[] brushes { get; set; }
        public OrganizationsPage(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            Orgs = new List<ServerAPI.Organisation>();
            brushes = new SolidColorBrush[10] { new SolidColorBrush(Color.FromRgb(255, 242, 0)),
                                                new SolidColorBrush(Color.FromRgb(0, 168, 243)),
                                                new SolidColorBrush(Color.FromRgb(236, 28, 38)),
                                                new SolidColorBrush(Color.FromRgb(14, 205, 69)),
                                                new SolidColorBrush(Color.FromRgb(255, 174, 200)),
                                                new SolidColorBrush(Color.FromRgb(185, 121, 86)),
                                                new SolidColorBrush(Color.FromRgb(195, 195, 195)),
                                                new SolidColorBrush(Color.FromRgb(253, 236, 166)),
                                                new SolidColorBrush(Color.FromRgb(255, 127, 39)),
                                                new SolidColorBrush(Color.FromRgb(196, 255, 14))};
        }
        public void orgRefresh()
        {
            int tmp = 0;
            Orgs = mainWindow.serv.getOrganisations();
            foreach (var org in Orgs)
            {
                org.colorBrush = brushes[tmp%10];
                tmp++;
            }
            OrgsPanel.ItemsSource = Orgs;
            

            this.DataContext = this;
        }
        private void Organization_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.groupPage.groupsRefresh(Int32.Parse(((Button)e.OriginalSource).Tag.ToString()), ((Button)e.OriginalSource).Content.ToString());
        }

    }
}
