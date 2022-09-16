namespace Backend.Models;

public class Node
{
    public Node(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double X { get; init; }
    public double Y { get; init; }
    public double Z { get; init; }
}
