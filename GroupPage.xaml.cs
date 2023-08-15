using System.Collections.Generic;
using System.Windows.Controls;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        MainWindow mainWindow;
        List<Group> groups;
        string orgName;
        public GroupPage(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            groups = new List<Group>();
            groupList.ItemsSource = groups;
        }
        public void groupsRefresh(int index, string orgname)
        {
            groups = mainWindow.serv.getGroups(index);
            groupList.ItemsSource = groups;

            orgName = orgname;
            grTitle.Text = $"Группы {orgName}";
        }

        
        private void Group_Selected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mainWindow.groupMainPage.refresh(groups[((ListBox)e.OriginalSource).SelectedIndex], orgName);
            }
            catch
            {
                return;
            }
        }
    }
}
