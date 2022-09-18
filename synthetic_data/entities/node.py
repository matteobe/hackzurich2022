"""
Author: @matteobe
"""

from __future__ import annotations
from typing import List
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
