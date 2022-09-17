"""
Author: @matteobe
"""

import json
import argparse
from pathlib import Path

import pandas as pd

import synthetic_data as sd


def build_dataset(scenario: str):
    """
    Generate sensor data for normal scenario
    """

    scenario_file = Path(__file__).parent / f"{scenario}.json"
    with open(scenario_file, "r") as f:
        data = json.load(f)

    start_date = data["start_date"]
    duration = data["days"]
    frequency = data["frequency"]
    nodes = data["nodes"]
    sensors = data["sensors"]

    timeline, points = sd.generators.timeline(start_date=start_date, days=duration, frequency=frequency)
    periodic_day = sd.generators.periodic(points, periods=duration)

    data = pd.DataFrame(index=timeline)
    for node in nodes:
        for sensor_name, sensor_info in sensors.items():
            s_min, s_max = sensor_info["min"], sensor_info["max"]
            daily_fluctuations = s_min + periodic_day * (s_max - s_min)
            week_noise = sd.utils.mean_regression(sd.generators.brownian_motion(points, sigma=10))
            day_noise = sd.generators.gaussian_noise(points, sigma=0.5)

            data[f"{node}_{sensor_name}"] = daily_fluctuations + week_noise + day_noise

    data.to_csv(f"{scenario}.csv", encoding="utf-8")


def cli():
    parser = argparse.ArgumentParser()
    parser.add_argument('--name', type=str,
                        choices=['normal', 'fire', 'shooter'],
                        help="Specify the scenario for which you would like to generate data.")
    args = parser.parse_args()
    build_dataset(scenario=args.name)


if __name__ == "__main__":
    cli()
