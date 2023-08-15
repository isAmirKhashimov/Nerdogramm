using Nerdogramm.ViewModels;
using System.Windows.Controls;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для AllUsersPage.xaml
    /// </summary>
    public partial class AllUsersPage : Page
    {
        internal AllUsersPage(AllUsersPageVM vm)
        {
            InitializeComponent();
            VM = vm;
            DataContext = VM;
        }

        internal AllUsersPageVM VM { get; private set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.UpdateUserList();
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            VM.UpdateUserList();
        }
    }
}
