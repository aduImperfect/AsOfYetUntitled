using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class CharacterObjectsCreator : MonoBehaviour
{
    [SerializeField] string charactersListFileName;

    public List<GameObject> characterObjectsInScene;

    private List<GameObject> allCharacterObjectsSubtypes;

    private CharacterFileData currCharacterFileData;

    [HideInInspector] public string currentCharacterObjSubtype;

    private CharacterObjects characterObjects = new CharacterObjects();

    public void GatherAllCharacterObjectsFromScene()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        if (characterObjectsInScene == null)
        {
            characterObjectsInScene = new List<GameObject>();
        }

        characterObjectsInScene.Clear();

        foreach (GameObject obj in allObjects)
        {
            characterObjectsInScene.Add(obj);
        }

        List<string> allLevelObjTypeNames = LevelObjectTypesContainer.GetLevelObjectTypes();

        //Remove all the Level Objects!!
        for (int lvlObjTypeNameIndex = 0; lvlObjTypeNameIndex < allLevelObjTypeNames.Count; ++lvlObjTypeNameIndex)
        {
            characterObjectsInScene.RemoveAll(t => t.CompareTag(allLevelObjTypeNames[lvlObjTypeNameIndex].Substring(0, 1) + allLevelObjTypeNames[lvlObjTypeNameIndex].Substring(1).ToLower()));
        }

        characterObjectsInScene.RemoveAll(t => t.CompareTag("Creator"));
        characterObjectsInScene.RemoveAll(t => t.CompareTag("Area"));
        characterObjectsInScene.RemoveAll(t => t.CompareTag("Parent"));
        characterObjectsInScene.RemoveAll(t => t.CompareTag("Untagged"));
        characterObjectsInScene.RemoveAll(t => t.CompareTag("Player"));
        characterObjectsInScene.RemoveAll(t => t.CompareTag("Invalid"));
        characterObjectsInScene.Remove(Camera.main.gameObject);
        characterObjectsInScene.Remove(this.gameObject);
    }

    public void GetAllCharacterTypePrefabs()
    {
        if (allCharacterObjectsSubtypes == null)
        {
            allCharacterObjectsSubtypes = new List<GameObject>();
        }

        allCharacterObjectsSubtypes.Clear();

        //Internal function runs based on relative path!!!
        string dataHandlingObjectSubTypePrefabDir = FilePaths.GetFullPrefabObjectTypesPath();
        allCharacterObjectsSubtypes = FindPrefabs.FindPrefabsInPath(dataHandlingObjectSubTypePrefabDir);
    }

    public void ClearCreatedCharacterObjects()
    {
        if (characterObjectsInScene == null)
        {
            characterObjectsInScene = new List<GameObject>();
        }

        characterObjectsInScene.Clear();
        this.transform.DestroyAllChildren();
    }

    public void ClearCreatedCharacterObjectsRuntime()
    {
        if (characterObjectsInScene == null)
        {
            characterObjectsInScene = new List<GameObject>();
        }

        characterObjectsInScene.Clear();

        if (this.transform.childCount == 0) return;

        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            GameObject childObj = this.transform.GetChild(i).gameObject;

            childObj.name = "DESTROY_" + i;
        }

        this.transform.DestroyAllChildrenRuntime();
    }

    public Sprite GetSpriteFromCharacterObjectSubtype(string characterObjectSubtypeToFind)
    {
        if (allCharacterObjectsSubtypes == null)
        {
            allCharacterObjectsSubtypes = new List<GameObject>();
            allCharacterObjectsSubtypes.Clear();
        }

        GetAllCharacterTypePrefabs();

        GameObject objInst = FindObject.FindByNameInList(characterObjectSubtypeToFind, ref allCharacterObjectsSubtypes);

        if (objInst == null)
        {
            return null;
        }

        SpriteRenderer spriteRenderer = objInst.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            return null;
        }

        return spriteRenderer.sprite;
    }

    public void AddCharacterObject()
    {
        GetAllCharacterTypePrefabs();

        GameObject currPrefabObj = null;

        foreach (GameObject currCharacterType in allCharacterObjectsSubtypes)
        {
            if (currCharacterType.name.Equals(currentCharacterObjSubtype))
            {
                currPrefabObj = currCharacterType;
                break;
            }
        }

        GameObject characterObjectsCreator = GameObject.Find(characterObjects.GetParentName());

        if (characterObjectsCreator == null)
        {
            Instantiate(characterObjectsCreator, Vector3.zero, Quaternion.identity);
        }

        string parentObjName = ("PARENT_" + StringParserWrapper.GetObjectTypeFromSubType(currentCharacterObjSubtype));
        GameObject parentObj = GameObject.Find(parentObjName);

        if (parentObj == null)
        {
            parentObj = new GameObject();
            parentObj.name = parentObjName;
            parentObj.transform.parent = characterObjectsCreator.transform;
            InternalEditorUtility.AddTag("Parent");
            parentObj.tag = "Parent";
            parentObj.transform.position = Vector2.zero;
        }

        GameObject spawn = Instantiate(currPrefabObj, parentObj.transform);

        ConvertToPrefabInstanceSettings prefabSettings = new ConvertToPrefabInstanceSettings();
        PrefabUtility.ConvertToPrefabInstance(spawn, currPrefabObj, prefabSettings, InteractionMode.AutomatedAction);

        IndividualCharacterManager spawnCharacterManager = spawn.GetComponent<IndividualCharacterManager>();
        
        spawn.name = currentCharacterObjSubtype;
        spawnCharacterManager.currentCharacterObjectSubtype = currentCharacterObjSubtype;
        characterObjectsInScene.Add(spawn);
    }

    public void SaveAllCharacters()
    {
        if (characterObjectsInScene == null)
        {
            characterObjectsInScene = new List<GameObject>();
            characterObjectsInScene.Clear();
        }

        GatherAllCharacterObjectsFromScene();

        string dirCharacter = FilePaths.GetFullCharactersListPath();
        Directory.CreateDirectory(dirCharacter);

        if (currCharacterFileData == null)
        {
            currCharacterFileData = new CharacterFileData();
        }
        
        CharacterFileDataList characterFileDataList = new CharacterFileDataList();

        string dirFilePath = Path.Combine(dirCharacter, charactersListFileName);

        foreach (GameObject character in characterObjectsInScene)
        {
            IndividualCharacterManager characterManager = character.GetComponent<IndividualCharacterManager>();

            CharacterFileData characterFileData = new CharacterFileData(characterManager.GetCharacterFileName(), characterManager.GetCharacterObjectSubType());
            characterFileDataList.characterDataList.Add(characterFileData);
            characterManager.SaveCharacter();
        }

        SaveAndLoadFile.SerializeObject<CharacterFileDataList>(characterFileDataList, dirFilePath);

        string dirDumpPath = FilePaths.GetFullCharacterDumpsPath();
        string dumpFileName = charactersListFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        Directory.CreateDirectory(dirDumpPath);

        string fullDumpPath = Path.Combine(dirDumpPath, dumpFileName);
        SaveAndLoadFile.SerializeObject<CharacterFileDataList>(characterFileDataList, fullDumpPath);
    }

    public void LoadAllCharacters()
    {
        if (Application.isPlaying)
        {
            ClearCreatedCharacterObjectsRuntime();
        }

        else
        {
            ClearCreatedCharacterObjects();
        }

        string dirCharacter = FilePaths.GetFullCharactersListPath();
        Directory.CreateDirectory(dirCharacter);

        if (currCharacterFileData == null)
        {
            currCharacterFileData = new CharacterFileData();
        }

        CharacterFileDataList characterFileDataList = new CharacterFileDataList();

        string dirFilePath = Path.Combine(dirCharacter, charactersListFileName);

        characterFileDataList = SaveAndLoadFile.DeSerializeObject<CharacterFileDataList>(dirFilePath);

        foreach (CharacterFileData characterFileData in characterFileDataList.characterDataList)
        {
            CreateCharacterObject(characterFileData.GetCharacterSubtype(), characterFileData.GetCharacterFileName());
        }
        ResetObjectsInSceneNumbering();
    }

    private void CreateCharacterObject(string characterObjectSubtype, string characterFileName)
    {
        if (characterObjects == null)
        {
            characterObjects = new CharacterObjects();
        }

        GameObject characterObjectCreator = GameObject.Find(characterObjects.GetParentName());

        if (characterObjectCreator == null)
        {
            //Look At
            //need to change
            //spawning the null object again
            return;
        }

        if (allCharacterObjectsSubtypes == null)
        {
            allCharacterObjectsSubtypes = new List<GameObject>();
            allCharacterObjectsSubtypes.Clear();
        }

        GetAllCharacterTypePrefabs();

        string parentObjName = "PARENT_" + StringParserWrapper.GetObjectTypeFromSubType(characterObjectSubtype);
        GameObject parentObj = GameObject.Find(parentObjName);

        if (parentObj == null)
        {
            parentObj = new GameObject();
            parentObj.name = parentObjName;
            parentObj.transform.parent = characterObjectCreator.transform;
            InternalEditorUtility.AddTag("Parent");
            parentObj.tag = "Parent";
            parentObj.transform.position = Vector2.zero;
        }

        GameObject currPrefabObj = null;

        foreach (GameObject currCharacterSubtype in allCharacterObjectsSubtypes)
        {
            if (currCharacterSubtype.name.Equals(characterObjectSubtype))
            {
                currPrefabObj = currCharacterSubtype;
                break;
            }
        }

        if (currPrefabObj == null)
        {
            return;
        }

        GameObject characterObj = SpawnCharacter(currPrefabObj, parentObj, characterFileName);
        characterObjectsInScene.Add(characterObj);
    }

    private GameObject SpawnCharacter(GameObject currPrefabObj, GameObject parentObj, string characterFileName)
    {
        ////Spawn object before changing data of objects if you don't want to mess with the prefabs
        GameObject spawnObj = Instantiate(currPrefabObj, Vector2.zero, Quaternion.identity, parentObj.transform);

        ConvertToPrefabInstanceSettings prefabSettings = new ConvertToPrefabInstanceSettings();
        PrefabUtility.ConvertToPrefabInstance(spawnObj, currPrefabObj, prefabSettings, InteractionMode.AutomatedAction);

        IndividualCharacterManager characterManager = spawnObj.GetComponent<IndividualCharacterManager>();
        spawnObj.transform.parent = parentObj.transform;
        characterManager.SetCharacterFileName(characterFileName);
        characterManager.UpdateCharacterFromFile();
        return spawnObj;
    }

    public void ResetObjectsInSceneNumbering()
    {
        if (characterObjectsInScene == null)
        {
            return;
        }

        int currentObjInSceneNum = 0;

        foreach (GameObject obj in characterObjectsInScene)
        {
            if (obj.CompareTag("Humanoid") || obj.CompareTag("Machine") || obj.CompareTag("Quadrupedal"))
            {
                obj.name = obj.name.ToString().Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries)[0] + "__" + (int)(currentObjInSceneNum++ + 1);
            }
        }
    }
}
