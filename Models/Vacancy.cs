using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.Models
{
    internal class Vacancy : INotifyPropertyChanged
    {
        private static Vacancy instance;

        public Vacancy(TypizedFieldDataContainer userData)
        {
            Id = userData[nameof(Id)];
            JobName = userData[nameof(JobName)];
            Candidate = userData[nameof(Candidate)];
            Company = userData[nameof(Company)];
            Date = userData[nameof(Date)];
        }

        public Vacancy()
        {
            Id = default;
            JobName = string.Empty;
            Candidate = string.Empty;
            Company = string.Empty;
            Date = default;
        }

        public int Id { get; set; }
        public string JobName { get; set; }
        public string Candidate { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static Vacancy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Vacancy();
                }
                return instance;
            }
        }
    }
}
