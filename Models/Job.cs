using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nerdogramm.Models
{
    internal class Job: INotifyPropertyChanged
    {
        private static Job instance;

        public Job(TypizedFieldDataContainer userData)
        {
            Id = userData[nameof(Id)];
            Name = userData[nameof(Name)];
            IsOverTime = userData[nameof(IsOverTime)];
            DefaultMoney = userData[nameof(DefaultMoney)];
            Hours = userData[nameof(Hours)];
            MinMonthes = userData[nameof(MinMonthes)];
        }

        public Job()
        {
            Id = default;
            Name = string.Empty;
            MinMonthes = default;
            DefaultMoney = default;
            IsOverTime = default;
            Hours = default;
        }

        public int Id { get; set; }
        public int MinMonthes { get; set; }
        public string Name { get; set; }
        public decimal DefaultMoney { get; set; }
        public bool IsOverTime { get; set; }
        public int Hours { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }

        public static Job Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Job();
                }
                return instance;
            }
        }
    }
}
