using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class FindPrefabs
{
    public static List<GameObject> FindPrefabsInPath(string pathToSearch)
    {
        List<GameObject> allPrefabs = new List<GameObject>();
        allPrefabs.Clear();

        IEnumerable<string> prefabFiles = Directory.EnumerateFiles(pathToSearch, "*.prefab", SearchOption.AllDirectories);

        foreach (string prefabPath in prefabFiles)
        {
            GameObject prefabObject = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            allPrefabs.Add(prefabObject);
        }

        return allPrefabs;
    }
}
