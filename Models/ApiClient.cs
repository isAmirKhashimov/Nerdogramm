using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdogramm.Models
{
    internal class ApiClient
    {

        private readonly string host;
        private readonly string user;
        private readonly string dataBaseName;
        private readonly string password;
        private readonly string port;
        private int currentUserId;

        private readonly NpgsqlConnection connection;

        private static ApiClient instance;
        public static ApiClient Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ApiClient();
                }
                return instance;
            }
        }

        private ApiClient()
        {
            host = "127.0.0.1";
            user = "postgres";
            dataBaseName = "StarLabs";
            password = "postgres";
            port = "5432";

            string connString =
                string.Format(
                    "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};SSLMode=Prefer",
                    host,
                    user,
                    dataBaseName,
                    port,
                    password);

            connection = new NpgsqlConnection(connString);
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public async Task<List<object[]>> CreateRequest(string request)
        {
            var result = new List<object[]>();
            using (var command = new NpgsqlCommand(request, connection))
            {
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    var row = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader.GetValue(i);
                    }
                    result.Add(row);
                }
                int a;
                reader.Close();
            }
            return result;
        }

        public async Task UpdateUserInfo(Person person)
        {
            OpenConnection();
            var result = await CreateRequest(@$"
            UPDATE ""Кандидаты""
            SET ""ФИО_Кандидата"" = '{person.Name}', ""Гражданство"" = '{person.Citizenship}',
            ""Планируемое время работы"" = {person.EstimatedWorkHoursPerWeek}, ""Желаемый оклад"" = {(int)person.WantedMoney}
            WHERE ""Id_Кандидата"" = {person.Id}
            ");
            CloseConnection();
        }

        public async Task<Person> GetUserById(int id)
        {
            OpenConnection();
            var result = await CreateRequest(@"
                SELECT ""Кандидаты"".""Id_Кандидата"", ""ФИО_Кандидата"", 
                ""Желаемый оклад"", ""Гражданство"", ""Планируемое время работы"",
                ""Название_Должности"", ""Опыт_Работы""
                FROM ""Кандидаты""
                JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                WHERE ""Кандидаты"".""Id_Кандидата"" = " + id);

            Person user = null;
            foreach (var row in result)
            {
                var userData = new TypizedFieldDataContainer(new TypizedFieldData[]
                {
                    new TypizedFieldData() { Name = nameof(Person.Instance.Id), TType = Person.Instance.Id.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Name), TType = Person.Instance.Name.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.WantedMoney), TType = Person.Instance.WantedMoney.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Citizenship), TType = Person.Instance.Citizenship.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.EstimatedWorkHoursPerWeek), TType = Person.Instance.EstimatedWorkHoursPerWeek.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.JobName), TType = WorkExperience.Instance.JobName.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.Monthes), TType = WorkExperience.Instance.Monthes.GetType() },
                }, row);


                if (user != null)
                {
                    user.WorkExperiences.Add(new WorkExperience()
                    {
                        Id = id,
                        JobName = userData[nameof(WorkExperience.Instance.JobName)],
                        Monthes = userData[nameof(WorkExperience.Instance.Monthes)],
                    });
                }
                else
                {
                    user = new Person(userData) { Id = id };
                }
            }
            CloseConnection();
            return user;
        }

        public async Task<List<Person>> GetUsers()
        {
            OpenConnection();
            var result = await CreateRequest(@"
                SELECT ""Кандидаты"".""Id_Кандидата"", ""ФИО_Кандидата"", 
                ""Желаемый оклад"", ""Гражданство"", ""Планируемое время работы"",
                ""Название_Должности"", ""Опыт_Работы""
                FROM ""Кандидаты""
                JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                ORDER BY ""Кандидаты"".""Id_Кандидата""
                ");
            var users = new Dictionary<int, Person>();
            foreach(var row in result)
            {
                var userData = new TypizedFieldDataContainer( new TypizedFieldData[]
                {
                    new TypizedFieldData() { Name = nameof(Person.Instance.Id), TType = Person.Instance.Id.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Name), TType = Person.Instance.Name.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.WantedMoney), TType = Person.Instance.WantedMoney.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Citizenship), TType = Person.Instance.Citizenship.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.EstimatedWorkHoursPerWeek), TType = Person.Instance.EstimatedWorkHoursPerWeek.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.JobName), TType = WorkExperience.Instance.JobName.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.Monthes), TType = WorkExperience.Instance.Monthes.GetType() },
                }, row);

                if (users.TryGetValue(userData[nameof(Person.Instance.Id)], out Person user))
                {
                    user.WorkExperiences.Add(new WorkExperience() 
                    { 
                        Id = userData[nameof(Person.Instance.Id)], 
                        JobName = userData[nameof(WorkExperience.Instance.JobName)],  
                        Monthes = userData[nameof(WorkExperience.Instance.Monthes)], 
                    });
                }
                else
                {
                    users.Add(userData[nameof(Person.Instance.Id)], new Person(userData));
                }
            }
            
            CloseConnection();
            return users.Values.ToList();
        }

        public async Task<List<Person>> GetUsersWithJobFilters(Job job, bool okay = false)
        {
            OpenConnection();
            List<object[]> result;
            
            if (okay)
            {
                result = await CreateRequest(@$"
                SELECT ""Кандидаты"".""Id_Кандидата"", ""ФИО_Кандидата"", 
                ""Желаемый оклад"", ""Гражданство"", ""Планируемое время работы"",
                ""Название_Должности"", ""Опыт_Работы""
                FROM ""Кандидаты""
                JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                WHERE ""Название_Должности"" = '{job.Name}' AND ""Опыт_Работы"" >= {job.MinMonthes}
                ORDER BY ""Кандидаты"".""Id_Кандидата""
                ");
            }
            else
            {
                result = await CreateRequest(@$"
                SELECT ""Кандидаты"".""Id_Кандидата"", ""ФИО_Кандидата"", 
                ""Желаемый оклад"", ""Гражданство"", ""Планируемое время работы"",
                ""Название_Должности"", ""Опыт_Работы""
                FROM ""Кандидаты""
                JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                WHERE ""Название_Должности"" = '{job.Name}' 
                ORDER BY ""Кандидаты"".""Id_Кандидата""
                ");
            }
            var users = new Dictionary<int, Person>();
            foreach (var row in result)
            {
                var userData = new TypizedFieldDataContainer(new TypizedFieldData[]
                {
                    new TypizedFieldData() { Name = nameof(Person.Instance.Id), TType = Person.Instance.Id.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Name), TType = Person.Instance.Name.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.WantedMoney), TType = Person.Instance.WantedMoney.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.Citizenship), TType = Person.Instance.Citizenship.GetType() },
                    new TypizedFieldData() { Name = nameof(Person.Instance.EstimatedWorkHoursPerWeek), TType = Person.Instance.EstimatedWorkHoursPerWeek.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.JobName), TType = WorkExperience.Instance.JobName.GetType() },
                    new TypizedFieldData() { Name = nameof(WorkExperience.Instance.Monthes), TType = WorkExperience.Instance.Monthes.GetType() },
                }, row);

                if (users.TryGetValue(userData[nameof(Person.Instance.Id)], out Person user))
                {
                    user.WorkExperiences.Add(new WorkExperience()
                    {
                        Id = userData[nameof(Person.Instance.Id)],
                        JobName = userData[nameof(WorkExperience.Instance.JobName)],
                        Monthes = userData[nameof(WorkExperience.Instance.Monthes)],
                    });
                }
                else
                {
                    users.Add(userData[nameof(Person.Instance.Id)], new Person(userData));
                }
            }

            CloseConnection();
            return users.Values.ToList();
        }


        public async Task<List<Job>> GetJobs()
        {
            OpenConnection();
            var result = await CreateRequest(@"
                SELECT *
                FROM ""Должности""
                ");
            var jobs = new List<Job>();
            foreach (var row in result)
            {
                var jobData = new TypizedFieldDataContainer(new TypizedFieldData[]
                {
                    new TypizedFieldData() { Name = nameof(Job.Instance.Id), TType = Job.Instance.Id.GetType() },
                    new TypizedFieldData() { Name = nameof(Job.Instance.Name), TType = Job.Instance.Name.GetType() },
                    new TypizedFieldData() { Name = nameof(Job.Instance.MinMonthes), TType = Job.Instance.MinMonthes.GetType() },
                    new TypizedFieldData() { Name = nameof(Job.Instance.DefaultMoney), TType = Job.Instance.DefaultMoney.GetType() },
                    new TypizedFieldData() { Name = nameof(Job.Instance.IsOverTime), TType = Job.Instance.IsOverTime.GetType() },
                    new TypizedFieldData() { Name = nameof(Job.Instance.Hours), TType = Job.Instance.Hours.GetType() }
                }, row);
                
                jobs.Add(new Job(jobData));
            }

            CloseConnection();
            return jobs.ToList();
        }


        public async Task<List<Vacancy>> GetVacancies(Filters filters)
        {
            OpenConnection();
            List<object[]> result;
            if (filters == null)
            {
                result = await CreateRequest(@"
                SELECT DISTINCT ""Id_Заявки"", ""ФИО_Кандидата"", ""Имя_Компании"", ""Имя_Должности"", ""Дата подачи""  FROM ""Кандидаты""
                JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                JOIN ""Заявки"" ON ""Заявки"".""Id_Кандидата"" = ""Кандидаты"".""Id_Кандидата""
                JOIN ""Должности"" ON ""Id_Должности"" = ""Id_Вакансии""
                JOIN ""Компании"" ON ""Компании"".""Id_Компании"" = ""Заявки"".""Id_Компании""
                ORDER BY ""Id_Заявки""
                ");
            }
            else
            {
                var cond = new List<string>();
                if (!string.IsNullOrEmpty(filters.Candidate))
                {
                    cond.Add(@$"""ФИО_Кандидата"" = '{filters.Candidate}'");
                }
                if (!string.IsNullOrEmpty(filters.CompanyName))
                {
                    cond.Add(@$"""Имя_Компании"" = '{filters.CompanyName}'");
                }
                if (!string.IsNullOrEmpty(filters.JobName))
                {
                    cond.Add(@$"""Имя_Должности"" = '{filters.JobName}'");
                }

                string request;
                if (cond.Count > 0)
                {
                    request = @$"
                              SELECT DISTINCT ""Id_Заявки"", ""ФИО_Кандидата"", ""Имя_Компании"", ""Имя_Должности"", ""Дата подачи""  FROM ""Кандидаты""
                              JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                              JOIN ""Заявки"" ON ""Заявки"".""Id_Кандидата"" = ""Кандидаты"".""Id_Кандидата""
                              JOIN ""Должности"" ON ""Id_Должности"" = ""Id_Вакансии""
                              JOIN ""Компании"" ON ""Компании"".""Id_Компании"" = ""Заявки"".""Id_Компании""
                              WHERE {string.Join(" AND ", cond)}
                              ORDER BY ""Id_Заявки""
                ";
                }
                else
                {
                    request = @$"
                              SELECT DISTINCT ""Id_Заявки"", ""ФИО_Кандидата"", ""Имя_Компании"", ""Имя_Должности"", ""Дата подачи""  FROM ""Кандидаты""
                              JOIN ""Опыт работы"" ON ""Кандидаты"".""Id_Кандидата"" = ""Опыт работы"".""Id_Кандидата""
                              JOIN ""Заявки"" ON ""Заявки"".""Id_Кандидата"" = ""Кандидаты"".""Id_Кандидата""
                              JOIN ""Должности"" ON ""Id_Должности"" = ""Id_Вакансии""
                              JOIN ""Компании"" ON ""Компании"".""Id_Компании"" = ""Заявки"".""Id_Компании""
                              ORDER BY ""Id_Заявки""
                ";
                }

                
                result = await CreateRequest(request);
            }
            var vacancies = new List<Vacancy>();
            foreach (var row in result)
            {
                var jobData = new TypizedFieldDataContainer(new TypizedFieldData[]
                {
                    new TypizedFieldData() { Name = nameof(Vacancy.Instance.Id), TType = Vacancy.Instance.Id.GetType() },
                    new TypizedFieldData() { Name = nameof(Vacancy.Instance.Candidate), TType = Vacancy.Instance.Candidate.GetType() },
                    new TypizedFieldData() { Name = nameof(Vacancy.Instance.Company), TType = Vacancy.Instance.Company.GetType() },
                    new TypizedFieldData() { Name = nameof(Vacancy.Instance.JobName), TType = Vacancy.Instance.JobName.GetType() },
                    new TypizedFieldData() { Name = nameof(Vacancy.Instance.Date), TType = Vacancy.Instance.Date.GetType() }
                }, row);

                vacancies.Add(new Vacancy(jobData));
            }

            CloseConnection();
            return vacancies.ToList();
        }
    }
}
