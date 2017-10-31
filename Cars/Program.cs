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
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            // Multiple orders
            var query = 
                cars//.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name)
                    .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);

            // Query syntax for multiple orders
            var query2 =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name
                // Annonymous method to project a car with only 3 columns
                // (Columns we actually need)
                select new
                {
                    //Manufacturer = car.Manufacturer,
                    car.Manufacturer,
                    car.Name,
                    car.Combined
                };

            // Projection with LINQ
            var query3 =
                cars.Select(c => new
                {
                    c.Manufacturer,
                    c.Name,
                    c.Combined
                });

            // Projection to take only the name
            var query4 =
                cars.Select(c => c.Name);

            var query5 =
                cars.SelectMany(c => c.Name)
                    .OrderBy(c => c);

            // Join with query syntax
            var query6 =
                from car in cars
                join manufacturer in manufacturers
                   on new { car.Manufacturer, car.Year }
                   equals new { Manufacturer = manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name ascending
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            // Join with method syntax
            var query7 =
                cars.Join(manufacturers,
                        c => new { c.Manufacturer, c.Year },
                        m => new { Manufacturer = m.Name, m.Year },
                        (c, m) => new
                        {
                            m.Headquarters,
                            c.Name,
                            c.Combined
                        })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            // Grouping with query syntax
            var query8 =
                from car in cars
                group car by car.Manufacturer.ToUpper()
                            into manufacturer
                // Ordering the manufacturer group
                orderby manufacturer.Key
                select manufacturer;

            // Grouping with method syntax
            var query9 =
                cars.GroupBy(c => c.Manufacturer.ToUpper())
                    .OrderBy(c => c.Key);

            // Grouping and joining with query syntax
            var query10 =
                from manufacturer in manufacturers
                join car in cars on manufacturer.Name equals car.Manufacturer
                  into carGroup
                orderby manufacturer.Name
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                };

            // Grouping and joining with method syntax
            var query11 =
                manufacturers.GroupJoin(
                    cars, m => m.Name, c => c.Manufacturer,
                    (m, g) => new
                    {
                        Manufacturer = m,
                        Cars = g
                    }).OrderBy(m => m.Manufacturer.Name);

            // Challenge: find cars with the most fuel efficiency by country
            var query12 =
                from manufacturer in manufacturers
                join car in cars on manufacturer.Name equals car.Manufacturer
                  into carGroup
                orderby manufacturer.Name
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                } into result
                group result by result.Manufacturer.Headquarters;

            var query13 =
                manufacturers.GroupJoin(
                    cars, m => m.Name, c => c.Manufacturer,
                    (m, g) => new
                    {
                        Manufacturer = m,
                        Cars = g
                    }).GroupBy(m => m.Manufacturer.Headquarters);

            // Aggregating > compute statistics to find the Max/Min/Avg value in that sequence
            // Query syntax
            var query14 =
                from car in cars
                group car by car.Manufacturer
                            into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Max descending
                select result;

            

            foreach (var result in query14)
            {
                Console.WriteLine($"{result.Name.ToUpper()}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }

            //foreach (var group in query13)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    // Ordering elements in group
            //    foreach (var car in group.SelectMany(g => g.)
            //                             .OrderByDescending(c => c.Combined)
            //                             .Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //foreach (var group in query10)
            //{
            //    Console.WriteLine($"{group.Manufacturer.Name}: {group.Manufacturer.Headquarters}");
            //    // Ordering elements in group
            //    foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //foreach (var group in query9)
            //{
            //    Console.WriteLine(group.Key);
            //    // Ordering elements in group
            //    foreach (var car in group.OrderByDescending(c => c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}

            //foreach (var car in query7.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} : {car.Name}");
            //}

            // Will prompt all the character in each words
            //foreach (var name in query4)
            //{
            //    foreach (var character in name)
            //    {
            //        Console.WriteLine(character);
            //    }
            //}

            // Same result 
            //foreach (var character in query5)
            //{
            //    Console.WriteLine(character);
            //}

            // All is Ford? 
            //var result = cars.All(c => c.Manufacturer == "Ford");
            //Console.WriteLine(result);

            //Console.WriteLine($"{query.Manufacturer} {query.Name} : {query.Combined}");

            //foreach (var car in query2.Take(20))
            //{
            //    Console.WriteLine($"{car.Manufacturer} : {car.Combined}");
            //}
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .ToManufacturer()
                            .ToList();

            return query;
        }

        private static List<Car> ProcessCars(string path)
        {
            var query = File.ReadAllLines(path)
                            .Skip(1)
                            .Where(line => line.Length > 1)
                            // Projection
                            //.Select(Car.ParseFromCsv)
                            // Custom projection: from string to Car
                            .ToCar()
                            // Concrete DS
                            .ToList();

            return query;

            // Query syntax
            //var query = 
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCsv(line);

            //return query.ToList();
        }
    }

}
