// See https://aka.ms/new-console-template for more information

using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

var graph = new Graph<string, string>();

graph.AddNode("F3_R1_1");
graph.AddNode("F3_R1_");

graph.Connect(1, 2, 5, "some custom information in edge"); //First node has key equal 1

ShortestPathResult result = graph.Dijkstra(1, 2); //result contains the shortest path

var path = result.GetPath();

Console.WriteLine(path);
