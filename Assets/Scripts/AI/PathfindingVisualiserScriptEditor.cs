using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathfindingVisualiser))]
public class PathfindingVisualiserScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathfindingVisualiser myTarget = (PathfindingVisualiser)target;

        if (GUILayout.Button("Calculate Path"))
        {
            myTarget.CalculatePath();
        }

        if (GUILayout.Button("Find Random Start And Goal Path And Calculate"))
        {
            myTarget.GetRandomStartAndGoalPositionsAndCalculate();
        }

        if (GUILayout.Button("Show Path"))
        {
            myTarget.canShowPath = true;
        }

        if (GUILayout.Button("Hide Path"))
        {
            myTarget.canShowPath = false;
        }

        if (GUILayout.Button("Clear Path"))
        {
            myTarget.ClearPath();
        }

        if (GUILayout.Button("Update Unit Size For Calculation"))
        {
            myTarget.SetUnitSize();
        }

        if (GUILayout.Button("Set Max Timeout Counter"))
        {
            myTarget.SetMaxTimeoutCounter();
        }
    }
}
