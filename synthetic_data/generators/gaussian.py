"""
Author: @matteobe
"""

import numpy as np


rng = np.random.default_rng(seed=42)


def gaussian_noise(points: int, sigma: float) -> np.ndarray:
    """
    Generate Gaussian noise points sequence

    Args:
        points (int): number of points to generate
        sigma (float): standard deviation of random process

    Returns:
        array (np.ndarray): gaussian noise
    """
    return rng.random(size=points) * sigma
