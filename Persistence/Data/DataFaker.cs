using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public static class DataFaker
    {
        public static List<Student> GetStudentsData()
        {
            Random rnd = new();
            List<Student> students = new List<Student>();
            for (int i = 1; i < 100; i++)
            {
                students.Add(new Student
                {
                    Id = i,
                    Name = Faker.Name.FullName(),
                    Birth_Date = DateTime.Now.AddYears(-rnd.Next(18, 25)),
                    ClassId = rnd.Next(1, 5),
                    CountryId = rnd.Next(1, 10),
                });
            }
            return students;
        }
        public static List<Class> GetClassesData()
        {
            List<Class> classes = new();
            classes.Add(new Class { Id = 1, Name = "First Class" });
            classes.Add(new Class { Id = 2, Name = "Second Class" });
            classes.Add(new Class { Id = 3, Name = "Thired Class" });
            classes.Add(new Class { Id = 4, Name = "Fourth Class" });
            classes.Add(new Class { Id = 5, Name = "Fifith Class" });
            return classes;
        }
        public static List<Country> GetCountriesData()
        {
            List<Country> countries = new();
            for (int i = 1; i < 10; i++)
            {
                countries.Add(new Country
                {
                    Id = i,
                    Name = Faker.Country.Name(),
                });
            }
            return countries;
        }

    }
}
