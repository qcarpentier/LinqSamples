﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",  Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The Lion King",    Rating = 8.5f, Year =  1994 },
                new Movie { Title = "Casablanca",       Rating = 8.0f, Year = 1942 },
                new Movie { Title = "Star Wars V",      Rating = 8.7f, Year = 1980 }
            };

            //var query = movies.Where(m => m.Year < 2000);
            //var query = movies.Filter(m => m.Year < 2000).ToList();
            //Console.WriteLine(query.Count());

            //foreach (var movie in query)
            //{
            //    Console.WriteLine(movie.Title);
            //}

                              // Where is a streaming operator
            var query = movies.Where(m => m.Year < 2000)
                              // OrderBy is a non streaming operator
                              .OrderByDescending(m => m.Rating);

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
