using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Quentin" },
                new Employee { Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new Employee[]
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            TagNames(developers);
            Console.WriteLine("***");
            TagNamesHardcore(developers);
        }

        private static void TagNamesHardcore(IEnumerable<Employee> developers)
        {
            // Hardcore foreach
            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }

        private static void TagNames(IEnumerable<Employee> developers)
        {
            foreach (var dev in developers)
            {
                Console.WriteLine(dev.Name);
            }
        }
    }
}
