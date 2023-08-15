using Nerdogramm.ViewModels;
using System.Windows.Controls;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для JobsPage.xaml
    /// </summary>
    public partial class JobsPage : Page
    {
        internal JobsPage(JobsPageVM vm)
        {
            InitializeComponent();
            VM = vm;
            DataContext = vm;
        }

        internal JobsPageVM VM { get; set; }
    }
}
