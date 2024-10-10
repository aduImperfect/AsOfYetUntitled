using System.Runtime.CompilerServices;
using UnityEngine;

public enum HeuristicsEnum
{
    CHEBYSHEV_DISTANCE,
    MANHATTAN_DISTANCE,
    EUCLIDEAN_DISTANCE,
    OCTILE_DISTANCE
}

public static class Heuristics
{
    public static float CalculateHeuristic(Vector3 positionFrom, Vector3 positionTo, HeuristicsEnum heuristic)
    {
        float xDiff = positionTo.x - positionFrom.x;
        float yDiff = positionTo.y - positionFrom.y;
        float zDiff = positionTo.z - positionFrom.z;

        switch (heuristic)
        {
            case HeuristicsEnum.CHEBYSHEV_DISTANCE:
                return CalculateHeuristic_Chebyshev(xDiff, yDiff, zDiff);
            case HeuristicsEnum.MANHATTAN_DISTANCE:
                return CalculateHeuristic_Manhattan(xDiff, yDiff, zDiff);
            case HeuristicsEnum.EUCLIDEAN_DISTANCE:
                return CalculateHeuristic_Euclidean(xDiff, yDiff, zDiff);
            case HeuristicsEnum.OCTILE_DISTANCE:
                return CalculateHeuristic_Octile(xDiff, yDiff, zDiff);
            default:
                return CalculateHeuristic_Manhattan(xDiff, yDiff, zDiff);
        }
    }

    public static float CalculateActualCost(Vector3 positionFrom, Vector3 positionTo)
    {
        float xDiff = positionTo.x - positionFrom.x;
        float yDiff = positionTo.y - positionFrom.y;
        float zDiff = positionTo.z - positionFrom.z;

        return CalculateHeuristic_Euclidean(xDiff, yDiff, zDiff);
    }

    private static float CalculateHeuristic_Chebyshev(float xDiff, float yDiff, float zDiff)
    {
        return Mathf.Max(Mathf.Max(xDiff, yDiff), zDiff);
    }

    private static float CalculateHeuristic_Manhattan(float xDiff, float yDiff, float zDiff)
    {
        return xDiff + yDiff + zDiff;
    }

    private static float CalculateHeuristic_Euclidean(float xDiff, float yDiff, float zDiff)
    {
        return Mathf.Sqrt((xDiff * xDiff) + (yDiff * yDiff) + (zDiff * zDiff));
    }

    private static float CalculateHeuristic_Octile(float xDiff, float yDiff, float zDiff)
    {
        return (Mathf.Min(Mathf.Min(xDiff, yDiff), zDiff) * Mathf.Sqrt(2)) + Mathf.Max(Mathf.Max(xDiff, yDiff), zDiff) - Mathf.Min(Mathf.Min(xDiff, yDiff), zDiff);
    }
}

