using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class InsaneClass : IXmlSerializable
{
    [XmlAttribute("Value1")]
    public int val1;

    [XmlAttribute("Value2")]
    public Vector3 val2;

    public InsaneClass()
    {
        val1 = 1;
        val2 = Vector3.zero;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
        return (null);
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("InsaneClassObject");

        reader.ReadStartElement("Value1");
        val1 = StringParserWrapper.GetInt(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("Value2");
        val2 = StringParserWrapper.GetVector3(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("Value1");
        writer.WriteString(val1.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("Value2");
        writer.WriteString(val2.ToString());
        writer.WriteEndElement();
    }
}

public class FloorTileInfo : IXmlSerializable
{
    [XmlAttribute("FloorTilePrefabId")]
    public int floorTilePrefabId;

    [XmlAttribute("FloorTilePosition")]
    public Vector3 floorTilePosition;

    [XmlAttribute("FloorTileOrientation")]
    public Quaternion floorTileOrientation;

    [XmlAttribute("FloorTileTerrainType")]
    public PathfindingTerrainType floorTileTerrainType;

    [XmlElement("InsaneClassObject")]
    public InsaneClass insaneClassObject;

    public FloorTileInfo()
    {
        floorTilePrefabId = -1;
        floorTilePosition = Vector3.zero;
        floorTileOrientation = Quaternion.identity;
        floorTileTerrainType = PathfindingTerrainType.INVALID_TERRAIN;
        insaneClassObject = new InsaneClass();
    }

    public FloorTileInfo(GameObject obj)
    {
        floorTilePrefabId = -1;
        floorTilePosition = obj.transform.position;
        floorTileOrientation = obj.transform.rotation;
        floorTileTerrainType = PathfindingTerrainType.INVALID_TERRAIN;
    }

    public void SetData(GameObject obj)
    {
        floorTilePrefabId = -1;
        floorTilePosition = obj.transform.position;
        floorTileOrientation = obj.transform.rotation;
        floorTileTerrainType = PathfindingTerrainType.INVALID_TERRAIN;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("FloorTileInfo");

        reader.ReadStartElement("FloorTilePrefabId");
        floorTilePrefabId = StringParserWrapper.GetInt(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("FloorTilePosition");
        floorTilePosition = StringParserWrapper.GetVector3(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("FloorTileOrientation");
        floorTileOrientation = StringParserWrapper.GetQuaternion(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("FloorTileTerrainType");
        floorTileTerrainType = StringParserWrapper.GetEnumPathfindingTerrainType(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("FloorTilePrefabId");
        writer.WriteString(floorTilePrefabId.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("FloorTilePosition");
        writer.WriteString(floorTilePosition.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("FloorTileOrientation");
        writer.WriteString(floorTileOrientation.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("FloorTileTerrainType");
        writer.WriteString(floorTileTerrainType.ToString());
        writer.WriteEndElement();
    }
}

public class DungeonGeneratorFileInfo
{
    public int xSize;
    public int ySize;
    public int zSize;
    public int unitSize;
    public int floorCounterPrefix;
    public string floorParent;
    public List<FloorTileInfo> floorTiles;

    public DungeonGeneratorFileInfo()
    {
        xSize = -1;
        ySize = -1;
        zSize = -1;
        unitSize = -1;
        floorCounterPrefix = -1;
        floorParent = "NULL";
        floorTiles = new List<FloorTileInfo>();
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
                    int randomVal = UnityEngine.Random.Range(-1, differentFloorsCount);

                    floorPrefabIds.Add(randomVal);

                    if (randomVal == -1)
                    {
                        generatedDungeonObjects.Add(null);
                        floorPrefabIds.Remove(randomVal);
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
        //Remove all non-null elements!!
        generatedDungeonObjects.RemoveAll(item => item != null);

        //Re-add with updated information from the generator parent!
        foreach (Transform child in this.transform)
        {
            generatedDungeonObjects.Add(child.gameObject);
        }

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

        dungeonGeneratorFileInfo.floorTiles.Clear();

        int floorPrefabsInc = 0;

        foreach (GameObject generatedObject in generatedDungeonObjects)
        {
            FloorTileInfo newFlrTileInfo = new FloorTileInfo();

            if (generatedObject == null)
            {
                //Default values (works also for invalid floor tiles as well!!).
                newFlrTileInfo.floorTilePrefabId = -1;
                newFlrTileInfo.floorTilePosition = Vector3.zero;
                newFlrTileInfo.floorTileOrientation = Quaternion.identity;
                newFlrTileInfo.floorTileTerrainType = PathfindingTerrainType.INVALID_TERRAIN;

                newFlrTileInfo.insaneClassObject.val1 = 1;
                newFlrTileInfo.insaneClassObject.val2 = Vector3.zero;

            }
            else
            {
                newFlrTileInfo.floorTilePrefabId = floorPrefabIds[floorPrefabsInc];
                newFlrTileInfo.floorTilePosition = generatedObject.transform.position;
                newFlrTileInfo.floorTileOrientation = generatedObject.transform.rotation;
                newFlrTileInfo.floorTileTerrainType = PathfindingTerrainType.NORMAL_TERRAIN;

                newFlrTileInfo.insaneClassObject.val1 = 1000;
                newFlrTileInfo.insaneClassObject.val2 = new Vector3(1001, 1002, 1003);

                //Only increment this for the valid index range!
                ++floorPrefabsInc;
            }

            dungeonGeneratorFileInfo.floorTiles.Add(newFlrTileInfo);

            //Increment counters.
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
            if (dungeonGeneratorFileInfo.floorTiles[floorIndex].floorTileTerrainType == PathfindingTerrainType.INVALID_TERRAIN)
            {
                generatedDungeonObjects.Add(null);
            }
            else
            {
                //Store the prefabIds.
                floorPrefabIds.Add(dungeonGeneratorFileInfo.floorTiles[floorIndex].floorTilePrefabId);

                GameObject dungeonObj = Instantiate(floorPrefabs[dungeonGeneratorFileInfo.floorTiles[floorIndex].floorTilePrefabId], dungeonGeneratorFileInfo.floorTiles[floorIndex].floorTilePosition, dungeonGeneratorFileInfo.floorTiles[floorIndex].floorTileOrientation, parentObj.transform);
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
