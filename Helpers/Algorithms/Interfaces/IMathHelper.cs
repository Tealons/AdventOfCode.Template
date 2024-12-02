using AoC_Helper_Methods.Models;

namespace AoC_Helper_Methods.Algorithms.Interfaces
{
    public interface IMathHelper
    {
        long GreatestCommonDivisor(long a, long b);
        int GreatestCommonDivisor(int a, int b);
        long GreatestCommonDivisor(IEnumerable<long> numbers);
        int GreatestCommonDivisor(IEnumerable<int> numbers);
        long LeastCommonMultiple(long a, long b);
        int LeastCommonMultiple(int a, int b);
        long LeastCommonMultiple(IEnumerable<long> numbers);
        int LeastCommonMultiple(IEnumerable<int> numbers);
        int CalculateSurfaceAreaOfCoordinateGrid(Coordinate[] coordinates);
    }
}