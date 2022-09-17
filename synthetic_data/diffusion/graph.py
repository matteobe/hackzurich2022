"""
Author: @matteobe
"""

from __future__ import annotations
from typing import List, Dict
from dataclasses import dataclass, field


@dataclass
class Node:
    name: str
    x: float
    y: float
    z: float
    source: bool = False
    initial_value: float = 0.
    measurements: List[float] = field(default_factory=list)
    value_delta: float = 0.

    def __post_init__(self):
        self.measurements.append(self.initial_value)

    def __hash__(self):
        return hash((self.x, self.y, self.z))

    def distance(self, node: Node):
        return ((self.x - node.x) ** 2 + (self.y - node.y) ** 2 + (self.z - node.z) ** 2) ** (1/2)

    def last_measurement(self):
        return self.measurements[-1]

    def step(self):
        """Perform one step in the simulation"""
        self.measurements.append(self.last_measurement() + self.value_delta)
        self.value_delta = 0


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
