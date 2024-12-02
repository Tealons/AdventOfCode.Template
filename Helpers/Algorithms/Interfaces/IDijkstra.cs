using AoC_Helper_Methods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Helper_Methods.Algorithms.Interfaces
{
    public interface IDijkstra
    {
        List<Vertex> CalculateFastedPath(List<Vertex> vertices, Vertex goalVertex);
        int[] Calculate(int[,] graph, int startingPoint);
        void PrintSolution(int[] dist, int n);
    }
}
