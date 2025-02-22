using System.Collections.Generic;
using UnityEngine;

public class FindObject
{
    public static GameObject FindByNameInList(string name, ref List<GameObject> allObjs)
    {
        foreach (GameObject obj in allObjs)
        {
            string objName = obj.name.ToUpper();
            if (objName.Equals(name))
            {
                return obj;
            }
        }

        return null;
    }
    public static GameObject FindByTagInList(string tag, ref List<GameObject> allObjs)
    {
        foreach (GameObject obj in allObjs)
        {
            string objTag = obj.tag.ToUpper();
            if (objTag.Equals(tag))
            {
                return obj;
            }
        }

        return null;
    }
}
