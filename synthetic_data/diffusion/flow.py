"""
Author: @matteobe

Script takes care of calculating the "diffusion" of values across a network.
"""

from synthetic_data.entities import Node, Graph


def calculate_node_flows(graph: Graph, source_node: Node, gradient_norm: float):
    """Calculate the flows"""
    gradients = list()

    # Calculate the gradients
    for idx, target in enumerate(graph.connections[source_node]):
        weight = graph.weights[source_node][idx]
        gradient = (source_node.last_measurement() - target.last_measurement()) / weight

        if gradient < 0:
            gradient = 0

        gradients.append(gradient)

    # Calculate the flow
    total_gradient = sum(gradients)

    if total_gradient == 0:
        flows = [0 for _ in gradients]
    else:
        flows = [gradient / gradient_norm for gradient in gradients]

    for idx, target in enumerate(graph.connections[source_node]):
        target.value_delta += flows[idx]


def calculate_graph_flows(graph: Graph, steps: int = 200, gradient_norm: float = 100) -> Graph:
    # Sources:
    source_nodes = [node for node in graph.connections if node.source]
    other_nodes = [node for node in graph.connections if not node.source]

    for _ in range(steps):
        # Emit from sources
        for source_node in source_nodes:
            calculate_node_flows(graph=graph, source_node=source_node, gradient_norm=gradient_norm)

        # Emit from other nodes
        for node in other_nodes:
            calculate_node_flows(graph=graph, source_node=node, gradient_norm=gradient_norm)

        # Do one step
        graph.step()

    return graph
