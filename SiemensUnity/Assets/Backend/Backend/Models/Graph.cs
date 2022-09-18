using System.Collections.Generic;
using UnityEngine;

namespace Backend.Models {
    public class Graph {
        public Dictionary<string, Node> nodes;

        public Graph() {
            nodes = new Dictionary<string, Node>();
        }

        public List<Node> GetShortestPath(Node a, Node b) {

            PriorityQueues.PriorityQueue<Edge> pq = new PriorityQueues.PriorityQueue<Edge>();
            Dictionary<Node, double> visited = new Dictionary<Node, double>();
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

            visited.Add(a, 0);
            prev.Add(a, a);

            foreach(KeyValuePair<string, double> neighbour in a.edges) {
                Node neighbourNode = nodes[neighbour.Key];
                pq.Enqueue(new Edge(a, neighbourNode, neighbour.Value));
                visited[neighbourNode] = neighbour.Value;
                prev[neighbourNode] = a;
            }
            
            while (pq.Count() != 0) {
                Edge edge = pq.Dequeue();

                foreach (KeyValuePair<string, double> neighbour in edge.B.edges) {
                    Node neighbourNode = nodes[neighbour.Key];
                    double alt = visited[edge.B] + neighbour.Value;
                    if ((visited.ContainsKey(neighbourNode) && visited[neighbourNode] > alt)|| !visited.ContainsKey(neighbourNode)) {
                        visited[neighbourNode] = alt;
                        pq.Enqueue(new Edge(edge.B, neighbourNode, visited[neighbourNode]));
                        
                        if (prev.ContainsKey(neighbourNode)) {
                            prev[neighbourNode] = edge.B;
                        } else {
                            prev.Add(neighbourNode, edge.B);
                        }
                    }
                }
            }

            if (!visited.ContainsKey(b)) throw new System.Exception("No path found");

            Node current = b;
            List<Node> path = new List<Node>();

            while (current != a) {
                Debug.Log(current.name);
                path.Add(current);
                current = prev[current];
            }
            path.Add(a);
            path.Reverse();

            return path;
        }
    }
}
