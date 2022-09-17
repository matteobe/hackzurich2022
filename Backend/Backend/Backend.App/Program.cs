// See https://aka.ms/new-console-template for more information

using Backend.App.Graphs;
using Backend.App.InputReaders;
using Dijkstra.NET.ShortestPath;

var reader = new InputReaderService();
var data = reader.Read();

var graphBuilder = new GraphBuilder(data);
graphBuilder.Build();
var result = graphBuilder.GetShortestPath("a", "b");

Console.WriteLine(result == null ? "none" : string.Join("-", result));
