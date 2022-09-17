using System.Collections.Generic;

namespace Backend.Models {
    class Graph {
        List<Node> nodes;

        public Graph() {
            nodes = new List<Node>();
        }

        public List<Node> GetShortestPath(Node a, Node b) {

            PriorityQueues.PriorityQueue<Edge> pq = new PriorityQueues.PriorityQueue<Edge>();
            Dictionary<Node, double> visited = new Dictionary<Node, double>();
            Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

            visited.Add(a, 0);

            foreach(KeyValuePair<Node, double> neighbour in a.edges) {
                pq.Enqueue(new Edge(a, neighbour.Key, neighbour.Value));
                visited[neighbour.Key] = neighbour.Value;
            }
            
            while (pq.Count() != 0) {
                Edge edge = pq.Dequeue();

                foreach (KeyValuePair<Node, double> neighbour in edge.B.edges) {

                    double alt = visited[edge.B] + neighbour.Value;
                    if ((visited.ContainsKey(neighbour.Key) && visited[neighbour.Key] > alt)|| !visited.ContainsKey(neighbour.Key)) {
                        visited[neighbour.Key] = alt;
                        pq.Enqueue(new Edge(edge.B, neighbour.Key, visited[neighbour.Key]));
                        
                        if (prev.ContainsKey(neighbour.Key)) {
                            prev[neighbour.Key] = edge.B;
                        } else {
                            prev.Add(neighbour.Key, edge.B);
                        }
                    }
                }
            }

            if (!visited.ContainsKey(b)) throw new System.Exception("No path found");

            Node current = b;
            List<Node> path = new List<Node>();

            while (current != a) {
                path.Add(current);
                current = prev[current];
            }
            path.Add(a);
            path.Reverse();

            return path;
        }
    }
}
