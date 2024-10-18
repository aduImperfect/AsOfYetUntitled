using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerMovement myTarget = (PlayerMovement)target;

        if (GUILayout.Button("Get Path"))
        {
            myTarget.GetPath();
        }

        if (GUILayout.Button("Move Player (Step By Step)"))
        {
            myTarget.MoveStepByStep();
        }
    }
}
