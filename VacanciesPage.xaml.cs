using Nerdogramm.ViewModels;
using System.Windows.Controls;

namespace Nerdogramm
{
    /// <summary>
    /// Логика взаимодействия для VacanciesPage.xaml
    /// </summary>
    public partial class VacanciesPage : Page
    {
        internal VacanciesPage(VacanciesPageVm vm)
        {
            InitializeComponent();
            VM = vm;
            DataContext = vm;
        }

        internal VacanciesPageVm VM { get; set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.UpdateUserList();
        }
    }
}
