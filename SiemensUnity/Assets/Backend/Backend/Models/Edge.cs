using static System.Math;


// not used
namespace Backend.Models {
    public class Edge : System.IComparable<Edge> {
        public Node A { get; }
        public Node B { get; }
        public double distance { get; set; }

        public Edge(Node a, Node b) {
            this.A = a;
            this.B = b;
            distance = CalculateDistance();
        }

        public Edge(Node a, Node b, double distance) {
            this.A = a;
            this.B = b;
            this.distance = distance;
        }

        private double CalculateDistance() {
            return Pow((B.X - A.X),2)  + Pow((B.Y - A.Y),2) + Pow((B.Z - A.Z),2);
        }

        public int CompareTo(Edge other) {
            if (this.distance < other.distance) return -1;
            else if (this.distance > other.distance) return 1;
            else return 0;
        }
    }
}
