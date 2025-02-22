using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterObjectsCreator))]
public class CharacterObjectsCreatorScriptEditor : Editor
{
    static string currentSearch = "";
    bool isCharacterSubtypeSelected = false;
    string selectedCharacterSubtype = null;

    Vector2 CharacterSubtypeScrollPos = Vector2.zero;
    static string newCharacterObjectsType = "";
    static string newCharacterObjectsSubtype = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterObjectsCreator myTarget = (CharacterObjectsCreator)target;

        GUILayout.Label("ALL EQUIPMENTS ARE CONSIDERED TO BE PART OF THE INVENTORY!!");
        GUILayout.Label("ALL INVENTORY CANNOT BE CONSIDERED AS PART OF EQUIPMENTS!!");

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Created Characters"))
        {
            if (Application.isPlaying)
            {
                myTarget.ClearCreatedCharacterObjectsRuntime();
            }
            else
            {
                myTarget.ClearCreatedCharacterObjects();
            }
        }

        GUILayout.Label("ANY CHARACTER SAVE DOES A COPY SAVE IN _CHARACTERS DUMP FOLDER (DATE & TIME WARE)");
        if (GUILayout.Button("Save All Characters"))
        {
            myTarget.SaveAllCharacters();
        }

        if (GUILayout.Button("Load All Characters"))
        {
            myTarget.LoadAllCharacters();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Gather All Character Objects From Scene"))
        {
            myTarget.GatherAllCharacterObjectsFromScene();
        }

        if (GUILayout.Button("Reset All Character Objects In Scene Numbering"))
        {
            myTarget.ResetObjectsInSceneNumbering();
        }

        GUILayout.Space(20);

        ////Search for CharacterSubtypes

        GUILayout.Label("SEARCHBAR BELOW FOR CHOOSING AN CHARACTER TO ADD!!!");
        GUILayout.Label("TYPE ONE OF THE CATEGORIES TO CHOOSE FROM:");

        List<string> allCharacterObjectTypeNames = CharacterObjectTypesContainer.GetCharacterObjectTypes();

        string rowText = "";
        for (int i = 0; i < allCharacterObjectTypeNames.Count; ++i)
        {
            string type = allCharacterObjectTypeNames[i];

            if (i == (allCharacterObjectTypeNames.Count - 1))
            {
                rowText += type + ".";
                GUILayout.Label(rowText);
                rowText = "";
            }
            else
            {
                rowText += type + ", ";
            }

            if ((i % 4) == 3)
            {
                GUILayout.Label(rowText);
                rowText = "";
            }
        }

        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();

            currentSearch = EditorGUILayout.TextField(currentSearch);

            GUILayout.EndHorizontal();

            List<string> allChrSubTypeNames = CharacterObjectTypesContainer.GetAllCharacterObjectSubtypes();

            if (currentSearch.Length > 0)
            {
                CharacterSubtypeScrollPos = GUILayout.BeginScrollView(CharacterSubtypeScrollPos, GUILayout.Height(150));

                currentSearch = currentSearch.ToUpper();
                int currResultCount = 0;
                for (int i = 0; i < allChrSubTypeNames.Count; ++i)
                {
                    if (allChrSubTypeNames[i].StartsWith(currentSearch))
                    {
                        if (GUILayout.Button(allChrSubTypeNames[i]))
                        {
                            selectedCharacterSubtype = allChrSubTypeNames[i];
                            isCharacterSubtypeSelected = true;
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

            if (isCharacterSubtypeSelected == true)
            {
                if (selectedCharacterSubtype == null)
                {
                    return;
                }

                string characterObjSubtype = selectedCharacterSubtype;

                myTarget.currentCharacterObjSubtype = characterObjSubtype;

                Sprite previewSprite = myTarget.GetSpriteFromCharacterObjectSubtype(characterObjSubtype);

                if (previewSprite == null)
                {
                    return;
                }

                Texture2D texture = AssetPreview.GetAssetPreview(previewSprite);
                GUILayout.Label(texture);
            }

            GUILayout.Space(20);
        }

        if (GUILayout.Button("Add Character Object"))
        {
            myTarget.AddCharacterObject();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Gather All Character Object Types And Subtypes"))
        {
            CharacterObjectTypesContainer.GatherAllCharacterObjectTypesAndSubtypesFromPrefabs();
        }

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        newCharacterObjectsSubtype = EditorGUILayout.TextField(newCharacterObjectsSubtype);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add New Character Objects Subtype"))
        {
            CharacterObjectTypesContainer.AddCharacterObjectsSubtype(newCharacterObjectsSubtype.ToUpper());
        }

        if (GUILayout.Button("Remove Character Objects Subtype"))
        {
            CharacterObjectTypesContainer.RemoveCharacterObjectSubtype(newCharacterObjectsSubtype.ToUpper());
        }

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        newCharacterObjectsType = EditorGUILayout.TextField(newCharacterObjectsType);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Remove Character Objects Type"))
        {
            CharacterObjectTypesContainer.RemoveCharacterObjectType(newCharacterObjectsType.ToUpper());
        }
    }
}
