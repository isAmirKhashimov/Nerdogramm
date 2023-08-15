using Nerdogramm.ViewModels;
using System.Windows.Controls;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для UserCabinetPage.xaml
    /// </summary>
    /// 
    public partial class UserCabinetPage : Page
    {
        internal UserCabinetPage(UserCabinetVM vm)
        {
            InitializeComponent();
            VM = vm;
            DataContext = VM;
        }

        internal UserCabinetVM VM { get; private set; }
    }
}
