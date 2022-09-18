"""
Author: @matteobe
"""

import numpy as np


rng = np.random.default_rng(seed=42)


def brownian_motion(points: int, sigma: float) -> np.ndarray:
    """
    Generate a brownian motion points sequence, normalized on 1 as a starting point

    Args:
        points (int): number of points to generate
        sigma (float): standard deviation of random process

    Returns:
        array (np.ndarray): normalized brownian motion
    """

    w = np.ones(shape=points)
    for i in range(1, points):
        y = rng.standard_normal() * sigma / np.sqrt(points)
        w[i] = w[i - 1] + y
    return w
