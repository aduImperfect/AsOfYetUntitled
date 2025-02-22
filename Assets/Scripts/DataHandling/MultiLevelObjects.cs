
using System.Collections.Generic;

public class MultiLevelObjects
{
    public List<LevelObjects> multiLevelObjectsList;

    string parentName;

    public MultiLevelObjects()
    {
        multiLevelObjectsList = new List<LevelObjects>();
        parentName = "LevelObjectsCreator";
    }

    public void Clear()
    {
        multiLevelObjectsList.Clear();
    }

    public string GetParentName()
    {
        return parentName;
    }
}
