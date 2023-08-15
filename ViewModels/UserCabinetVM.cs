using Nerdogramm.Adds;
using Nerdogramm.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Nerdogramm.ViewModels
{
    internal class UserCabinetVM : INotifyPropertyChanged
    {
        private ICommand saveCommand;
        private Person user;

        public UserCabinetVM()
        {
            User = ApiClient.Instance.GetUserById(1).Result;
        }

        public Person User { get => user; set
            {
                user = value;
                OnPropertyChanged();
            } }


        public ICommand AddCommand
        {
            get
            {
                return saveCommand ??= new CommandHandler(() => AddExp(), () => CanExecute);
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??= new CommandHandler(() => Save(), () => CanExecute);
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddExp()
        {
            User.WorkExperiences.Add(new WorkExperience(true) { JobName = "Должность", Monthes = 12 });
            OnPropertyChanged(nameof(User));
            User.OnPropertyChanged(nameof(User.WorkExperiences));
        }
        private async void Save()
        {
            await ApiClient.Instance.UpdateUserInfo(User);
        }
    }
}
