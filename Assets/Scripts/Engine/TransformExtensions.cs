using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void DestroyAllChildren(this Transform t)
    {
        if (t.childCount == 0) return;

        for (int i = t.childCount - 1; i >= 0; i--)
        {
            UnityEngine.Object.DestroyImmediate(t.GetChild(i).gameObject);
        }
    }

    public static void DestroyAllChildrenRuntime(this Transform t)
    {
        if (t.childCount == 0) return;

        for (int i = t.childCount - 1; i >= 0; --i)
        {
            UnityEngine.Object.Destroy(t.GetChild(i).gameObject);
        }
    }

    public static void DestroyAllChildrenRuntime(this Transform t, GameObject objectToNotDestroy)
    {
        if (t.childCount == 0) return;

        for (int i = t.childCount - 1; i >= 0; --i)
        {
            if ((objectToNotDestroy != null) && (t.GetChild(i).gameObject.Equals(objectToNotDestroy)))
            {
                continue;
            }

            UnityEngine.Object.Destroy(t.GetChild(i).gameObject);
        }
    }

    public static void GatherAllChildren(this Transform t, ref List<GameObject> childObjs, uint depth)
    {
        if (depth == 0)
        {
            return;
        }

        if (childObjs == null)
        {
            childObjs = new List<GameObject>();
        }

        if (t.childCount == 0) return;

        for (int i = 0; i < t.childCount; ++i)
        {
            GameObject childObj = t.GetChild(i).gameObject;
            childObj.transform.GatherAllChildren(ref childObjs, depth - 1);
            childObjs.Add(childObj);
        }

        return;
    }
}
