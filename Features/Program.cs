using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Features.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            // Func returns something, last parm is the return type
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            // Action returns void
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3,5)));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Quentin" },
                new Employee { Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new Employee[]
            {
                new Employee { Id = 3, Name = "Alex" },
                new Employee { Id = 4, Name = "Jean-Marc" },
                new Employee { Id = 5, Name = "Karim" },
                new Employee { Id = 6, Name = "Pierre" },
                new Employee { Id = 7, Name = "Jim" },
                new Employee { Id = 8, Name = "Guillaume" }
            };

            Console.WriteLine(developers.Count());

            // Use the Named method
            //TagNames(developers.Where(NameStartsWithQ));

            // Use the Anonymous method
            //TagNames(developers.Where(
            //    delegate(Employee employee)
            //    {
            //        return employee.Name.StartsWith("Q");
            //    }));

            // Easier syntax > Lambda expression
            TagNames(developers.Where(e => e.Name.StartsWith("Q")));
            Console.WriteLine("***");
            TagNamesHardcore(sales.Where(e => e.Name.Length <= 5)
                                  .OrderByDescending(e => e.Name));
        }

        // Named method
        private static bool NameStartsWithQ(Employee employee)
        {
            return employee.Name.StartsWith("Q");
        }

        private static void TagNamesHardcore(IEnumerable<Employee> developers)
        {
            // IEnumerator plays > Hardcore foreach
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
