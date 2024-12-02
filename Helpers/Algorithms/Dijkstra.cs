using AoC_Helper_Methods.Algorithms.Interfaces;
using AoC_Helper_Methods.Models;

namespace AoC_Helper_Methods.Algorithms
{
    public class Dijkstra : IDijkstra
    {
        ///<summary>
        ///<para>Dijkstra based on vertices instead of the int matrix</para>
        ///</summary>
        ///<returns>
        ///<para>List<Vertex> of all the vertices from start vertex to goal vertex</para>
        ///<para>Vertex ShortestPathFromStart is cumulative for each</para>
        //////<para>Returns 'null' if no path can be found</para>
        ///</returns>
        public List<Vertex> CalculateFastedPath(List<Vertex> vertices, Vertex goalVertex)
        {
            var visitedVertices = new List<Vertex>();

            // If there are no more reacheable vertices anymore and the goal hasn't been found yet, then there's no path to the goal at all
            while (vertices.Any(x => x.ShortestPathFromStart < long.MaxValue)) 
            {
                /* 
                    The Min compares based on the lowest 'ShortestPathFromStart' via an extension method on the Vertex class
                    Every iteration in the while loop must contain only 1 closestVertex
                    The next closest vertex might change due to the calculations based on this one, so have to start from here each time
                */
                var closestVertex = vertices.Min();

                foreach (var neighbor in closestVertex.NeighborDistances)
                {
                    // If it's already visited, this is by definition not the fastest path to that vertex. So it can be skipped
                    if (visitedVertices.Any(x => x.Name == neighbor.Key))
                    {
                        continue;
                    }

                    var neighborVertex = vertices.First(x => x.Name == neighbor.Key);

                    // Goal is reached, so answer can be returned
                    if (goalVertex == neighborVertex)
                    {
                        var result = new List<Vertex> { neighborVertex };

                        var previousVertexInPath = neighborVertex.PreviousVertexForShortestPath;

                        // The starting vertex should not have a previous vertex, so can back propagate to find the most optimal route and add all the steps to the list
                        while (true)
                        {
                            result.Prepend(previousVertexInPath);

                            if (previousVertexInPath.PreviousVertexForShortestPath == null)
                            {
                                return result;
                            }

                            previousVertexInPath = previousVertexInPath.PreviousVertexForShortestPath;
                        }
                    }

                    // Update the neighbor's shortest path only if this route is faster than what was previously calculated
                    if (neighbor.Value < neighborVertex.ShortestPathFromStart)
                    {
                        neighborVertex.ShortestPathFromStart = neighbor.Value;
                        neighborVertex.PreviousVertexForShortestPath = closestVertex;
                    }

                    // Move the processed vertex out of the list that's used for processing and into the visited list
                    visitedVertices.Add(neighborVertex);
                    vertices.Remove(neighborVertex);
                }
            }

            return null;
        }

        // Function that implements Dijkstra's single source shortest path algorithm
        // for a graph represented using adjacency matrix representation
        public int[] Calculate(int[,] graph, int startingPoint)
        {
            var numberOfVertices = graph.GetLength(0);
            // The output array. dist[i] will hold the shortest distance from src to i
            var distance = new int[numberOfVertices]; 

            // sptSet[i] will true if vertex i is included in shortest path
            // tree or shortest distance from src to i is finalized
            bool[] shortestPathDeterminations = new bool[numberOfVertices];

            // Initialize all distances as INFINITE and stpSet[] as false
            for (int i = 0; i < numberOfVertices; i++)
            {
                distance[i] = int.MaxValue;
                shortestPathDeterminations[i] = false;
            }

            // Distance of source vertex
            // from itself is always 0
            distance[startingPoint] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < numberOfVertices - 1; count++)
            {
                // Pick the minimum distance vertex from the set of vertices not yet processed. u is always equal to
                // src in first iteration.
                int u = CalculateMinDistance(distance, shortestPathDeterminations, numberOfVertices);

                // Mark the picked vertex as processed
                shortestPathDeterminations[u] = true;

                // Update dist value of the adjacent
                // vertices of the picked vertex.
                for (int v = 0; v < numberOfVertices; v++)

                    // Update dist[v] only if is not in sptSet, there is an edge from u to v, and total weight of path
                    // from src to v through u is smaller than current value of dist[v]
                    if (!shortestPathDeterminations[v] && graph[u, v] != 0 &&
                         distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                        distance[v] = distance[u] + graph[u, v];
            }

            return distance;
        }

        // A utility function to find the vertex with minimum distance value, from the set of vertices
        // not yet included in shortest path tree
        private static int CalculateMinDistance(int[] pathDistances,
                        bool[] shortestPathDeterminations, int numberOfVertices)
        {
            // Initialize min value
            var minimumDistance = int.MaxValue;
            var indexOfShortestPathDistance = -1;

            for (int v = 0; v < numberOfVertices; v++)
                if (shortestPathDeterminations[v] == false && pathDistances[v] <= minimumDistance)
                {
                    minimumDistance = pathDistances[v];
                    indexOfShortestPathDistance = v;
                }

            return indexOfShortestPathDistance;
        }

        // A utility function to print the constructed distance array
        public void PrintSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance "
                          + "from Source\n");
            for (int i = 0; i < dist.Length; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }
    }
}
