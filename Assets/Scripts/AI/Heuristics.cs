using UnityEngine;

public static class Heuristics
{
    public static float CalculateHeuristic(Vector3 positionFrom, Vector3 positionTo, HeuristicType heuristic)
    {
        float xDiff = Mathf.Abs(positionTo.x - positionFrom.x);
        float yDiff = Mathf.Abs(positionTo.y - positionFrom.y);
        float zDiff = Mathf.Abs(positionTo.z - positionFrom.z);

        switch (heuristic)
        {
            case HeuristicType.CHEBYSHEV_DISTANCE:
                return CalculateHeuristic_Chebyshev(xDiff, yDiff, zDiff);
            case HeuristicType.MANHATTAN_DISTANCE:
                return CalculateHeuristic_Manhattan(xDiff, yDiff, zDiff);
            case HeuristicType.EUCLIDEAN_DISTANCE:
                return CalculateHeuristic_Euclidean(xDiff, yDiff, zDiff);
            case HeuristicType.OCTILE_DISTANCE:
                return CalculateHeuristic_Octile(xDiff, yDiff, zDiff);
            default:
                return CalculateHeuristic_Manhattan(xDiff, yDiff, zDiff);
        }
    }

    public static float CalculateActualCost(Vector3 positionFrom, Vector3 positionTo)
    {
        float xDiff = Mathf.Abs(positionTo.x - positionFrom.x);
        float yDiff = Mathf.Abs(positionTo.y - positionFrom.y);
        float zDiff = Mathf.Abs(positionTo.z - positionFrom.z);

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

