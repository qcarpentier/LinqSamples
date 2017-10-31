using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class CarStatistics
    {
        // Instanciate the accumulator
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }
        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Avg = Total / Count;

            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public double Avg { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
    }
}
