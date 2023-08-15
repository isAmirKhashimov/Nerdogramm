using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.Models
{
    internal class Person: INotifyPropertyChanged
    {
        public string LastName
        {
            get => Name.Split()[0];
            set
            {
                var fio = Name.Split();
                Name = $"{value} {fio[1]} {fio[2]}";
            }
        }

        public string FirstName
        {
            get => Name.Split()[1];
            set
            {
                var fio = Name.Split();
                Name = $"{fio[0]} {value} {fio[2]}";
            }
        }

        public string FathersName
        {
            get => Name.Split()[2];
            set
            {
                var fio = Name.Split();
                Name = $"{fio[0]} {fio[1]} {value}";
            }
        }

        public Person(TypizedFieldDataContainer userData)
        {
            Id = userData[nameof(Id)];
            Name = userData[nameof(Name)];
            Citizenship = userData[nameof(Citizenship)];
            WantedMoney = userData[nameof(WantedMoney)];
            EstimatedWorkHoursPerWeek = userData[nameof(EstimatedWorkHoursPerWeek)];
            WorkExperiences = new ObservableCollection<WorkExperience>()
            {
                new WorkExperience()
                {
                    Id = userData[nameof(WorkExperience.Id)],
                    JobName = userData[nameof(WorkExperience.JobName)],
                    Monthes = userData[nameof(WorkExperience.Monthes)]
                }
            };
        }

        public Person() 
        {
            Id = default;
            Name = string.Empty;
            Citizenship = string.Empty;
            WantedMoney = default;
            EstimatedWorkHoursPerWeek = default;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public decimal WantedMoney { get; set; }
        public int EstimatedWorkHoursPerWeek { get; set; }
        public ObservableCollection<WorkExperience> WorkExperiences { get; set; }

        private static Person instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
        public static Person Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Person();
                }
                return instance;
            }
        }
    }
}
