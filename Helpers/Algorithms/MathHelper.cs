using AoC_Helper_Methods.Algorithms.Interfaces;
using AoC_Helper_Methods.Models;

namespace AoC_Helper_Methods.Algorithms
{
    public class MathHelper : IMathHelper
    {
        public long GreatestCommonDivisor(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public long GreatestCommonDivisor(IEnumerable<long> numbers)
        {
            return numbers.Aggregate(GreatestCommonDivisor);
        }

        public int GreatestCommonDivisor(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        public int GreatestCommonDivisor(IEnumerable<int> numbers)
        {
            return numbers.Aggregate(GreatestCommonDivisor);
        }

        public long LeastCommonMultiple(long a, long b)
        {
            return a * (b / GreatestCommonDivisor(a, b));
        }

        public long LeastCommonMultiple(IEnumerable<long> numbers)
        {
            return numbers.Aggregate(LeastCommonMultiple);
        }

        public int LeastCommonMultiple(int a, int b)
        {
            return a * (b / GreatestCommonDivisor(a, b));
        }

        public int LeastCommonMultiple(IEnumerable<int> numbers)
        {
            return numbers.Aggregate(LeastCommonMultiple);
        }

        public int CalculateSurfaceAreaOfCoordinateGrid(Coordinate[] coordinates)
        {
            var result = 0;
            var numberOfCoordinates = coordinates.Count();

            for (var i = 0; i < numberOfCoordinates - 1; i++)
            {
                result += coordinates[i].X * coordinates[i + 1].Y - coordinates[i + 1].X * coordinates[i].Y;
            }

            return (result + (coordinates[numberOfCoordinates - 1].X * coordinates[0].Y - coordinates[0].X * coordinates[numberOfCoordinates - 1].Y)) / 2;
        }
    }
}
