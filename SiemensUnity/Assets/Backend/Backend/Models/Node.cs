using System.Collections.Generic;
using static System.Math;

namespace Backend.Models
{
    public class Node
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public NodeType NodeType { get; }

        public List<KeyValuePair<Node,double>> edges;

        public Node(double x, double y, double z, NodeType nodeType)
        {
            X = x;
            Y = y;
            Z = z;
            NodeType = nodeType;

            edges = new List<KeyValuePair<Node, double>>();
        }

        public void AddEdge(Node b) {
            edges.Add(new KeyValuePair<Node, double>(b, CalculateDistance(this, b)));
        }
        
        private double CalculateDistance(Node A, Node B) {
            return Pow((B.X - A.X), 2) + Pow((B.Y - A.Y), 2) + Pow((B.Z - A.Z), 2);
        }
    }

}

