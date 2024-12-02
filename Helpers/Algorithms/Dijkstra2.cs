using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Distance = int;

namespace AoC_Helper_Methods.Algorithms
{




    // Each Node has multiple edges
    public record Node(string Name)
    {
        public bool IsStartNode = false;

        public bool IsEndNode = false;
        public int x { get; set; }
        public int y { get; set; }

        public int value { get; set; }
        public List<Edge> Edges { get; set; } = [];
    }

    // An edge has a distance, and is connected to another Node
    public record Edge(Node ConnectedTo, Distance Distance);

    public static class Dijkstra2
    {
        public static void Letsparty(List<string> lines)
        {
            var graph = new List<Node>();
            var y = 1;

            //build nodes
            foreach (string line in lines)
            {
                var x = 1;
                foreach (var item in line)
                {

                    var value = (int)item % 32;
                    var node = new Node($"{x},{y}") { x = x, y = y, value = value };
                    if (item.ToString() == "S")
                    {
                        //node.IsStartNode = true;
                        node.value = 1;
                    }

                    if (item.ToString() == "E")
                    {
                        node.IsEndNode = true;
                        node.value = 26;
                    }

                    graph.Add(node);
                    x++;
                }
                y++;
            }


            foreach (var item in graph)
            {
                //x+1 y+1
                var otherX = item.x;
                var otherY = item.y + 1;
                var otherNode = graph.FirstOrDefault(x => x.x == otherX && x.y == otherY);
                if (otherNode != null)
                {
                    var distance = otherNode.value - item.value;
                    if (distance < 2)
                    {
                        item.Edges.Add(new Edge(otherNode, 1));
                    }
                }


                //x+1 y-1
                otherX = item.x + 1;
                otherY = item.y;
                otherNode = graph.FirstOrDefault(x => x.x == otherX && x.y == otherY);
                if (otherNode != null)
                {

                    var distance = otherNode.value - item.value;
                    if (distance < 2)
                    {
                        item.Edges.Add(new Edge(otherNode, 1));
                    }
                }

                //x-1 y+1
                otherX = item.x;
                otherY = item.y - 1;
                otherNode = graph.FirstOrDefault(x => x.x == otherX && x.y == otherY);
                if (otherNode != null)
                {

                    var distance = otherNode.value - item.value;
                    if (distance < 2)
                    {
                        item.Edges.Add(new Edge(otherNode, 1));
                    }
                }

                //x-1 y-1
                otherX = item.x - 1;
                otherY = item.y;
                otherNode = graph.FirstOrDefault(x => x.x == otherX && x.y == otherY);
                if (otherNode != null)
                {

                    var distance = otherNode.value - item.value;
                    if (distance < 2)
                    {
                        item.Edges.Add(new Edge(otherNode, 1));
                    }
                }
            }

            //potential starting points
            int? shortestPath = int.MaxValue;
            foreach (var item in graph.Where(x => x.value == 1).ToList())
            {
                item.IsStartNode = true;
                var temp = FuckingGo(graph.ToArray(), item, graph.FirstOrDefault(x => x.IsEndNode == true));
                if (temp < shortestPath)
                {
                    shortestPath = temp;
                }

            }
            Console.WriteLine($"Part 2: {shortestPath}");

           // FuckingGo(graph.ToArray(), graph.FirstOrDefault(x => x.IsStartNode == true), graph.FirstOrDefault(x => x.IsEndNode == true));

        }


        public static int? FuckingGo(Node[] graph, Node startNode, Node endNode)
        {

            var route = CalculateShortestPath(graph, startNode, endNode);
            if (route is null)
            {
                // This won't happen in our sample graph, but in general for a graph
                // you can't guarantee there's always going to be a path between two nodes
                Console.WriteLine($"No route could be found between {startNode.Name} and {endNode.Name}");
                return null;
            }

            Console.WriteLine($"Shortest route between {startNode.Name} and {endNode.Name}");
            foreach (var (node, distance) in route)
            {
                Console.WriteLine($"{node.Name}: {distance}");
            }
            Console.WriteLine($"Total steps: {route.Count - 1}");
            return route.Count - 1;

        }


        public static List<(Node, Distance)>? CalculateShortestPath(Node[] cities, Node startNode, Node endNode)
        {
            // Initialize all the distances to max, and the "previous" Node to null
            var distances = cities
                .Select((Node, i) => (Node, details: (Previous: (Node?)null, Distance: int.MaxValue)))
                .ToDictionary(x => x.Node, x => x.details);

            // priority queue for tracking shortest distance from the start node to each other node
            var queue = new PriorityQueue<Node, Distance>();

            // initialize the start node at a distance of 0
            distances[startNode] = (null, 0);

            // add the start node to the queue for processing
            queue.Enqueue(startNode, 0);

            // as long as we have a node to process, keep looping
            while (queue.Count > 0)
            {
                // remove the node with the current smallest distance from the start node
                var current = queue.Dequeue();

                // if this is the node we want, then we're finished
                // as we must already have the shortest route!
                if (current == endNode)
                {
                    // build the route by tracking back through previous
                    return BuildRoute(distances, endNode);
                }

                // add the node to the "visited" list
                var currentNodeDistance = distances[current].Distance;

                foreach (var edge in current.Edges)
                {
                    // get the current shortest distance to the connected node
                    Distance distance = distances[edge.ConnectedTo].Distance;
                    // calculate the new cumulative distance to the edge
                    Distance newDistance = currentNodeDistance + edge.Distance;

                    // if the new distance is shorter, then it represents a new 
                    // shortest-path to the connected edge
                    if (newDistance < distance)
                    {
                        // update the shortest distance to the connection
                        // and record the "current" node as the shortest
                        // route to get there 
                        distances[edge.ConnectedTo] = (current, newDistance);

                        // if the node is already in the queue, first remove it
                        queue.Remove(edge.ConnectedTo, out _, out _);
                        // now add the node with the new distance
                        queue.Enqueue(edge.ConnectedTo, newDistance);
                    }
                }
            }

            // if we don't have anything left, then we've processed everything,
            // but didn't find the node we want
            return null;
        }


        static List<(Node, Distance)> BuildRoute(
            Dictionary<Node, (Node? previous, Distance Distance)> distances,
            Node endNode)
        {
            var route = new List<(Node, Distance)>();
            Node? prev = endNode;

            // Keep examining the previous version until we
            // get back to the start node
            while (prev is not null)
            {
                var current = prev;
                (prev, var distance) = distances[current];
                route.Add((current, distance));
            }

            // reverse the route
            route.Reverse();
            return route;
        }
    }
}
