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
    /// Логика взаимодействия для GroupMainPage.xaml
    /// </summary>
    public partial class GroupMainPage : Page
    {
        MainWindow mainWindow;
        ServerAPI.GroupInfo groupInfo;
        Group group;
        public GroupMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        public void refresh(Group refgroup, string orgname)
        {
            group = refgroup;

            groupInfo = mainWindow.serv.getGroupInfo(mainWindow.tmpkey, refgroup.id);


            /*
            groupInfo = new ServerAPI.GroupInfo();
            groupInfo.description = "Some Decription";
            groupInfo.messages = new List<ServerAPI.Message>();
            groupInfo.messages.Add(new ServerAPI.Message(4, "Отрефакторьте код по ссылке далее: "));
            groupInfo.messages.Add(new ServerAPI.Message(3, "Создайте торговую площадку: "));
            groupInfo.messages.Add(new ServerAPI.Message(2, "Взломайте пентагон: "));
            groupInfo.messages.Add(new ServerAPI.Message(4, "Отрефакторьте код по ссылке далее: "));
            groupInfo.messages.Add(new ServerAPI.Message(3, "Создайте торговую площадку: "));
            groupInfo.messages.Add(new ServerAPI.Message(2, "Взломайте пентагон: "));*/

            if (groupInfo != null)
            {
                messageList.ItemsSource = groupInfo.messages;
                descriptionField.Text = groupInfo.description;
            }
            else
            {
                messageList.ItemsSource = new List<ServerAPI.Message>();
                descriptionField.Text = "У Вас недостаточно рейтинга для данной группы";
            }
                
            ruleTags.ItemsSource = group.tags;

            groupName.Text = group.header;
            groupNameField.Text = group.header;
            orgName.Text = orgname;


        }
    }
}
