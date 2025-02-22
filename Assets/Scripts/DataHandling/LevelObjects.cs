
using System.Collections.Generic;

public class LevelObjects
{
    public string levelObjectsAreaName;
    public float levelObjectsAreaPositionX;
    public float levelObjectsAreaPositionY;
    public float levelObjectsAreaPositionZ;
    public string levelObjectsAreaTag;
    public List<ObjectData> levelObjList;

    public LevelObjects()
    {
        levelObjectsAreaName = "AREA_INVALID";
        levelObjectsAreaPositionX = 0;
        levelObjectsAreaPositionY = 0;
        levelObjectsAreaPositionZ = 0;
        levelObjectsAreaTag = "Invalid";
        levelObjList = new List<ObjectData>();
    }

    public void Clear()
    {
        levelObjectsAreaName = "AREA_INVALID";
        levelObjectsAreaPositionX = 0;
        levelObjectsAreaPositionY = 0;
        levelObjectsAreaPositionZ = 0;
        levelObjectsAreaTag = "Invalid";
        levelObjList.Clear();
    }
}
