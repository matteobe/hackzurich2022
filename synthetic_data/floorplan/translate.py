"""
Author: @matteobe

Translate Unity Graph export into Nodes and Graph objects for diffusion simulation
"""

import json
from pathlib import Path


def translate_unity_export():
    """
    Translate a unity export to a graph builder JSON
    """

    file = Path(__file__).parent / "unity_export.json"
    with open(file, "r") as f:
        data = json.load(f)

    parsed_data = dict()

    nodes = list()
    connections = list()
    for node, properties in data['nodes'].items():
        # Parse nodes
        nodes.append({
            'name': properties['name'],
            'x': properties['X'],
            'y': properties['Z'],
            'z': properties['Y']
        })

        # Parse connections
        target_nodes = list(properties['edges'].keys())
        connections.extend([tuple({node, target_node}) for target_node in target_nodes])

    connections = list(set(connections))

    # Create the new JSON structure
    parsed_data['nodes'] = nodes
    parsed_data['connections'] = connections

    file = Path(__file__).parents[1] / "scenarios" / "graph.json"
    with open(file, "w") as f:
        json.dump(parsed_data, f, indent=4)


if __name__ == "__main__":
    translate_unity_export()
