"""
Author: @matteobe
"""

import numpy as np
import plotly.express as px
import plotly.graph_objects as go

from synthetic_data.entities import Graph


def plot_graph_measurements(graph: Graph):
    """
    Plot the measurements timeseries for all nodes in a graph
    """
    nodes = list(graph.connections.keys())

    x = np.arange(len(nodes[0].measurements))
    fig = go.Figure()
    for node in nodes:
        fig.add_trace(go.Scatter(x=x, y=node.measurements, mode='lines', name=node.name))
    fig.show()


def plot_graph_map(graph: Graph):
    """
    Plot the graph with its nodes
    """
    nodes = list(graph.connections.keys())

    x = [node.x for node in nodes]
    y = [node.y for node in nodes]
    name = [node.name for node in nodes]

    # Print nodes
    fig = px.scatter(x=x, y=y, text=name)
    fig.update_traces(textposition='top center')

    # Print edges
    already_printed = list()
    for source_node in graph.connections:
        for target_node in graph.connections[source_node]:
            if (target_node, source_node) in already_printed:
                continue
            else:
                x = [target_node.x, source_node.x]
                y = [target_node.y, source_node.y]
                fig.add_trace(go.Scatter(x=x, y=y, showlegend=False, line=dict(color='grey', width=1, dash='dash')))

    fig.show()
