"""
Author: @matteobe

Diffusion-based modelling of a sensor
"""

import json
import argparse
import click
from pathlib import Path

import pandas as pd

from synthetic_data.generators.signals import timeline, timedelta_in_secs
from synthetic_data.entities.graph import graph_from_config
from synthetic_data.diffusion.flow import calculate_graph_flows
from synthetic_data.utils.visualization import plot_graph_measurements, plot_graph_map
from synthetic_data.scenarios import escapepro_entry


def build_dataset(scenario: str):
    """
    Generate sensor data for normal scenario
    """

    parent_dir = Path(__file__).parent
    graph_definition = parent_dir / "graph.json"
    with open(graph_definition, "r") as f:
        graph_def = json.load(f)

    scenario_file = parent_dir / f"{scenario}.json"
    with open(scenario_file, "r") as f:
        scenario_info = json.load(f)

    # Merge graph and scenario information for diffusion calculation
    update_info = {node['name']: node for node in scenario_info['nodes']}
    nodes = graph_def['nodes']
    for idx, node in enumerate(nodes):
        if node['name'] in update_info:
            nodes[idx].update(update_info[node['name']])

    # Create the timeline
    timestamps, points = timeline(start_date=scenario_info['start_date'],
                                  duration=scenario_info['duration'],
                                  frequency=scenario_info['frequency'])
    print(f"Number of generated timestamps: {points}")

    # Run the simulation
    graph = graph_from_config(graph_def)
    graph = calculate_graph_flows(graph=graph,
                                  steps=points,
                                  gradient_norm=scenario_info['gradient_norm'])

    # Print trajectories and floor-plan
    fig1 = plot_graph_map(graph)
    fig2 = plot_graph_measurements(graph, timestamps)
    fig1.show()
    fig2.show()
    fig1.write_image("sensors_map.png")
    fig2.write_image("sensors_readings.png")

    # Export sensor data to CSV
    data = pd.DataFrame(index=timestamps)
    for node in graph.connections:
        data[f"{node.name}"] = node.measurements
    data.to_csv(f"emergency.csv", encoding="utf-8", float_format='%.2f')

    # Export sensor data to JSON
    data = dict()
    data['frequency'] = timedelta_in_secs(scenario_info['frequency'])
    data['timestamps'] = timestamps.astype(str).to_list()
    sensors = dict()
    for node in graph.connections:
        sensors[f"{node.name}"] = node.measurements
    data['sensors'] = sensors

    with open(f"emergency.json", "w") as f:
        json.dump(data, f, indent=4)


def cli():
    parser = argparse.ArgumentParser()
    parser.add_argument('--scenario', type=str,
                        choices=['fire', 'shooter'],
                        help="Specify the emergency scenario for which you would like to simulate.")
    args = parser.parse_args()
    build_dataset(scenario=args.scenario)


@escapepro_entry.command()
@click.option("--scenario", type=str, help="Choose from 'fire'")
def emergency(scenario: str):
    """Generate sensor data for emergency scenarios"""
    build_dataset(scenario=scenario)


if __name__ == "__main__":
    cli()
