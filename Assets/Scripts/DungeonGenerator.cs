using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGeneratorFileInfo
{
    public int xSize;
    public int ySize;
    public int zSize;
    public int unitSize;
    public int floorCounterPrefix;
    public string floorParent;
    public List<int> floorPrefabIdentifiers;
    public List<Vector3> floorPositions;
    public List<Quaternion> floorOrientations;
    public List<PathfindingTerrainType> floorTerrainTypes;

    public DungeonGeneratorFileInfo()
    {
        xSize = -1;
        ySize = -1;
        zSize = -1;
        unitSize = -1;
        floorCounterPrefix = -1;
        floorParent = "NULL";
        floorPrefabIdentifiers = new List<int>();
        floorPositions = new List<Vector3>();
        floorOrientations = new List<Quaternion>();
        floorTerrainTypes = new List<PathfindingTerrainType>();
    }
}

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] public int xSize;
    [SerializeField] public int ySize;
    [SerializeField] public int zSize;
    [SerializeField] public int unitSize;
    [SerializeField] public GameObject[] floorPrefabs;
    [SerializeField] public string fileName;

    private DungeonGeneratorFileInfo dungeonGeneratorFileInfo;

    private List<int> floorPrefabIds = new List<int>();
    private List<GameObject> generatedDungeonObjects = new List<GameObject>();


    private void Awake()
    {
    }

    public void GenerateDungeon()
    {
        //Clear previously generated dungeon first.
        ClearGeneratedDungeon();

        int differentFloorsCount = floorPrefabs.Length;

        int instantiatedObjCount = 0;

        for (int xIndex = 0; xIndex < xSize; ++xIndex)
        {
            for (int yIndex = 0; yIndex < ySize; ++yIndex)
            {
                for (int zIndex = 0; zIndex < zSize; ++zIndex)
                {
                    int randomVal = Random.Range(-1, differentFloorsCount);

                    floorPrefabIds.Add(randomVal);

                    if (randomVal == -1)
                    {
                        generatedDungeonObjects.Add(null);
                    }
                    else
                    {
                        GameObject dungeonObj = Instantiate(floorPrefabs[randomVal], new Vector3(xIndex * unitSize, yIndex * unitSize, zIndex * unitSize), Quaternion.identity, this.transform);
                        dungeonObj.name = instantiatedObjCount + "_" + dungeonObj.name;
                        generatedDungeonObjects.Add(dungeonObj);

                        ++instantiatedObjCount;
                    }
                }
            }
        }
    }

    public void ClearGeneratedDungeon()
    {
        floorPrefabIds.Clear();
        generatedDungeonObjects.Clear();
        this.transform.DestroyAllChildren();
    }

    public void SaveGeneratedDungeonToFile()
    {
        if (generatedDungeonObjects.Count == 0)
        {
            return;
        }

        if (dungeonGeneratorFileInfo == null)
        {
            dungeonGeneratorFileInfo = new DungeonGeneratorFileInfo();
        }

        dungeonGeneratorFileInfo.xSize = xSize;
        dungeonGeneratorFileInfo.ySize = ySize;
        dungeonGeneratorFileInfo.zSize = zSize;
        dungeonGeneratorFileInfo.unitSize = unitSize;
        dungeonGeneratorFileInfo.floorCounterPrefix = 0;
        dungeonGeneratorFileInfo.floorParent = this.transform.name;

        dungeonGeneratorFileInfo.floorPrefabIdentifiers.Clear();
        dungeonGeneratorFileInfo.floorPositions.Clear();
        dungeonGeneratorFileInfo.floorOrientations.Clear();
        dungeonGeneratorFileInfo.floorTerrainTypes.Clear();

        int floorPrefabsInc = 0;

        foreach (GameObject generatedObject in generatedDungeonObjects)
        {
            dungeonGeneratorFileInfo.floorPrefabIdentifiers.Add(floorPrefabIds[floorPrefabsInc]);

            //Add the invalid empty floors as well!!
            if (generatedObject == null)
            {
                dungeonGeneratorFileInfo.floorPositions.Add(Vector3.zero);
                dungeonGeneratorFileInfo.floorOrientations.Add(Quaternion.identity);
                dungeonGeneratorFileInfo.floorTerrainTypes.Add(PathfindingTerrainType.INVALID_TERRAIN);
            }
            else
            {
                dungeonGeneratorFileInfo.floorPositions.Add(generatedObject.transform.position);
                dungeonGeneratorFileInfo.floorOrientations.Add(generatedObject.transform.rotation);
                dungeonGeneratorFileInfo.floorTerrainTypes.Add(PathfindingTerrainType.NORMAL_TERRAIN);
            }

            //Increment counters.
            ++floorPrefabsInc;
            ++dungeonGeneratorFileInfo.floorCounterPrefix;
        }

        SaveAndLoadFile.SerializeObject<DungeonGeneratorFileInfo>(dungeonGeneratorFileInfo, fileName);
    }

    public void LoadDungeonFromFile()
    {
        ClearGeneratedDungeon();

        if (dungeonGeneratorFileInfo == null)
        {
            dungeonGeneratorFileInfo = new DungeonGeneratorFileInfo();
        }

        dungeonGeneratorFileInfo = SaveAndLoadFile.DeSerializeObject<DungeonGeneratorFileInfo>(fileName);

        GenerateDungeonFromFile();
    }

    private void GenerateDungeonFromFile()
    {
        generatedDungeonObjects.Clear();

        GameObject parentObj = GameObject.Find(dungeonGeneratorFileInfo.floorParent);

        //If the parent object does not exist in the scene. Instantiate it!!
        if (parentObj == null)
        {
            Instantiate(parentObj, Vector3.zero, Quaternion.identity);
        }

        int instantiatedObjCount = 0;
        for (int floorIndex = 0; floorIndex < dungeonGeneratorFileInfo.floorCounterPrefix; ++floorIndex)
        {
            if (dungeonGeneratorFileInfo.floorTerrainTypes[floorIndex] == PathfindingTerrainType.INVALID_TERRAIN)
            {
                generatedDungeonObjects.Add(null);
            }
            else
            {
                GameObject dungeonObj = Instantiate(floorPrefabs[dungeonGeneratorFileInfo.floorPrefabIdentifiers[floorIndex]], dungeonGeneratorFileInfo.floorPositions[floorIndex], dungeonGeneratorFileInfo.floorOrientations[floorIndex], parentObj.transform);
                dungeonObj.name = instantiatedObjCount + "_" + dungeonObj.name;
                generatedDungeonObjects.Add(dungeonObj);

                ++instantiatedObjCount;
            }
        }

        xSize = dungeonGeneratorFileInfo.xSize;
        ySize = dungeonGeneratorFileInfo.ySize;
        zSize = dungeonGeneratorFileInfo.zSize;
        unitSize = dungeonGeneratorFileInfo.unitSize;
    }
}
