"""
Author: @matteobe
"""

import numpy as np


def mean_regression(array: np.ndarray) -> np.ndarray:
    """
    Perform regression to the mean on an array, by taking the first and last points as start and end-point of the
    mean line.
    By definition, the values of the line will then be around 0.

    Args:
        array (np.ndarray): array with numerical value

    Returns:
        array (np.ndarray):
    """

    if len(array) <= 2:
        raise ValueError("Array must be of length >= 2.")

    slope = (array[-1] - array[0]) / (len(array) - 1)
    mean_line = array[0] + slope * np.arange(0, len(array), 1)

    # Remove mean line from array
    return array - mean_line
