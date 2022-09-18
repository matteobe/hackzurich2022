"""
Author: @matteobe
"""

from __future__ import annotations
from typing import List, Dict

from synthetic_data.entities import Node


class Graph:
    connections: Dict[Node, List[Node]]
    weights: Dict[Node, List[float]]

    def __init__(self, bidirectional: bool = True):
        self.bidirectional = bidirectional
        self.connections = dict()
        self.weights = dict()

    def add_connection(self, start_node: Node, end_node: Node):
        """Add a connection to the graph"""

        for node in (start_node, end_node):
            if node not in self.connections:
                self.connections[node] = list()
            if node not in self.weights:
                self.weights[node] = list()

        distance = start_node.distance(node=end_node)
        connections = [(start_node, end_node)]
        if self.bidirectional:
            connections.append((end_node, start_node))

        for a, b in connections:
            self.connections[a].append(b)
            self.weights[a].append(distance)

    def step(self):
        """Perform one step in the simulation"""
        for node in self.connections:
            node.step()


def graph_from_config(graph_def: Dict) -> Graph:
    """Instantiate a graph object from a graph definition in JSON format"""
    nodes = dict()

    # Generate the nodes
    for node in graph_def['nodes']:
        nodes[node['name']] = Node(**node)

    # Create the connections
    g = Graph()
    for start_node, end_node in graph_def['connections']:
        g.add_connection(nodes[start_node], nodes[end_node])

    return g
