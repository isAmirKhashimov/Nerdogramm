using Nerdogramm.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nerdogramm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RegWindow regWindow;
        public AuthWindow authWindow;
        public TasksPage tasksPage;
        public OrganizationsPage organizationsPage;
        public GroupPage groupPage;
        public GroupMainPage groupMainPage;
        public ServerAPI serv;

        public VacanciesPage VacanciesPage;
        public JobsPage JobsPage;
        public UserCabinetPage UserCabinetPage;
        public AllUsersPage AllUsersPage;
        public string tmpkey;
        public MainWindow()
        {
            InitializeComponent();
            AllUsersPage = new AllUsersPage(new AllUsersPageVM());
            JobsPage = new JobsPage(new JobsPageVM());
            UserCabinetPage = new UserCabinetPage(new UserCabinetVM());
            VacanciesPage = new VacanciesPage(new VacanciesPageVm());
            CurrentPage.Content = UserCabinetPage;


            /*
            serv = new ServerAPI();
            regWindow = new RegWindow(this);
            authWindow = new AuthWindow(this);
            tasksPage = new TasksPage(this);
            groupPage = new GroupPage(this);
            groupMainPage = new GroupMainPage(this);
            userCabinetPage = new UserCabinetPage(this);
            organizationsPage = new OrganizationsPage(this);*/

            /*
            if (!serv.ping())
            {
                MessageBox.Show("Соединения нет");
                Environment.Exit(0);
            }*/

            //authWindow.Show();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void goUserCabinet_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = UserCabinetPage;
        }

        private void goGroups_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = VacanciesPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimizer_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void goAllUsers_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = AllUsersPage;
        }

        private void goJobs_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = JobsPage;
        }
    }
}
