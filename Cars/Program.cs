using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            // Multiple orders
            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            // Query syntax for multiple orders
            var query2 = from car in cars
                         orderby car.Combined descending, car.Name
                         select car;

            foreach (var car in query2.Take(30))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    // Projection
                    .Select(Car.ParseFromCsv)
                    // Concrete DS
                    .ToList();

            // Query syntax
            //var query = 
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCsv(line);

            //return query.ToList();
        }
    }

}
