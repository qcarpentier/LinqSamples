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

            write(square(add(3, 5)));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Quentin" },
                new Employee { Id = 2, Name = "Chris" },
                new Employee { Id = 9, Name = "Sven" },
                new Employee { Id = 10, Name = "Michel" },
                new Employee { Id = 11, Name = "Jonas" },
                new Employee { Id = 12, Name = "Omer" },
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

            // Use the Named method
            //TagNames(developers.Where(NameStartsWithQ));

            // Use the Anonymous method
            //TagNames(developers.Where(
            //    delegate(Employee employee)
            //    {
            //        return employee.Name.StartsWith("Q");
            //    }));

            // Easier syntax > Lambda expression
            var devStartsWithQ = developers.Where(e => e.Name.StartsWith("Q"));

            // Differents syntaxes for the same result
            var querySyntax = from developer in developers
                              where developer.Name.Length <= 5
                              orderby developer.Name descending
                              select developer;
            var methodSyntax = developers.Where(e => e.Name.Length <= 5)
                                         .OrderByDescending(e => e.Name);

            TagNames(devStartsWithQ);
            WriteLine();
            TagNamesHardcore(methodSyntax);
            WriteLine();
            TagNames(querySyntax);
        }

        private static void WriteLine()
        {
            Console.WriteLine();
            Console.WriteLine("***");
            Console.WriteLine();
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
