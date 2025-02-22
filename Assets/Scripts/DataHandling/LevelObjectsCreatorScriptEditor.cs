using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelObjectsCreator))]
public class LevelObjectsCreatorScriptEditor : Editor
{
    static string currentSearch = "";
    bool isSubtypeSelected = false;
    string selectedObjectSubtype = null;
    static string newLevelObjectsType = "";
    static string newLevelObjectsSubtype = "";

    static bool gatherOccurred = false;

    Vector2 objectSubTypeScrollPos = Vector2.zero;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (gatherOccurred == false)
        {
            LevelObjectTypesContainer.GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
            gatherOccurred = true;
        }

        LevelObjectsCreator myTarget = (LevelObjectsCreator)target;

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Created Level"))
        {
            if (Application.isPlaying)
            {
                myTarget.ClearCreatedLevelObjectsRuntime();
            }
            else
            {
                myTarget.ClearCreatedLevelObjects();
            }
        }

        GUILayout.Label("ANY LEVELS SAVED DOES A COPY SAVE IN _LEVELS DUMP FOLDER (DATE & TIME WARE)");
        if (GUILayout.Button("Save Levels To Files"))
        {
            myTarget.SaveLevelsToFiles();
        }

        if (GUILayout.Button("Load Multi Layered Levels From File Names"))
        {
            myTarget.LoadMultiLayeredLevelsFromFiles();
        }

        if (GUILayout.Button("Load Levels From Main Folder"))
        {
            myTarget.LoadLevelsFromMainFolder();
        }

        if (GUILayout.Button("Gather All Level Objects From Scene"))
        {
            myTarget.GatherAllLevelObjectsFromScene();
        }

        //if (GUILayout.Button("Reset All Level Objects In Scene Numbering"))
        //{
        //    myTarget.ResetObjectsInSceneNumbering();
        //}

        GUILayout.Space(10);

        //Search Field For Choosing an ObjectSubType!!!
        GUILayout.Label("SEARCHBAR BELOW FOR CHOOSING AN OBJECT TO ADD!!!");
        GUILayout.Label("TYPE ONE OF THE CATEGORIES TO CHOOSE FROM:");

        List<string> allLevelObjectTypeNames = LevelObjectTypesContainer.GetLevelObjectTypes();

        string rowText = "";
        for (int i = 0; i < allLevelObjectTypeNames.Count; ++i)
        {
            string type = allLevelObjectTypeNames[i];

            if (i == (allLevelObjectTypeNames.Count - 1))
            {
                rowText += type + ".";
                GUILayout.Label(rowText);
                rowText = "";
            }
            else
            {
                rowText += type + ", ";
            }

            if ((i % 7) == 6)
            {
                GUILayout.Label(rowText);
                rowText = "";
            }
        }

        GUILayout.Space(10);

        GUILayout.Label("Search Field for Level Objects");

        GUILayout.BeginHorizontal();
        currentSearch = EditorGUILayout.TextField(currentSearch);
        GUILayout.EndHorizontal();

        //string[] allSubTypeNames = Enum.GetNames(typeof(ObjectSubtype));

        List<string> allLevelObjectSubtypeNames = LevelObjectTypesContainer.GetAllLevelObjectSubtypes();

        if (currentSearch.Length > 0)
        {
            objectSubTypeScrollPos = GUILayout.BeginScrollView(objectSubTypeScrollPos, GUILayout.Height(150));

            currentSearch = currentSearch.ToUpper();
            int currResultCount = 0;
            //for (int i = 0; i < allSubTypeNames.Count - (int)(ObjectSubtype.CHARACTEROBJSUBTYPE_COUNT - ObjectSubtype.LEVELOBJSUBTYPE_COUNT + 2); ++i)

            for (int i = 0; i < allLevelObjectSubtypeNames.Count; ++i)
            {
                if (allLevelObjectSubtypeNames[i].StartsWith(currentSearch))
                {
                    if (GUILayout.Button(allLevelObjectSubtypeNames[i]))
                    {
                        selectedObjectSubtype = allLevelObjectSubtypeNames[i];
                        isSubtypeSelected = true;
                        break;
                    }
                    ++currResultCount;
                }
            }

            if (currResultCount == 0)
            {
                GUILayout.Label("NO RESULT FOUND");
            }
            GUILayout.EndScrollView();
        }

        if (GUILayout.Button("Add Level Object"))
        {
            myTarget.AddLevelObject();
        }

        if (isSubtypeSelected == true)
        {
            if (selectedObjectSubtype != null)
            {
                string objSubtype = selectedObjectSubtype;
                myTarget.currLevelObjSubType = objSubtype;

                Sprite previewSprite = myTarget.GetSpriteFromLevelObjectSubType(objSubtype);

                if (previewSprite != null)
                {
                    Texture2D texture = AssetPreview.GetAssetPreview(previewSprite);
                    GUILayout.Label(texture);
                }
            }
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Gather All Level Object Types And Subtypes"))
        {
            LevelObjectTypesContainer.GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        }

        GUILayout.Space(20);
        
        GUILayout.Label("Text Field ADD/REMOVE Level Objects Subtypes");
        
        GUILayout.BeginHorizontal();
        newLevelObjectsSubtype = EditorGUILayout.TextField(newLevelObjectsSubtype);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add New Level Objects Subtype"))
        {
            LevelObjectTypesContainer.AddCharacterObjectsSubtype(newLevelObjectsSubtype.ToUpper());
        }

        if (GUILayout.Button("Remove Level Objects Subtype"))
        {
            LevelObjectTypesContainer.RemoveLevelObjectSubtype(newLevelObjectsSubtype.ToUpper());
        }

        GUILayout.Space(10);

        GUILayout.Label("Text Field To REMOVE Level Objects Types");

        GUILayout.BeginHorizontal();
        newLevelObjectsType = EditorGUILayout.TextField(newLevelObjectsType);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Remove Level Objects Type"))
        {
            LevelObjectTypesContainer.RemoveLevelObjectType(newLevelObjectsType.ToUpper());
        }
    }
}
