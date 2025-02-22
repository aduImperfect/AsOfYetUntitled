using System.IO;

public static class FilePaths
{
    private static readonly string dataHandlingFolderPath = "DataHandling";
    private static readonly string levelsFolderPath = "Levels";
    private static readonly string charactersFolderPath = "Characters";
    private static readonly string charactersListFolderPath = "CharactersList";
    private static readonly string weaponsFolderPath = "Weapons";
    private static readonly string dumpsMainFolderPath = "_Dumps";
    private static readonly string levelDumpsFolderPath = "_Levels";
    private static readonly string characterDumpsFolderPath = "_Characters";
    private static readonly string weaponDumpsFolderPath = "_Weapons";
    private static readonly string mainLevelsPath = "Main";
    private static readonly string tempLevelsPath = "_Temp";
    private static readonly string extraLevelsPath = "_Extras";
    private static readonly string adityaLevelsPath = "_Aditya";
    private static readonly string balajiLevelsPath = "_Balaji";
    private static readonly string kartheekLevelsPath = "_Kartheek";
    private static readonly string kowsikLevelsPath = "_Kowsik";
    private static readonly string tanishLevelsPath = "_Tanish";
    private static readonly string tarunLevelsPath = "_Tarun";

    private static readonly string assetsFolderPath = "Assets";
    private static readonly string prefabsFolderPath = "Prefabs";
    private static readonly string scriptsFolderPath = "Scripts";
    private static readonly string resourcesFolderPath = "Resources";
    private static readonly string prefabObjectTypesFolderPath = "ObjectTypes";
    private static readonly string prefabSceneObjectsFolderPath = "SceneObjects";
    private static readonly string prefabExtension = ".prefab";
    private static readonly string metaExtension = ".meta";

    private static readonly string sceneImagesFolderPath = "SceneImages";

    private static readonly string sceneCreatorImagesFolderPath = "SceneCreatorImages";

    private static readonly string objectTypeScriptPath = "ObjectType.cs";
    private static readonly string objectSubtypeScriptPath = "ObjectSubtype.cs";

    public static string GetMainDataHandlingPath()
    {
        return dataHandlingFolderPath;
    }

    public static string GetFullLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath);
    }

    public static string GetMainLevelsPath()
    {
        return Path.Combine(mainLevelsPath);
    }

    public static string GetFullMainLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, mainLevelsPath);
    }

    public static string GetTempLevelsPath()
    {
        return Path.Combine(tempLevelsPath);
    }

    public static string GetFullTempLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, tempLevelsPath);
    }

    public static string GetExtrasLevelsPath()
    {
        return Path.Combine(extraLevelsPath);
    }

    public static string GetFullExtrasLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath);
    }

    public static string GetAdityaLevelsPath()
    {
        return Path.Combine(extraLevelsPath, adityaLevelsPath);
    }

    public static string GetFullAdityaLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, adityaLevelsPath);
    }

    public static string GetBalajiLevelsPath()
    {
        return Path.Combine(extraLevelsPath, balajiLevelsPath);
    }

    public static string GetFullBalajiLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, balajiLevelsPath);
    }

    public static string GetKartheekLevelsPath()
    {
        return Path.Combine(extraLevelsPath, kartheekLevelsPath);
    }

    public static string GetFullKartheekLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, kartheekLevelsPath);
    }

    public static string GetKowsikLevelsPath()
    {
        return Path.Combine(extraLevelsPath, kowsikLevelsPath);
    }

    public static string GetFullKowsikLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, kowsikLevelsPath);
    }

    public static string GetTanishLevelsPath()
    {
        return Path.Combine(extraLevelsPath, tanishLevelsPath);
    }

    public static string GetFullTanishLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, tanishLevelsPath);
    }

    public static string GetTarunLevelsPath()
    {
        return Path.Combine(extraLevelsPath, tarunLevelsPath);
    }

    public static string GetFullTarunLevelsPath()
    {
        return Path.Combine(dataHandlingFolderPath, levelsFolderPath, extraLevelsPath, tarunLevelsPath);
    }

    public static string GetFullCharactersPath()
    {
        return Path.Combine(dataHandlingFolderPath, charactersFolderPath);
    }

    public static string GetFullCharactersListPath()
    {
        return Path.Combine(dataHandlingFolderPath, charactersFolderPath, charactersListFolderPath);
    }

    public static string GetFullWeaponsPath()
    {
        return Path.Combine(dataHandlingFolderPath, weaponsFolderPath);
    }

    public static string GetFullMainDumpsPath()
    {
        return Path.Combine(dataHandlingFolderPath, dumpsMainFolderPath);
    }

    public static string GetFullLevelDumpsPath()
    {
        return Path.Combine(dataHandlingFolderPath, dumpsMainFolderPath, levelDumpsFolderPath);
    }

    public static string GetFullCharacterDumpsPath()
    {
        return Path.Combine(dataHandlingFolderPath, dumpsMainFolderPath, characterDumpsFolderPath);
    }

    public static string GetFullWeaponDumpsPath()
    {
        return Path.Combine(dataHandlingFolderPath, dumpsMainFolderPath, weaponDumpsFolderPath);
    }

    public static string GetFullPrefabDataHandlingPath()
    {
        return Path.Combine(assetsFolderPath, prefabsFolderPath, dataHandlingFolderPath);
    }

    public static string GetFullPrefabObjectTypesPath()
    {
        return Path.Combine(assetsFolderPath, prefabsFolderPath, dataHandlingFolderPath, prefabObjectTypesFolderPath);
    }

    public static string GetFullPrefabLevelObjectTypesPath()
    {
        return Path.Combine(assetsFolderPath, prefabsFolderPath, dataHandlingFolderPath, prefabObjectTypesFolderPath, levelsFolderPath);
    }

    public static string GetFullPrefabCharacterObjectTypesPath()
    {
        return Path.Combine(assetsFolderPath, prefabsFolderPath, dataHandlingFolderPath, prefabObjectTypesFolderPath, charactersFolderPath);
    }

    public static string GetFullPrefabSceneObjectsPath()
    {
        return Path.Combine(assetsFolderPath, prefabsFolderPath, dataHandlingFolderPath, prefabSceneObjectsFolderPath);
    }

    public static string GetPrefabExtension()
    {
        return prefabExtension;
    }

    public static string GetMetaExtension()
    {
        return metaExtension;
    }

    public static string GetFullResourcesPath()
    {
        return Path.Combine(assetsFolderPath, resourcesFolderPath);
    }

    public static string GetFullSceneImagesPath()
    {
        return Path.Combine(sceneImagesFolderPath);
    }

    public static string GetFullSceneCreatorImagesPath()
    {
        return Path.Combine(sceneImagesFolderPath, sceneCreatorImagesFolderPath);
    }

    public static string GetFullScriptDataHandlingPath()
    {
        return Path.Combine(assetsFolderPath, scriptsFolderPath, dataHandlingFolderPath);
    }

    public static string GetFullScriptObjectTypePath()
    {
        return Path.Combine(assetsFolderPath, scriptsFolderPath, dataHandlingFolderPath, objectTypeScriptPath);
    }

    public static string GetFullScriptObjectSubtypePath()
    {
        return Path.Combine(assetsFolderPath, scriptsFolderPath, dataHandlingFolderPath, objectSubtypeScriptPath);
    }
}
