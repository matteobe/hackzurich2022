using Backend.App.InputReaders.Models;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace Backend.App.Graphs;

public class GraphBuilder
{
    public GraphBuilder(InputData data)
    {
        _data = data;
        _nodeNaming = new Dictionary<string, uint>();
        _graph = new Graph<string, string>();
    }

    private readonly InputData _data;
    private readonly Dictionary<string, uint> _nodeNaming;
    private readonly Graph<string, string> _graph;

    public void Build()
    {
        foreach (var (nodeName, coordinates) in _data.Nodes)
        {
            var nodeId = _graph.AddNode(nodeName);
            _nodeNaming.Add(nodeName, nodeId);
        }

        foreach (var connection in _data.Connections)
        {
            var n0 = connection[0];
            var c0 = _data.Nodes[n0];
            var n1 = connection[1];
            var c1 = _data.Nodes[n1];

            var distance = (c0.X - c1.X) ^ 2 + (c0.Y - c1.Y) ^ 2 + (c0.Z - c1.Z) ^ 2;

            _graph.Connect(_nodeNaming[n0], _nodeNaming[n1], distance, null);
            _graph.Connect(_nodeNaming[n1], _nodeNaming[n0], distance, null);
        }
    }

    public List<string>? GetShortestPath(string from, string to)
    {
        var result = _graph.Dijkstra(_nodeNaming[from], _nodeNaming[to]);
        if (!result.IsFounded)
        {
            return null;
        }

        return result.GetPath().Select(x => _nodeNaming.First(n => n.Value == x).Key).ToList();
    }
}
