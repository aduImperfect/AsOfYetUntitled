using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class FindSprites
{
    private const string SPRITESDIRECTORY = "Assets\\Sprites\\Cow\\1.Cow_Idle_Side-Sheet.png";

    static List<Sprite> allSpriteData = new List<Sprite>();

    public static Sprite FindSprtieByFileName(string fileName)
    {
        string[] fileNameBreakdown = fileName.Split('_');
        string parentFileName = "";

        if (int.TryParse(fileNameBreakdown[fileNameBreakdown.Length - 1], out int temp))
        {
            //parentFileName = 
        }
        Debug.Log(parentFileName);
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("File name is null or empty.");
            return null;
        }

        if (allSpriteData.Count <= 0)
        {
            FindAllSpritesInSpriteDirectory();
        }

        foreach (Sprite sprite in allSpriteData)
        {
            if (sprite.name == fileName)
            {
                return sprite;
            }
        }
        return null;
    }

    private static void FindAllSpritesInSpriteDirectory()
    {
        allSpriteData.Clear();
        Object[] allData = AssetDatabase.LoadAllAssetsAtPath(SPRITESDIRECTORY);

        foreach (Object data in allData)
        {
            if(data is Sprite)
            {
                allSpriteData.Add(data as Sprite);
            }
        }
    }

}
