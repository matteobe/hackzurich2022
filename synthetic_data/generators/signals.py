"""
Author: @matteobe
"""

from typing import Tuple, List
import datetime

import numpy as np
import pandas as pd

days_map = {
    'mon': 0,
    'tue': 1,
    'wed': 2,
    'thu': 3,
    'fri': 4,
    'sat': 5,
    'sun': 6
}


def periodic(points: int, periods: int) -> np.ndarray:
    """
    Generate a sequence of periods for seasonality / periodicity simulation.
    The series generated is from 0 to 1, so that it can be scaled with the transformation y = base + x * (peak - base)

    Args:
        points (int): number of datapoints to be generated (length of the series)
        periods (int): number of periods to simulate

    Returns:
        array (np.ndarray): array containing a normalized periodic curve
    """

    x = np.linspace(-np.pi / 2, (periods * 4 - 1) / 2 * np.pi, points)
    return np.sin(x) / 2 + 0.5


def timeline(start_date: str, days: int, frequency: str) -> Tuple[pd.DatetimeIndex, int]:
    """
    Generate a timeline based on the number of required days and the timestamp frequency

    Args:
        start_date (str): date in European format (YYYY-MM-DD)
        days (int): number of days to be covered in the timeline
        frequency (str): available are 'd', 'h', 'min', 'sec'

    Return:
        timestamps (pd.DatetimeIndex): array containing the time-stamps in the pandas format
        length (int): number of datapoints in the series
    """

    # Parse frequency
    if 'min' in frequency:
        multiplier = 24 * 60
    elif 'h' in frequency:
        multiplier = 24
    else:
        multiplier = 1

    # Parse numeric value
    num = int(''.join(c for c in frequency if c.isnumeric()))
    multiplier = multiplier / num

    # Calculate number of required synthetic_data points
    points = days * multiplier
    timestamps = pd.date_range(start=start_date, periods=points, freq=frequency)
    return timestamps, len(timestamps)


def presence(timestamps: pd.DatetimeIndex, start_time: str, end_time: str, days: List[str] = None) -> np.ndarray:
    """
    Boolean signal representing presence in a building

    Args:
        timestamps (pd.DatetimeIndex): timestamps to be used for
        start_time (str): time in the format hh:mm (24h format)
        end_time (str): time in the format hh:mm (24h format)
        days (list): list with week days to be considered (english), defaults to week-days

    Return:
        present (np.ndarray): array with true if presence allowed at that time
    """

    # Check the start and end times
    time_components = ['hour', 'minute']
    start_time = datetime.time(**{key: int(value) for key, value in zip(time_components, start_time.split(":"))})
    end_time = datetime.time(**{key: int(value) for key, value in zip(time_components, end_time.split(":"))})
    times = timestamps.time
    in_time_window = (times >= start_time) & (times <= end_time)

    # Check for days
    if days is None:
        days = ['mon', 'tue', 'wed', 'thu', 'fri']

    days = [days_map[day[0:3].lower()] for day in days]
    in_days = timestamps.weekday.isin(days)
    return in_days & in_time_window
