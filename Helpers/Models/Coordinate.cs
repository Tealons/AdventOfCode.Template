using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Helper_Methods.Models
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x; Y = y;
        }

        public Coordinate(string x, string y)
        {
            X = Convert.ToInt32(x); Y = Convert.ToInt32(y);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public long CalculateAbsoluteDistance(Coordinate otherCoordinate)
        {
            return Math.Abs(X - otherCoordinate.X) + Math.Abs(Y - otherCoordinate.Y);
        }

        public Coordinate GetNeirestNeighbour(IEnumerable<Coordinate> neighbors)
        {
            var nearestDistance = long.MaxValue;
            Coordinate result = null;

            foreach (var neighbor in neighbors)
            {
                var distance = CalculateAbsoluteDistance(neighbor);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    result = neighbor;
                }
            }

            return result ?? throw new Exception();
        }
    }
}
