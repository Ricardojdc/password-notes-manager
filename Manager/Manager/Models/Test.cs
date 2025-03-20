using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Models
{
    class Test
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public Test() { }


        public static List<Test> GeneratePeople(int count)
        {
            string[] names = { "John", "Alice", "Robert", "Emma", "Michael", "Sophia", "William", "Olivia", "James", "Isabella" };
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Miami", "Dallas", "San Francisco", "Seattle", "Boston", "Denver" };

            Random random = new Random();
            return Enumerable.Range(1, count).Select(i => new Test
            {
                Name = $"{names[random.Next(names.Length)]} {i}",
                Age = random.Next(18, 80),
                City = cities[random.Next(cities.Length)]
            }).ToList();

        }
    }
}
