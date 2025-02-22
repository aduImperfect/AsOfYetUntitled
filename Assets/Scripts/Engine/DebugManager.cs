using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugManager
{
    static bool drawDebugLines;
    static bool fogOfWarModeOn;

    public static void DrawLines(List<Vector3> points, Color linesColor)
    {
        if (!drawDebugLines)
        {
            return;
        }

        if (points.Count == 0)
        {
            return;
        }

        for (int i = 0; i < points.Count - 1; ++i)
        {
            Debug.DrawLine(points[i], points[i + 1], linesColor);
        }
    }

    public static void SetDebugDrawMode(bool value)
    {
        drawDebugLines = value;
    }

    public static void ToggleFogOfWarMode()
    {
        fogOfWarModeOn = !fogOfWarModeOn;
    }
}
