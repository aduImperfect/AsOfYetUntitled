using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonGeneratorScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DungeonGenerator myTarget = (DungeonGenerator)target;

        if (GUILayout.Button("Generate New Dungeon"))
        {
            myTarget.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Generated Dungeon"))
        {
            myTarget.ClearGeneratedDungeon();
        }

        if (GUILayout.Button("Save Generated Dungeon To File"))
        {
            myTarget.SaveGeneratedDungeonToFile();
        }

        if (GUILayout.Button("Load Dungeon From File"))
        {
            myTarget.LoadDungeonFromFile();
        }
    }
}
