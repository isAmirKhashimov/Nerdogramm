using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.Models
{
    internal class WorkExperience : INotifyPropertyChanged
    {
        public WorkExperience(bool isNew = false)
        {
            IsNew = isNew;
        }
        public bool IsNew { get; private set; }
        public int Id { get; set; }
        public string JobName
        {
            get => jobName; set
            {
                jobName = value;
                OnPropertyChanged();
            }
        }
        public int Monthes
        {
            get => monthes; set
            {
                monthes = value;
                OnPropertyChanged();
            }
        }

        private static WorkExperience instance;
        private string jobName;
        private int monthes;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static WorkExperience Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkExperience() { JobName = string.Empty };
                }
                return instance;
            }
        }
    }
}
