using System.Collections.Generic;
using static System.Math;

namespace Backend.Models
{
    public class Node
    {
        public string name { get; }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public NodeType NodeType { get; }

        public Dictionary<string,double> edges;
        public List<string> floorPlanes;

        public Node(double x, double y, double z, NodeType nodeType, string name)
        {
            this.name = name;
            X = x;
            Y = y;
            Z = z;
            NodeType = nodeType;

            edges = new Dictionary<string, double>();
            floorPlanes = new List<string>();
        }

        public void AddEdge(Node b) {
            edges.Add(b.name, CalculateDistance(this, b));
        }
        
        private double CalculateDistance(Node A, Node B) {
            return Pow((B.X - A.X), 2) + Pow((B.Y - A.Y), 2) + Pow((B.Z - A.Z), 2);
        }
    }

}

