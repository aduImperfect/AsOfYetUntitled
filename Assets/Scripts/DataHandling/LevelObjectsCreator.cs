using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelObjectsCreator : MonoBehaviour
{
    private List<GameObject> allLevelObjectSubTypes;

    [SerializeField] public string newLevelAreaFileName;

    [SerializeField] public List<string> multiLayeredLevelFileNames;
    [SerializeField] public int batchLevelNumber = 1;
    [SerializeField] public int currentBatchLevelNumber = 0;
    [SerializeField] public bool isLoading;
    [SerializeField] public bool isLoaded;
    [SerializeField] public bool autoLoad;

    [SerializeField] public List<GameObject> levelObjectsInScene;
    [SerializeField] public List<GameObject> areaLevelObjectsInScene;

    [HideInInspector] public string currLevelObjSubType;

    private MultiLevelObjects multiLevelObjects = new MultiLevelObjects();

    public void FixedUpdate()
    {
        if (batchLevelNumber == 0)
        {
            return;
        }

        else if ((autoLoad == true) && (isLoaded == false) && (isLoading == false))
        {
            LoadMultiLayeredLevelsFromFiles();
        }

        if ((isLoading == true) && (isLoaded == false))
        {
            if (currentBatchLevelNumber != batchLevelNumber)
            {
                //Add remaining loaded objects into the scene in batches!!
                CreateLevelFromFile();
            }
            else
            {
                isLoaded = true;
            }
        }
    }

    public void GetAllLevelObjectSubTypePrefabs()
    {
        if (allLevelObjectSubTypes == null)
        {
            allLevelObjectSubTypes = new List<GameObject>();
        }

        allLevelObjectSubTypes.Clear();

        //Internal function runs based on relative path!!!
        string dataHandlingObjectSubTypePrefabDir = FilePaths.GetFullPrefabLevelObjectTypesPath();
        allLevelObjectSubTypes = FindPrefabs.FindPrefabsInPath(dataHandlingObjectSubTypePrefabDir);
    }

    public void ClearCreatedLevelObjects()
    {
        currentBatchLevelNumber = 0;
        isLoaded = isLoading = false;

        if (levelObjectsInScene == null)
        {
            levelObjectsInScene = new List<GameObject>();
        }

        levelObjectsInScene.Clear();
        areaLevelObjectsInScene.Clear();

        this.transform.DestroyAllChildren();
    }

    public void ClearCreatedLevelObjectsRuntime()
    {
        currentBatchLevelNumber = 0;
        isLoaded = isLoading = false;

        if (levelObjectsInScene == null)
        {
            levelObjectsInScene = new List<GameObject>();
        }

        levelObjectsInScene.Clear();
        areaLevelObjectsInScene.Clear();

        if (this.transform.childCount == 0) return;

        for (int i = this.transform.childCount - 1; i >= 0; --i)
        {
            GameObject childObj = this.transform.GetChild(i).gameObject;
            childObj.name = "DESTROY_" + i;
        }

        this.transform.DestroyAllChildrenRuntime();
    }

    public void SaveLevelsToFiles()
    {
        GameObject levelObjectsCreator = GameObject.Find(multiLevelObjects.GetParentName());

        //If the main level objects creator does not exist in the scene. Instantiate it!!
        if (levelObjectsCreator == null)
        {
            Instantiate(levelObjectsCreator, Vector3.zero, Quaternion.identity);
        }

        bool noMainAreaLevel = false;

        string mainLevelObjectsAreaName = "AREA_" + newLevelAreaFileName;

        GameObject mainAreaObj = new GameObject();
        mainAreaObj.name = mainLevelObjectsAreaName;
        mainAreaObj.transform.parent = levelObjectsCreator.transform;
        mainAreaObj.transform.position = Vector3.zero;
        mainAreaObj.tag = "Area";

        LevelObjects mainLevelObjects = new LevelObjects();

        //If there is no valid new file name.. delete the new mainArea Object that is being created altogether!!
        if (newLevelAreaFileName.Equals(""))
        {
            noMainAreaLevel = true;

            if (Application.isPlaying)
            {
                Destroy(mainAreaObj);
            }
            else
            {
                DestroyImmediate(mainAreaObj);
            }
        }

        string dirPath = FilePaths.GetFullLevelsPath();
        string dirDumpPath = FilePaths.GetFullLevelDumpsPath();

        Directory.CreateDirectory(dirPath);
        Directory.CreateDirectory(dirDumpPath);

        if (multiLevelObjects == null)
        {
            multiLevelObjects = new MultiLevelObjects();
        }

        if (multiLevelObjects.multiLevelObjectsList == null)
        {
            multiLevelObjects.multiLevelObjectsList = new List<LevelObjects>();
        }

        multiLevelObjects.multiLevelObjectsList.Clear();

        for (int index = 0; index < multiLayeredLevelFileNames.Count; ++index)
        {
            LevelObjects levelObjects = new LevelObjects();

            if (levelObjectsInScene == null)
            {
                levelObjectsInScene = new List<GameObject>();
                levelObjectsInScene.Clear();
            }

            GatherAllLevelObjectsFromScene();

            levelObjects.levelObjectsAreaName = "AREA_" + multiLayeredLevelFileNames[index];

            GameObject areaObj = GameObject.Find(levelObjects.levelObjectsAreaName);

            //An area parent for separation of multiple areas in a level!
            if (areaObj == null)
            {
                Debug.LogWarning("No Area Parent Object of the name: " + levelObjects.levelObjectsAreaName + " found!!! Cannot save file!!! Skipping to next Area Save.");
                continue;
            }

            if (!areaObj.activeInHierarchy)
            {
                Debug.LogWarning("Area Parent Object of the name: " + levelObjects.levelObjectsAreaName + " is Inactive!!! Hence skipped saving this file!!! Skipping to next Area Save.");
                continue;
            }

            levelObjects.levelObjectsAreaPositionX = areaObj.transform.position.x;
            levelObjects.levelObjectsAreaPositionY = areaObj.transform.position.y;
            levelObjects.levelObjectsAreaPositionZ = areaObj.transform.position.z;

            levelObjects.levelObjectsAreaTag = areaObj.tag;

            List<GameObject> childGameObjectsOfAreaObject = new List<GameObject>();
            areaObj.transform.GatherAllChildren(ref childGameObjectsOfAreaObject, 2);

            foreach (GameObject obj in childGameObjectsOfAreaObject)
            {
                //Ignore objects with the Parent tag!!
                if (obj.CompareTag("Parent"))
                {
                    continue;
                }

                string[] objName = obj.name.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);

                //ObjectSubtype currObjType = StringParserWrapper.GetEnumObjectSubtype(objName[0].ToUpper());

                string currObjType = objName[0].ToUpper();
                levelObjects.levelObjList.Add(new ObjectData(obj, currObjType));

                if (noMainAreaLevel)
                {
                    continue;
                }

                ObjectData mainObjData = new ObjectData(obj, currObjType);

                string objParentName = obj.transform.parent.name;
                string[] objParentNames = objParentName.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);

                mainObjData.SetParentName(mainAreaObj.name + "__" + objParentNames[1]);

                mainLevelObjects.levelObjList.Add(mainObjData);
            }

            string fullLevelPath = Path.Combine(dirPath, multiLayeredLevelFileNames[index]);
            SaveAndLoadFile.SerializeObject<LevelObjects>(levelObjects, fullLevelPath);

            //Dump Saves!!
            string dumpFileName = multiLayeredLevelFileNames[index] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            string fullDumpPath = Path.Combine(dirDumpPath, dumpFileName);
            SaveAndLoadFile.SerializeObject<LevelObjects>(levelObjects, fullDumpPath);
        }

        if (noMainAreaLevel)
        {
            return;
        }

        mainLevelObjects.levelObjectsAreaName = mainAreaObj.name;

        mainLevelObjects.levelObjectsAreaPositionX = mainAreaObj.transform.position.x;
        mainLevelObjects.levelObjectsAreaPositionY = mainAreaObj.transform.position.y;
        mainLevelObjects.levelObjectsAreaPositionZ = mainAreaObj.transform.position.z;

        mainLevelObjects.levelObjectsAreaTag = mainAreaObj.tag;

        string fullMainLevelPath = Path.Combine(dirPath, newLevelAreaFileName);

        //This is particularly for an additional file that is created by combining the other area files and objects!!
        SaveAndLoadFile.SerializeObject<LevelObjects>(mainLevelObjects, fullMainLevelPath);

        //Dump Saves!!
        string dumpMainFileName = mainLevelObjects.levelObjectsAreaName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        string fullMainDumpPath = Path.Combine(dirDumpPath, dumpMainFileName);
        SaveAndLoadFile.SerializeObject<LevelObjects>(mainLevelObjects, fullMainDumpPath);

        multiLevelObjects.multiLevelObjectsList.Add(mainLevelObjects);

        //Job complete!!
        if (Application.isPlaying)
        {
            Destroy(mainAreaObj);
        }
        else
        {
            DestroyImmediate(mainAreaObj);
        }

        areaLevelObjectsInScene.Remove(mainAreaObj);
    }

    public void LoadMultiLayeredLevelsFromFiles()
    {
        if (levelObjectsInScene == null)
        {
            currentBatchLevelNumber = 0;
        }
        else if (levelObjectsInScene.Count == 0)
        {
            currentBatchLevelNumber = 0;
        }

        if ((currentBatchLevelNumber == 0) || (currentBatchLevelNumber == batchLevelNumber))
        {
            if (Application.isPlaying)
            {
                ClearCreatedLevelObjectsRuntime();
            }
            else
            {
                ClearCreatedLevelObjects();
            }
        }

        if (multiLevelObjects == null)
        {
            multiLevelObjects = new MultiLevelObjects();
        }

        string dirPath = FilePaths.GetFullLevelsPath();
        Directory.CreateDirectory(dirPath);

        //Still processing the batch loading.
        if ((currentBatchLevelNumber == 0) || (currentBatchLevelNumber == batchLevelNumber))
        {
            currentBatchLevelNumber = 0;
            multiLevelObjects.Clear();

            foreach (string currFileName in multiLayeredLevelFileNames)
            {
                string fullLevelPath = Path.Combine(dirPath, currFileName);
                LevelObjects levelObjects = SaveAndLoadFile.DeSerializeObject<LevelObjects>(fullLevelPath);
                multiLevelObjects.multiLevelObjectsList.Add(levelObjects);
            }
        }

        CreateLevelFromFile();

        isLoading = true;
    }

    public void LoadLevelsFromMainFolder()
    {
        if (levelObjectsInScene == null)
        {
            currentBatchLevelNumber = 0;
        }
        else if (levelObjectsInScene.Count == 0)
        {
            currentBatchLevelNumber = 0;
        }

        if ((currentBatchLevelNumber == 0) || (currentBatchLevelNumber == batchLevelNumber))
        {
            if (Application.isPlaying)
            {
                ClearCreatedLevelObjectsRuntime();
            }
            else
            {
                ClearCreatedLevelObjects();
            }
        }

        if (multiLevelObjects == null)
        {
            multiLevelObjects = new MultiLevelObjects();
        }

        string dirPath = FilePaths.GetFullMainLevelsPath();
        Directory.CreateDirectory(dirPath);

        //Still processing the batch loading.
        if ((currentBatchLevelNumber == 0) || (currentBatchLevelNumber == batchLevelNumber))
        {
            currentBatchLevelNumber = 0;
            multiLevelObjects.Clear();

            string[] mainFiles = Directory.GetFiles(dirPath);

            foreach (string currFileName in mainFiles)
            {
                LevelObjects levelObjects = SaveAndLoadFile.DeSerializeObject<LevelObjects>(currFileName);
                multiLevelObjects.multiLevelObjectsList.Add(levelObjects);
            }
        }

        CreateLevelFromFile();

        isLoading = true;
    }

    public void GatherAllLevelObjectsFromScene()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        if (levelObjectsInScene == null)
        {
            levelObjectsInScene = new List<GameObject>();
        }

        levelObjectsInScene.Clear();

        foreach (GameObject obj in allObjects)
        {
            levelObjectsInScene.Add(obj);
        }

        List<string> allCharacterObjTypeNames = CharacterObjectTypesContainer.GetCharacterObjectTypes();

        //Remove all the Character Object Types From The Scene!!
        for (int chrObjTypeNameIndex = 0; chrObjTypeNameIndex < allCharacterObjTypeNames.Count; ++chrObjTypeNameIndex)
        {
            levelObjectsInScene.RemoveAll(t => t.CompareTag(allCharacterObjTypeNames[chrObjTypeNameIndex].Substring(0, 1) + allCharacterObjTypeNames[chrObjTypeNameIndex].Substring(1).ToLower()));
        }

        levelObjectsInScene.RemoveAll(t => t.CompareTag("Creator"));
        levelObjectsInScene.RemoveAll(t => t.CompareTag("Area"));
        levelObjectsInScene.RemoveAll(t => t.CompareTag("Parent"));
        levelObjectsInScene.RemoveAll(t => t.CompareTag("Untagged"));
        levelObjectsInScene.RemoveAll(t => t.CompareTag("Player"));
        levelObjectsInScene.RemoveAll(t => t.CompareTag("Invalid"));
        levelObjectsInScene.Remove(Camera.main.gameObject);
        levelObjectsInScene.Remove(this.gameObject);

        //Find all the area objects in the scene.
        if (areaLevelObjectsInScene == null)
        {
            areaLevelObjectsInScene = new List<GameObject>();
        }

        areaLevelObjectsInScene.Clear();

        foreach (GameObject obj in allObjects)
        {
            if (!obj.tag.Equals("Area"))
            {
                continue;
            }

            areaLevelObjectsInScene.Add(obj);
        }

        //Reset Level Objects' Scene Numbering.
        ResetObjectsInSceneNumbering();
    }

    public Sprite GetSpriteFromLevelObjectSubType(string objTypeToFind)
    {
        if (allLevelObjectSubTypes == null)
        {
            allLevelObjectSubTypes = new List<GameObject>();
            allLevelObjectSubTypes.Clear();
        }

        GetAllLevelObjectSubTypePrefabs();

        GameObject objInst = FindObject.FindByNameInList(objTypeToFind, ref allLevelObjectSubTypes);

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

    public void ResetObjectsInSceneNumbering()
    {
        if (levelObjectsInScene == null)
        {
            return;
        }

        int currentObjInSceneNum = 0;

        foreach (GameObject obj in levelObjectsInScene)
        {
            obj.name = obj.name.ToString().Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries)[0] + "__" + (int)(currentObjInSceneNum++ + 1);
        }
    }

    public void AddLevelObject()
    {
        if (allLevelObjectSubTypes == null)
        {
            allLevelObjectSubTypes = new List<GameObject>();
            allLevelObjectSubTypes.Clear();
        }

        GetAllLevelObjectSubTypePrefabs();

        GameObject objInst = FindObject.FindByNameInList(currLevelObjSubType, ref allLevelObjectSubTypes);

        if (objInst == null)
        {
            return;
        }

        GameObject levelObjectsCreator = GameObject.Find(multiLevelObjects.GetParentName());

        //If the parent object does not exist in the scene. Instantiate it!!
        if (levelObjectsCreator == null)
        {
            Instantiate(levelObjectsCreator, Vector3.zero, Quaternion.identity);
        }

        string areaObjName = "AREA_" + newLevelAreaFileName;
        GameObject areaObj = GameObject.Find(areaObjName);

        //An area parent for separation of multiple areas in a level!
        if (areaObj == null)
        {
            areaObj = new GameObject();
            areaObj.name = areaObjName;
            areaObj.transform.parent = levelObjectsCreator.transform;
            areaObj.transform.position = Vector3.zero;
            areaObj.tag = "Area";
        }

        string type = StringParserWrapper.GetObjectTypeFromSubType(currLevelObjSubType);

        string parentObjName = areaObj.name + "__PARENT_" + type;

        GameObject parentObj = GameObject.Find(parentObjName);

        //If the parent object does not exist in the scene. Instantiate it!!
        if (parentObj == null)
        {
            parentObj = new GameObject();
            parentObj.name = parentObjName;
            parentObj.transform.parent = areaObj.transform;
            parentObj.transform.localPosition = Vector3.zero;
            parentObj.tag = "Parent";
        }

        GameObject instantiatedObj = Instantiate(objInst, parentObj.transform.position, objInst.transform.rotation, parentObj.transform);

        ConvertToPrefabInstanceSettings prefabSettings = new ConvertToPrefabInstanceSettings();
        PrefabUtility.ConvertToPrefabInstance(instantiatedObj, objInst, prefabSettings, InteractionMode.AutomatedAction);

        int currentCount = levelObjectsInScene.Count + 1;

        instantiatedObj.name = objInst.name + "__" + currentCount;

        GatherAllLevelObjectsFromScene();
    }

    public void ShiftSelectedLevelObjectsToArea()
    {
        //Selected Level Objects.
        GameObject[] selectedObjects = Selection.gameObjects;

        GameObject levelObjectsCreator = GameObject.Find(multiLevelObjects.GetParentName());

        //Area handling.
        string areaObjName = "AREA_" + newLevelAreaFileName;
        GameObject areaObj = GameObject.Find(areaObjName);

        //An area parent for separation of multiple areas in a level!
        if (areaObj == null)
        {
            areaObj = new GameObject();
            areaObj.name = areaObjName;
            areaObj.transform.parent = levelObjectsCreator.transform;
            //areaObj.transform.position = Vector3.zero;
            areaObj.tag = "Area";
        }

        List<string> levelObjectTypeNames = LevelObjectTypesContainer.GetLevelObjectTypes();

        foreach (GameObject selectedObject in selectedObjects)
        {
            foreach (string levelObjectTypeName in levelObjectTypeNames)
            {
                if (selectedObject.CompareTag(levelObjectTypeName.Substring(0, 1) + levelObjectTypeName.Substring(1).ToLower()))
                {
                    string parentObjName = areaObjName + "__PARENT_" + levelObjectTypeName.ToString();
                    GameObject parentObj = GameObject.Find(parentObjName);

                    if (parentObj == null)
                    {
                        parentObj = new GameObject();
                        parentObj.name = parentObjName;
                        parentObj.transform.parent = areaObj.transform;
                        parentObj.tag = "Parent";
                    }

                    selectedObject.transform.parent = parentObj.transform;
                    break;
                }
            }
        }

    }

    private void CreateLevelFromFile()
    {
        if ((currentBatchLevelNumber == 0) || (currentBatchLevelNumber == batchLevelNumber))
        {
            levelObjectsInScene.Clear();
        }

        GameObject levelObjectsCreator = GameObject.Find(multiLevelObjects.GetParentName());

        //If the parent object does not exist in the scene. Instantiate it!! (THIS SHOULD NOT BE POSSIBLE!!).
        if (levelObjectsCreator == null)
        {
            Instantiate(levelObjectsCreator, Vector3.zero, Quaternion.identity);
        }

        foreach (LevelObjects lvlObj in multiLevelObjects.multiLevelObjectsList)
        {
            string areaObjName = lvlObj.levelObjectsAreaName;

            GameObject areaObj = GameObject.Find(areaObjName);

            //An area parent for separation of multiple areas in a level!
            if (areaObj == null)
            {
                areaObj = new GameObject();
                areaObj.name = areaObjName;
                areaObj.transform.parent = levelObjectsCreator.transform;
                areaObj.transform.position = new Vector3(lvlObj.levelObjectsAreaPositionX, lvlObj.levelObjectsAreaPositionY, lvlObj.levelObjectsAreaPositionZ);
                areaObj.tag = "Area";
            }

            GetAllLevelObjectSubTypePrefabs();

            int numberToLoadAndDisplay = lvlObj.levelObjList.Count / batchLevelNumber;

            int batchStart = currentBatchLevelNumber * numberToLoadAndDisplay;
            int batchEnd = (currentBatchLevelNumber < (batchLevelNumber - 1)) ? ((currentBatchLevelNumber + 1) * numberToLoadAndDisplay + 1) : lvlObj.levelObjList.Count;

            for (int objIndex = batchStart; objIndex < batchEnd; ++objIndex)
            {
                string parentObjName = areaObjName + "__PARENT_" + lvlObj.levelObjList[objIndex].GetObjectType();
                GameObject parentObj = GameObject.Find(parentObjName);

                if (parentObj == null)
                {
                    parentObj = new GameObject();
                    parentObj.name = parentObjName;
                    parentObj.transform.parent = areaObj.transform;
                    parentObj.tag = "Parent";
                }

                GameObject currPrefabObj = null;

                foreach (GameObject currObjType in allLevelObjectSubTypes)
                {
                    if (currObjType.name.Equals(lvlObj.levelObjList[objIndex].GetObjectSubtype()))
                    {
                        currPrefabObj = currObjType;
                        break;
                    }
                }

                if (currPrefabObj == null)
                {
                    ///////////// null reference go awaayy *casts magic*
                    continue;
                }

                currPrefabObj.SetActive(lvlObj.levelObjList[objIndex].GetIsActive());

                GameObject levelObj = SpawnLevelObject(currPrefabObj, parentObj, lvlObj.levelObjList[objIndex]);
            }
        }

        currentBatchLevelNumber++;
        GatherAllLevelObjectsFromScene();
    }

    private GameObject SpawnLevelObject(GameObject currPrefabObj, GameObject parentObj, ObjectData currObjectData)
    {
        ///Spawn object before changing data of objects if you don't want to mess with the prefabs
        GameObject spawnObj = Instantiate(currPrefabObj, currObjectData.GetPosition(), currObjectData.GetOrientation(), parentObj.transform);

        ConvertToPrefabInstanceSettings prefabSettings = new ConvertToPrefabInstanceSettings();
        PrefabUtility.ConvertToPrefabInstance(spawnObj, currPrefabObj, prefabSettings, InteractionMode.AutomatedAction);

        spawnObj.name = currObjectData.GetName();

        Transform objTransform = spawnObj.GetComponent<Transform>();
        objTransform.localScale = currObjectData.GetScale();

        //Managing sprite data.
        SpriteData sprData = currObjectData.GetSpriteData();
        SpriteRenderer levelObjSprRen = spawnObj.GetComponent<SpriteRenderer>();

        if (levelObjSprRen != null)
        {
            levelObjSprRen.sprite.name = sprData.GetSpriteName();
            levelObjSprRen.color = sprData.GetSpriteColor();
            levelObjSprRen.flipX = sprData.GetSpriteFlipX();
            levelObjSprRen.flipY = sprData.GetSpriteFlipY();
            levelObjSprRen.sharedMaterial.name = sprData.GetSpriteMaterialName();
            levelObjSprRen.enabled = sprData.GetSpriteIsEnabled();
            levelObjSprRen.sortingLayerName = sprData.GetSpriteSortingLayerName();
            levelObjSprRen.sortingLayerID = sprData.GetSpriteSortingLayerID();
            levelObjSprRen.sortingOrder = sprData.GetSpriteSortingOrder();
            levelObjSprRen.renderingLayerMask = sprData.GetSpriteRenderingLayerMask();
        }

        Collider2D[] coll2DList = spawnObj.GetComponents<Collider2D>();

        //Destroy prior colliders (set up from the prefabs) that exist!!
        //if (Application.isPlaying)
        //{
        //    for (int i = 0; i < coll2DList.Length; ++i)
        //    {
        //        Destroy(coll2DList[i]);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < coll2DList.Length; ++i)
        //    {
        //        DestroyImmediate(coll2DList[i]);
        //    }
        //}

        ////Managing collider data.
        ColliderData[] collDataList = currObjectData.GetColliderData();

        foreach (ColliderData collData in collDataList)
        {
            foreach (Collider2D coll2D in coll2DList)
            {
                if (coll2D is BoxCollider2D && collData is BoxColliderData)
                {
                    BoxCollider2D boxCollider = coll2D as BoxCollider2D;
                    BoxColliderData boxColliderData = collData as BoxColliderData;

                    boxCollider.offset = boxColliderData.GetOffset();
                    boxCollider.isTrigger = boxColliderData.GetIsTrigger();
                    boxCollider.enabled = boxColliderData.GetIsEnabled();
                    boxCollider.size = boxColliderData.GetColliderSize();
                    break;
                }
                
                if (collData is PolygonColliderData)
                {
                    PolygonCollider2D polygonCollider = coll2D as PolygonCollider2D;
                    PolygonColliderData polygonColliderData = collData as PolygonColliderData;

                    polygonCollider.offset = polygonColliderData.GetOffset();
                    polygonCollider.isTrigger = polygonColliderData.GetIsTrigger();
                    polygonCollider.enabled = polygonColliderData.GetIsEnabled();
                    polygonCollider.points = polygonColliderData.GetPolygonColliderPointsList().ToArray();
                    break;
                }
                
                if (collData is CircleColliderData)
                {
                    CircleCollider2D circleCollider = coll2D as CircleCollider2D;
                    CircleColliderData circleColliderData = collData as CircleColliderData;

                    circleCollider.offset = circleColliderData.GetOffset();
                    circleCollider.isTrigger = circleColliderData.GetIsTrigger();
                    circleCollider.enabled = circleColliderData.GetIsEnabled();
                    circleCollider.radius = circleColliderData.GetColliderRadius();
                    break;
                }
                
                if (collData is CapsuleColliderData)
                {
                    CapsuleCollider2D capsuleCollider = coll2D as CapsuleCollider2D;
                    CapsuleColliderData capsuleColliderData = collData as CapsuleColliderData;

                    capsuleCollider.offset = capsuleColliderData.GetOffset();
                    capsuleCollider.isTrigger = capsuleColliderData.GetIsTrigger();
                    capsuleCollider.enabled = capsuleColliderData.GetIsEnabled();
                    capsuleCollider.size = capsuleColliderData.GetColliderSize();
                    break;
                }
            }
        }

        //Set Light Data Here

        Light2dData lightData = currObjectData.GetLight2DData();
        Light2D light2D = spawnObj.GetComponent<Light2D>();

        if (light2D != null)
        {
            lightData.SetDataToLight2D(ref light2D);
        }

        LevelSwitcherData levelSwitcherData = currObjectData.GetLevelSwitcherData();
        LevelSwitcher lvlSwitcher = spawnObj.GetComponent<LevelSwitcher>();

        if (lvlSwitcher != null)
        {
            lvlSwitcher.newLevelName = levelSwitcherData.GetLevelName();
        }

        spawnObj.layer = currObjectData.GetLayer();

        return spawnObj;
    }
}
