// See https://aka.ms/new-console-template for more information

using Backend.App.Graphs;
using Backend.App.InputReaders;
using Dijkstra.NET.ShortestPath;

var reader = new InputReaderService();
var data = reader.Read();

var graphBuilder = new GraphBuilder();
var graph = graphBuilder.Build(data);

ShortestPathResult result = graph.Dijkstra(1, 2); //result contains the shortest path

var path = result.GetPath();

Console.WriteLine(path);
