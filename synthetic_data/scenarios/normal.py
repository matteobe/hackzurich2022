"""
Author: @matteobe
"""

import json
import argparse
from pathlib import Path

import pandas as pd

import synthetic_data as sd
from synthetic_data.scenarios import escapepro_entry


def build_dataset(scenario: str):
    """
    Generate sensor data for normal scenario
    """

    scenario_file = Path(__file__).parent / f"{scenario}.json"
    with open(scenario_file, "r") as f:
        data = json.load(f)

    start_date = data["start_date"]
    duration = data["duration"]
    frequency = data["frequency"]
    nodes = data["nodes"]
    sensors = data["sensors"]

    timestamps, points = sd.generators.timeline(start_date=start_date, duration=duration, frequency=frequency)
    periods = int(sd.generators.signals.timedelta_in_secs(timedelta_str=duration) / (24 * 60 * 60))
    periodic_day = sd.generators.periodic(points, periods=periods)

    data = pd.DataFrame(index=timestamps)
    for node in nodes:
        for sensor_name, sensor_info in sensors.items():
            s_min, s_max = sensor_info["min"], sensor_info["max"]
            daily_fluctuations = s_min + periodic_day * (s_max - s_min)
            week_noise = sd.utils.mean_regression(sd.generators.brownian_motion(points, sigma=10))
            day_noise = sd.generators.gaussian_noise(points, sigma=0.5)

            data[f"{node}_{sensor_name}"] = daily_fluctuations + week_noise + day_noise

    data.to_csv(f"normal.csv", encoding="utf-8", float_format='%.2f')


def cli():
    parser = argparse.ArgumentParser()
    parser.add_argument('--name', type=str, default='normal', choices=['normal'],
                        help="Specify the scenario for which you would like to generate data.")
    args = parser.parse_args()
    build_dataset(scenario=args.name)


@escapepro_entry.command()
def normal():
    """Generate sensor data for normal operations scenarios"""
    build_dataset(scenario="normal")


if __name__ == "__main__":
    cli()
