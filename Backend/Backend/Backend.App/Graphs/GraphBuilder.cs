using Backend.App.InputReaders.Models;
using Dijkstra.NET.Graph;

namespace Backend.App.Graphs;

public class GraphBuilder
{
    public Graph<string, string> Build(InputData data)
    {
        var graph = new Graph<string, string>();
        var nodeNaming = new Dictionary<string, uint>();

        foreach (var (nodeName, coordinates) in data.Nodes)
        {
            var nodeId = graph.AddNode(nodeName);
            nodeNaming.Add(nodeName, nodeId);
        }

        foreach (var connection in data.Connections)
        {
            var n0 = connection[0];
            var c0 = data.Nodes[n0];
            var n1 = connection[1];
            var c1 = data.Nodes[n1];

            var distance = (c0.X - c1.X) ^ 2 + (c0.Y - c1.Y) ^ 2 + (c0.Z - c1.Z) ^ 2;

            graph.Connect(nodeNaming[n0], nodeNaming[n1], distance, null);
            graph.Connect(nodeNaming[n1], nodeNaming[n0], distance, null);
        }

        return graph;
    }
}
