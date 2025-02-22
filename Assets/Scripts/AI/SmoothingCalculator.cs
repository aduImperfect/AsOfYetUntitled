using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public static class SmoothingCalculator
{
    //This function creates a new way point between points 2 and 3 using Catmull-Rom Splines.
    public static Vector3 CreateNewWaypointBetweenTwoPointsUsingCatmullRom(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, uint s)
    {
        return point1 * ((-0.5f * s * s * s + s * s) - (0.5f * s)) + point2 * ((1.5f * s * s * s) + (-2.5f * s * s) + 1.0f) + point3 * ((-1.5f * s * s * s) + (2.0f * s * s) + (0.5f * s)) + point4 * ((0.5f * s * s * s) - (0.5f * s * s));
    }

    //This function creates a new way point between points 2 and 3 using Bezier Splines.
    public static Vector3 CreateNewWaypointBetweenTwoPointsUsingBezier(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, uint t)
    {
        return point1 * (1 - t) * (1 - t) * (1 - t) + point2 * (3 * t) * (1 - t) * (1 - t) + point3 * (3 * t * t) * (1 - t) + point4 * (t * t * t);
    }


}
