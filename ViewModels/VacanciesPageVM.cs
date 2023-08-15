using Nerdogramm.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.ViewModels
{
    internal class VacanciesPageVm : INotifyPropertyChanged
    {
        private ObservableCollection<Vacancy> vacancies;
        private bool okay;
        private ObservableCollection<Job> jobs;
        private Job job;
        private ObservableCollection<Person> users;
        private Person user;

        public VacanciesPageVm()
        {
            Vacancies = new ObservableCollection<Vacancy>(ApiClient.Instance.GetVacancies(null).Result);
            Users = new ObservableCollection<Person>(ApiClient.Instance.GetUsers().Result);
            Jobs = new ObservableCollection<Job>(ApiClient.Instance.GetJobs().Result);
        }

        internal void UpdateUserList()
        {
            Vacancies = new ObservableCollection<Vacancy>(ApiClient.Instance.GetVacancies(new Filters() { JobName = Job?.Name, Candidate = User?.Name }).Result);
        }

        public Person User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Person> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }

        public Job Job
        {
            get => job;
            set
            {
                job = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Job> Jobs
        {
            get => jobs;
            set
            {
                jobs = value;
                OnPropertyChanged();
            }
        }

        public bool Okay
        {
            get => okay;
            set
            {
                okay = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Vacancy> Vacancies
        {
            get => vacancies;
            set
            {
                vacancies = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
