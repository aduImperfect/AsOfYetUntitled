using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public static class LevelObjectTypesContainer
{
    private static List<string> levelObjectTypes = new List<string>();
    private static Dictionary<string, List<string> > levelObjectSubtypes = new Dictionary<string, List<string> >();

    private static List<string> allLevelObjectSubtypes = new List<string>();

    public static void ResetLevelObjectTypes()
    {
        levelObjectTypes.Clear();
    }

    public static void ResetLevelObjectSubtypes()
    {
        levelObjectSubtypes.Clear();
        allLevelObjectSubtypes.Clear();
    }

    public static int GetLevelObjectTypesCount()
    {
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        return levelObjectTypes.Count;
    }

    public static int GetLevelObjectSubtypesCountUnderALevelObjectType(string objType)
    {
        return levelObjectSubtypes[objType].Count;
    }

    public static int GetTotalLevelObjectSubtypesCount()
    {
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        return allLevelObjectSubtypes.Count;
    }

    public static List<string> GetLevelObjectTypes()
    {
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        return levelObjectTypes;
    }

    public static List<string> GetLevelObjectSubtypesUnderLevelObjectType(string levelObjectType)
    {
        return levelObjectSubtypes[levelObjectType];
    }

    public static List<string> GetAllLevelObjectSubtypes()
    {
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        return allLevelObjectSubtypes;
    }

    public static void AddLevelObjectsType(string objType)
    {
        if (levelObjectTypes.Contains(objType))
        {
            Debug.Log(objType + " already exists!!");
            return;
        }

        levelObjectTypes.Add(objType);
        levelObjectSubtypes.Add(objType, new List<string>());
        CreateNewLevelObjectTypeFolder(objType);

        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
    }

    public static void AddCharacterObjectsSubtype(string objSubtype)
    {
        string[] objSubTypeStrs = objSubtype.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

        bool isCategoryKeyThere = levelObjectSubtypes.ContainsKey(objSubTypeStrs[0]);

        if (isCategoryKeyThere)
        {
            List<string> objSubTypes = levelObjectSubtypes[objSubTypeStrs[0]];

            if ((objSubTypes != null) && objSubTypes.Contains(objSubtype))
            {
                Debug.Log(objSubtype + " already exists!!");
                return;
            }

            levelObjectSubtypes[objSubTypeStrs[0]].Add(objSubtype);
            Debug.Log("New LevelObjectSubtype: " + objSubtype + " added under the LevelObjectType: " + objSubTypeStrs[0] + ".");
            allLevelObjectSubtypes.Add(objSubtype);

            CreateNewLevelObjectSubtypePrefab(objSubTypeStrs[0], objSubtype);
            return;
        }

        levelObjectTypes.Add(objSubTypeStrs[0]);
        levelObjectSubtypes.Add(objSubTypeStrs[0], new List<string>());

        levelObjectSubtypes[objSubTypeStrs[0]].Add(objSubtype);
        Debug.Log("New LevelObjectSubtype: " + objSubtype + " added under the LevelObjectType: " + objSubTypeStrs[0] + ".");
        allLevelObjectSubtypes.Add(objSubtype);

        CreateNewLevelObjectSubtypePrefab(objSubTypeStrs[0], objSubtype);

        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
    }

    public static void AddInvalidLevelObjectTypeAndSubtype()
    {
        levelObjectSubtypes["INVALID"].Add("INVALID");
    }

    public static void RemoveLevelObjectType(string objType)
    {
        levelObjectTypes.Remove(objType);
        DeleteLevelObjectTypeFolder(objType);
    }

    public static void RemoveLevelObjectSubtype(string objSubtype)
    {
        string[] objSubTypeStrs = objSubtype.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

        bool isCategoryKeyThere = levelObjectSubtypes.ContainsKey(objSubTypeStrs[0]);

        if (!isCategoryKeyThere)
        {
            Debug.Log("No Level Object Type with name: " + objSubTypeStrs[0] + " exists for " + objSubtype + "!!!");
        }

        levelObjectSubtypes[objSubTypeStrs[0]].Remove(objSubtype);

        allLevelObjectSubtypes.Remove(objSubtype);

        //If there are no more subtype objects under the specific object type then remove the object type completely!!
        if (levelObjectSubtypes[objSubTypeStrs[0]].Count == 0)
        {
            RemoveLevelObjectType(objSubTypeStrs[0]);
        }
        else
        {
            DeleteLevelObjectSubtype(objSubTypeStrs[0], objSubtype);
        }
    }

    public static string GetLevelObjectTypeVal(int objTypeNum)
    {
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
        return levelObjectTypes[objTypeNum];
    }

    private static void CreateNewLevelObjectTypeFolder(string objType)
    {
        Directory.CreateDirectory(Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType));
        AssetDatabase.Refresh();
    }

    public static void GatherAllLevelObjectTypesAndSubtypesFromPrefabs()
    {
        string dataHandlingObjectSubTypePrefabDir = FilePaths.GetFullPrefabLevelObjectTypesPath();
        List <GameObject> allPrefabs = FindPrefabs.FindPrefabsInPath(dataHandlingObjectSubTypePrefabDir);

        ResetLevelObjectTypes();
        ResetLevelObjectSubtypes();

        foreach (GameObject prefab in allPrefabs)
        {
            string[] prefabType = prefab.name.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            bool isCategoryKeyThere = levelObjectSubtypes.ContainsKey(prefabType[0]);

            if (isCategoryKeyThere)
            {
                List<string> objSubTypes = levelObjectSubtypes[prefabType[0]];

                if ((objSubTypes != null) && objSubTypes.Contains(prefab.name))
                {
                    //Debug.Log(prefab.name + " already exists!!");
                    continue;
                }

                levelObjectSubtypes[prefabType[0]].Add(prefab.name);
                //Debug.Log("New LevelObjectSubtype: " + prefab.name + " added under the LevelObjectType: " + prefabType[0] + ".");

                allLevelObjectSubtypes.Add(prefab.name);
                continue;
            }

            levelObjectTypes.Add(prefabType[0]);
            levelObjectSubtypes.Add(prefabType[0], new List<string>());

            levelObjectSubtypes[prefabType[0]].Add(prefab.name);
            //Debug.Log("New LevelObjectSubtype: " + prefab.name + " added under the LevelObjectType: " + prefabType[0] + ".");

            allLevelObjectSubtypes.Add(prefab.name);
        }
    }

    private static void DeleteLevelObjectSubtype(string objType, string objSubtype)
    {
        File.Delete(Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType, objSubtype + FilePaths.GetPrefabExtension()));
        File.Delete(Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType, objSubtype + FilePaths.GetMetaExtension()));

        AssetDatabase.Refresh();

        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
    }

    private static void DeleteLevelObjectTypeFolder(string objType)
    {
        Directory.Delete(Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType), true);
        File.Delete(Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType + FilePaths.GetMetaExtension()));

        AssetDatabase.Refresh();

        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
    }

    private static void CreateNewLevelObjectSubtypePrefab(string objType, string objSubtype)
    {
        CreateNewLevelObjectTypeFolder(objType);

        GameObject gObject = new GameObject();
        gObject.name = objSubtype;

        bool prefabSuccess = false;
        PrefabUtility.SaveAsPrefabAsset(gObject, Path.Combine(FilePaths.GetFullPrefabLevelObjectTypesPath(), objType, objSubtype + FilePaths.GetPrefabExtension()), out prefabSuccess);

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
        GatherAllLevelObjectTypesAndSubtypesFromPrefabs();
    }
}
