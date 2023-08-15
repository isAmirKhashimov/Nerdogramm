using Nerdogramm.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.ViewModels
{
    internal class JobsPageVM : INotifyPropertyChanged
    {
        private ObservableCollection<Job> jobs;
        public JobsPageVM()
        {
            Jobs = new ObservableCollection<Job>(ApiClient.Instance.GetJobs().Result);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
