using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class CharacterObjectTypesContainer
{
    private static List<string> characterObjectTypes = new List<string>();
    private static Dictionary<string, List<string>> characterObjectSubtypes = new Dictionary<string, List<string>>();

    private static List<string> allCharacterObjectSubtypes = new List<string>();

    public static void ResetCharacterObjectTypes()
    {
        characterObjectTypes.Clear();
    }

    public static void ResetCharacterObjectSubtypes()
    {
        characterObjectSubtypes.Clear();
        allCharacterObjectSubtypes.Clear();
    }

    public static int GetCharacterObjectTypesCount()
    {
        return characterObjectTypes.Count;
    }

    public static int GetCharacterObjectSubtypesCountUnderACharacterObjectType(string objType)
    {
        return characterObjectSubtypes[objType].Count;
    }

    public static int GetTotalCharacterObjectSubtypesCount()
    {
        return allCharacterObjectSubtypes.Count;
    }

    public static List<string> GetCharacterObjectTypes()
    {
        return characterObjectTypes;
    }

    public static List<string> GetCharacterObjectSubtypesUnderCharacterObjectType(string levelObjectType)
    {
        return characterObjectSubtypes[levelObjectType];
    }

    public static List<string> GetAllCharacterObjectSubtypes()
    {
        return allCharacterObjectSubtypes;
    }

    public static void AddCharacterObjectsType(string objType)
    {
        if (characterObjectTypes.Contains(objType))
        {
            Debug.Log(objType + " already exists!!");
            return;
        }

        characterObjectTypes.Add(objType);
        characterObjectSubtypes.Add(objType, new List<string>());
        CreateNewCharacterObjectTypeFolder(objType);

        GatherAllCharacterObjectTypesAndSubtypesFromPrefabs();
    }

    public static void AddCharacterObjectsSubtype(string objSubtype)
    {
        string[] objSubTypeStrs = objSubtype.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

        bool isCategoryKeyThere = characterObjectSubtypes.ContainsKey(objSubTypeStrs[0]);

        if (isCategoryKeyThere)
        {
            List<string> objSubTypes = characterObjectSubtypes[objSubTypeStrs[0]];

            if ((objSubTypes != null) && objSubTypes.Contains(objSubtype))
            {
                Debug.Log(objSubtype + " already exists!!");
                return;
            }

            characterObjectSubtypes[objSubTypeStrs[0]].Add(objSubtype);
            Debug.Log("New LevelObjectSubtype: " + objSubtype + " added under the LevelObjectType: " + objSubTypeStrs[0] + ".");
            allCharacterObjectSubtypes.Add(objSubtype);

            CreateNewCharacterObjectSubtypePrefab(objSubTypeStrs[0], objSubtype);
            return;
        }

        characterObjectTypes.Add(objSubTypeStrs[0]);
        characterObjectSubtypes.Add(objSubTypeStrs[0], new List<string>());

        characterObjectSubtypes[objSubTypeStrs[0]].Add(objSubtype);
        Debug.Log("New LevelObjectSubtype: " + objSubtype + " added under the LevelObjectType: " + objSubTypeStrs[0] + ".");
        allCharacterObjectSubtypes.Add(objSubtype);

        CreateNewCharacterObjectSubtypePrefab(objSubTypeStrs[0], objSubtype);

        GatherAllCharacterObjectTypesAndSubtypesFromPrefabs();
    }

    public static void AddInvalidCharacterObjectTypeAndSubtype()
    {
        characterObjectSubtypes["INVALID"].Add("INVALID");
    }

    public static void RemoveCharacterObjectType(string objType)
    {
        characterObjectTypes.Remove(objType);
        DeleteCharacterObjectTypeFolder(objType);
    }

    public static void RemoveCharacterObjectSubtype(string objSubtype)
    {
        string[] objSubTypeStrs = objSubtype.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

        bool isCategoryKeyThere = characterObjectSubtypes.ContainsKey(objSubTypeStrs[0]);

        if (!isCategoryKeyThere)
        {
            Debug.Log("No Level Object Type with name: " + objSubTypeStrs[0] + " exists for " + objSubtype + "!!!");
        }

        characterObjectSubtypes[objSubTypeStrs[0]].Remove(objSubtype);

        allCharacterObjectSubtypes.Remove(objSubtype);

        //If there are no more subtype objects under the specific object type then remove the object type completely!!
        if (characterObjectSubtypes[objSubTypeStrs[0]].Count == 0)
        {
            RemoveCharacterObjectType(objSubTypeStrs[0]);
        }
        else
        {
            DeleteCharacterObjectSubtype(objSubTypeStrs[0], objSubtype);
        }
    }

    public static string GetCharacterObjectTypeVal(int objTypeNum)
    {
        return characterObjectTypes[objTypeNum];
    }

    private static void CreateNewCharacterObjectTypeFolder(string objType)
    {
        Directory.CreateDirectory(Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType));
        AssetDatabase.Refresh();
    }

    public static void GatherAllCharacterObjectTypesAndSubtypesFromPrefabs()
    {
        string dataHandlingObjectSubTypePrefabDir = FilePaths.GetFullPrefabCharacterObjectTypesPath();
        List<GameObject> allPrefabs = FindPrefabs.FindPrefabsInPath(dataHandlingObjectSubTypePrefabDir);

        ResetCharacterObjectTypes();
        ResetCharacterObjectSubtypes();

        foreach (GameObject prefab in allPrefabs)
        {
            string[] prefabType = prefab.name.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            bool isCategoryKeyThere = characterObjectSubtypes.ContainsKey(prefabType[0]);

            if (isCategoryKeyThere)
            {
                List<string> objSubTypes = characterObjectSubtypes[prefabType[0]];

                if ((objSubTypes != null) && objSubTypes.Contains(prefab.name))
                {
                    //Debug.Log(prefab.name + " already exists!!");
                    continue;
                }

                characterObjectSubtypes[prefabType[0]].Add(prefab.name);
                //Debug.Log("New LevelObjectSubtype: " + prefab.name + " added under the LevelObjectType: " + prefabType[0] + ".");

                allCharacterObjectSubtypes.Add(prefab.name);
                continue;
            }

            characterObjectTypes.Add(prefabType[0]);
            characterObjectSubtypes.Add(prefabType[0], new List<string>());

            characterObjectSubtypes[prefabType[0]].Add(prefab.name);
            //Debug.Log("New LevelObjectSubtype: " + prefab.name + " added under the LevelObjectType: " + prefabType[0] + ".");

            allCharacterObjectSubtypes.Add(prefab.name);
        }
    }

    private static void DeleteCharacterObjectSubtype(string objType, string objSubtype)
    {
        File.Delete(Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType, objSubtype + FilePaths.GetPrefabExtension()));
        File.Delete(Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType, objSubtype + FilePaths.GetMetaExtension()));

        AssetDatabase.Refresh();

        GatherAllCharacterObjectTypesAndSubtypesFromPrefabs();
    }

    private static void DeleteCharacterObjectTypeFolder(string objType)
    {
        Directory.Delete(Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType), true);
        File.Delete(Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType + FilePaths.GetMetaExtension()));

        string objTypeTag = objType.Substring(0, 1) + objType.Substring(1).ToLower();
        InternalEditorUtility.RemoveTag(objTypeTag);

        AssetDatabase.Refresh();

        GatherAllCharacterObjectTypesAndSubtypesFromPrefabs();
    }

    private static void CreateNewCharacterObjectSubtypePrefab(string objType, string objSubtype)
    {
        CreateNewCharacterObjectTypeFolder(objType);

        GameObject gObject = new GameObject();
        gObject.name = objSubtype;

        string objTypeTag = objType.Substring(0, 1) + objType.Substring(1).ToLower();
        InternalEditorUtility.AddTag(objTypeTag);
        gObject.tag = objTypeTag;

        bool prefabSuccess = false;
        PrefabUtility.SaveAsPrefabAsset(gObject, Path.Combine(FilePaths.GetFullPrefabCharacterObjectTypesPath(), objType, objSubtype + FilePaths.GetPrefabExtension()), out prefabSuccess);

        if (!prefabSuccess)
        {
            Debug.Log("Failed To Create Prefab: " + objSubtype + FilePaths.GetPrefabExtension() + " under the " + objType + " folder!!");
            return;
        }

        if (Application.isPlaying)
        {
            UnityEngine.Object.Destroy(gObject);
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(gObject);
        }

        AssetDatabase.Refresh();
    }

    public static void UpdatingTagsForCharacterObjectsPrefabs()
    {
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new string[] { FilePaths.GetFullPrefabCharacterObjectTypesPath() });

        foreach (string path in prefabPaths)
        {
            string fullPath = AssetDatabase.GUIDToAssetPath(path);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);

            string[] prefabSubNames = prefab.name.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            string objTypeTag = prefabSubNames[0].Substring(0, 1) + prefabSubNames[0].Substring(1).ToLower();
            if (!prefab.CompareTag(objTypeTag))
            {
                prefab.tag = objTypeTag;
            }
        }
    }
}
