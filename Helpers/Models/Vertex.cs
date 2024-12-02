using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Helper_Methods.Models
{
    public class Vertex : IComparable<Vertex>
    {
        public string Name { get; set; }
        public Dictionary<string, int> NeighborDistances { get; set; } = [];
        public long ShortestPathFromStart { get; set; } = long.MaxValue;
        public Vertex PreviousVertexForShortestPath { get; set; }

        public Vertex(string name, Dictionary<string, int> neighborDistances) 
        {
            Name = name;
            NeighborDistances = neighborDistances;
        }

        public int CompareTo(Vertex otherVertex)
        {
            if (ShortestPathFromStart == otherVertex.ShortestPathFromStart)
            {
                return 0;
            }

            return ShortestPathFromStart < otherVertex.ShortestPathFromStart ? -1 : 1;
        }
    }
}
