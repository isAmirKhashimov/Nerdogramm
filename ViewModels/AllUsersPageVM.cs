using Nerdogramm.Adds;
using Nerdogramm.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Nerdogramm.ViewModels
{
    internal class AllUsersPageVM : INotifyPropertyChanged
    {
        private ObservableCollection<Person> users;
        private ObservableCollection<Job> jobs;
        private Job job;
        private bool okay;

        public AllUsersPageVM()
        {
            Users = new ObservableCollection<Person>(ApiClient.Instance.GetUsers().Result);
            Jobs = new ObservableCollection<Job>(ApiClient.Instance.GetJobs().Result);
        }

        internal void UpdateUserList()
        {
            if (Job != null)
            {
                Users = new ObservableCollection<Person>(ApiClient.Instance.GetUsersWithJobFilters(Job, okay).Result);
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
